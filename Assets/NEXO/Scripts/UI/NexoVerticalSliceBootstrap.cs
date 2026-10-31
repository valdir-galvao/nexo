using System.Collections;
using UnityEngine;
using Nexo.Core;
using Nexo.Audio;
using Nexo.Gameplay;
using Nexo.Persistence;
using Nexo.Progression;
using Nexo.Safety;
using Nexo.Learning;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Nexo.UI
{
    /// <summary>Bootstrap temporário: cria uma ilha jogável sem depender de prefabs.</summary>
    public sealed class NexoVerticalSliceBootstrap : MonoBehaviour
    {
        private VerticalSliceMission mission;
        private Transform playerTransform;
        private Camera view;
        private Vector2 touchMove;
        private string prompt = "";
        private bool showIntro = true;
        private bool showMenu = true;
        private bool showDiary;
        private bool showSettings;
        private NexoAudioDirector audioDirector;
        private LocalDataControls dataControls;
        private bool confirmDelete;
        private bool showTransfer;
        private bool transferAnswered;
        private float introElapsed;
        private bool worldBuilt;
        private Canvas uiCanvas;
        private CanvasGroup introGroup;
        private Text introTitle;
        private Text introSubtitle;
        private Text introActionLabel;
        private RectTransform introButtonRect;
        private ParticleSystem introParticles;
        private bool modernUiReady;
        private Coroutine introTransition;
        private Material defaultWorldMaterial;
        private float timeScaleBeforePause = 1f;
        private Transform challengeDoorTransform;
        private Renderer challengeDoorRenderer;
        private bool challengeOpen;
        private bool challengeAnswered;
        private bool challengeCorrect;
        private int score;
        private string challengeFeedback = "";
        private Coroutine challengePulse;
        private NexoChallengeDoor activeChallengeDoor;
        private int activeStage = 1;
        private int highScore;
        private const string HighScoreKey = "NEXO_High_Score";
        private const string StageKey = "NEXO_Unlocked_Stage";

        public GameState CurrentState { get { return mission == null ? GameState.Initializing : mission.StateMachine.State; } }
        public bool GameplayMaterialReady { get { return defaultWorldMaterial != null && defaultWorldMaterial.shader != null; } }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateIfMissing()
        {
            try
            {
                if (FindFirstObjectByType<NexoVerticalSliceBootstrap>() == null)
                {
                    var runtimeObject = new GameObject("Nexo Vertical Slice Runtime");
                    if (runtimeObject != null && runtimeObject.AddComponent<NexoVerticalSliceBootstrap>() == null)
                        Debug.LogError("Nexo: n?o foi poss?vel criar o componente de bootstrap.");
                }
            }
            catch (System.Exception exception) { Debug.LogException(exception); }
        }

        private void Start()
        {
            try
            {
                StartCore();
            }
            catch (System.Exception exception)
            {
                Debug.LogException(exception);
                // Keep the process alive even if a platform-specific service fails during boot.
                showMenu = true;
                showIntro = true;
                modernUiReady = false;
                EnsurePresentationCameraSafe();
            }
        }

        private void StartCore()
        {
            ConfigureMobileRuntime();
            PreloadGraphics();
            EnsureMobileCanvas();
            EnsurePresentationCamera();
            BuildIntroParticles();
            highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
            activeStage = Mathf.Clamp(PlayerPrefs.GetInt(StageKey, 1), 1, 3);
            mission = new VerticalSliceMission(new JsonFileSaveStore());
            mission.Initialize();
            audioDirector = gameObject.AddComponent<NexoAudioDirector>();
            if (audioDirector != null) audioDirector.Configure(mission.Save != null ? mission.Save.audio : null);
            dataControls = new LocalDataControls();
            showIntro = mission.StateMachine.State == GameState.Intro;
            introElapsed = 0f;
            transferAnswered = HasTransferEvidence();
            RefreshModernIntro();
            if (modernUiReady) StartCoroutine(FadeIntroIn());
            // Keep the intro path UI-only. Build gameplay physics only after entry.
            if (!showIntro) EnsureWorldBuilt();
        }

        private void EnsurePresentationCameraSafe()
        {
            try
            {
                if (view == null) view = Camera.main;
                if (view == null)
                {
                    var cameraObject = new GameObject("Nexo Safe Camera");
                    if (cameraObject != null) view = cameraObject.AddComponent<Camera>();
                }
                if (view != null)
                {
                    view.clearFlags = CameraClearFlags.SolidColor;
                    view.backgroundColor = new Color(0.025f, 0.055f, 0.12f, 1f);
                    view.enabled = true;
                }
            }
            catch (System.Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private void ConfigureMobileRuntime()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            try
            {
                if (pauseStatus)
                {
                    timeScaleBeforePause = Time.timeScale;
                    Time.timeScale = 0f;
                    if (introParticles != null) introParticles.Pause(true);
                }
                else
                {
                    Time.timeScale = timeScaleBeforePause > 0f ? timeScaleBeforePause : 1f;
                    if (introParticles != null && (showIntro || showMenu)) introParticles.Play(true);
                }
            }
            catch (System.Exception exception) { Debug.LogException(exception); }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            try
            {
                if (!hasFocus)
                {
                    if (introParticles != null) introParticles.Pause(true);
                }
                else if (introParticles != null && (showIntro || showMenu) && Time.timeScale > 0f)
                {
                    introParticles.Play(true);
                }
            }
            catch (System.Exception exception) { Debug.LogException(exception); }
        }

        private void PreloadGraphics()
        {
            string[] requiredShaders =
            {
                "Universal Render Pipeline/Lit",
                "Universal Render Pipeline/Unlit",
                "UI/Default",
                "TextMeshPro/Distance Field"
            };
            for (int i = 0; i < requiredShaders.Length; i++) Shader.Find(requiredShaders[i]);

            bool urpActive = GraphicsSettings.currentRenderPipeline != null;
            Shader worldShader = urpActive ? Shader.Find("Universal Render Pipeline/Unlit") : Shader.Find("Unlit/Color");
            if (worldShader == null) worldShader = urpActive ? Shader.Find("Unlit/Color") : Shader.Find("Universal Render Pipeline/Unlit");
            if (worldShader == null) worldShader = Shader.Find("Sprites/Default");
            Shader fallbackShader = Shader.Find("UI/Default");
            if (fallbackShader == null) fallbackShader = Shader.Find("Standard");
            if (worldShader == null) worldShader = fallbackShader;
            var resourceMaterial = Resources.Load<Material>("Materials/URP_Default_Gameplay");
            if (resourceMaterial != null && worldShader != null && (resourceMaterial.shader == null || resourceMaterial.shader != worldShader))
                resourceMaterial.shader = worldShader;
            if (resourceMaterial != null && resourceMaterial.shader == null && fallbackShader != null)
                resourceMaterial.shader = fallbackShader;
            if (resourceMaterial != null && resourceMaterial.shader != null)
                defaultWorldMaterial = resourceMaterial;
            if (worldShader != null && defaultWorldMaterial == null)
                defaultWorldMaterial = new Material(worldShader) { name = "Nexo Runtime World Material" };
            if (defaultWorldMaterial == null)
                Debug.LogError("Nexo: nenhum shader de material disponivel; geometria sera ocultada para evitar magenta.");
        }

        private void EnsureWorldBuilt()
        {
            if (worldBuilt) return;
            worldBuilt = true;
            BuildWorld();
        }

        private void EnsureMobileCanvas()
        {
            Canvas canvas = GetComponentInChildren<Canvas>(true);
            if (canvas == null)
            {
                var canvasObject = new GameObject("Nexo Mobile UI Canvas");
                canvasObject.transform.SetParent(transform, false);
                canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObject.AddComponent<GraphicRaycaster>();
            }

            uiCanvas = canvas;
            if (!Application.isBatchMode && EventSystem.current == null)
            {
                var eventSystem = new GameObject("Nexo UI EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }

            var scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler == null) scaler = canvas.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
            BuildModernIntro(canvas);
        }

        private void EnsurePresentationCamera()
        {
            view = Camera.main;
            if (view == null)
            {
                var cameraObject = new GameObject("Nexo Main Camera");
                cameraObject.tag = "MainCamera";
                view = cameraObject.AddComponent<Camera>();
            }
            view.clearFlags = CameraClearFlags.SolidColor;
            view.backgroundColor = new Color(0.025f, 0.055f, 0.12f, 1f);
            view.enabled = true;
        }

        private void BuildModernIntro(Canvas canvas)
        {
            if (canvas == null || introGroup != null) return;
            var root = new GameObject("Nexo Intro Design System");
            root.transform.SetParent(canvas.transform, false);
            var rect = root.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            introGroup = root.AddComponent<CanvasGroup>();

            var panel = new GameObject("Intro Panel");
            panel.transform.SetParent(root.transform, false);
            var panelRect = panel.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.08f, 0.12f);
            panelRect.anchorMax = new Vector2(0.92f, 0.88f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;
            var panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0.035f, 0.09f, 0.18f, 0.96f);

            introTitle = CreateIntroText(panel.transform, "NEXO", 76, FontStyle.Bold, new Vector2(0.08f, 0.60f), new Vector2(0.92f, 0.90f));
            introSubtitle = CreateIntroText(panel.transform, "Reconstrua a conexao entre as ilhas.", 32, FontStyle.Normal, new Vector2(0.10f, 0.42f), new Vector2(0.90f, 0.62f));
            var hint = CreateIntroText(panel.transform, "Observe. Descubra. Restaure.", 26, FontStyle.Italic, new Vector2(0.10f, 0.30f), new Vector2(0.90f, 0.42f));
            hint.color = new Color(0.42f, 0.86f, 1f, 1f);

            var buttonObject = new GameObject("Start Mission Button");
            buttonObject.transform.SetParent(panel.transform, false);
            introButtonRect = buttonObject.AddComponent<RectTransform>();
            introButtonRect.anchorMin = new Vector2(0.20f, 0.08f);
            introButtonRect.anchorMax = new Vector2(0.80f, 0.25f);
            introButtonRect.offsetMin = Vector2.zero;
            introButtonRect.offsetMax = Vector2.zero;
            var buttonImage = buttonObject.AddComponent<Image>();
            buttonImage.color = new Color(0.10f, 0.58f, 0.82f, 1f);
            var button = buttonObject.AddComponent<Button>();
            button.targetGraphic = buttonImage;
            var buttonColors = button.colors;
            buttonColors.normalColor = new Color(0.10f, 0.58f, 0.82f, 1f);
            buttonColors.highlightedColor = new Color(0.18f, 0.72f, 0.95f, 1f);
            buttonColors.pressedColor = new Color(0.06f, 0.36f, 0.60f, 1f);
            button.colors = buttonColors;
            button.onClick.AddListener(HandleStartButtonPressed);
            introActionLabel = CreateIntroText(buttonObject.transform, "INICIAR MISSAO", 32, FontStyle.Bold, Vector2.zero, Vector2.one);
            introActionLabel.alignment = TextAnchor.MiddleCenter;
            modernUiReady = true;
        }

        private void BuildIntroParticles()
        {
            if (view == null || introParticles != null) return;
            var particleObject = new GameObject("Nexo Intro Floating Lights");
            particleObject.transform.SetParent(view.transform, false);
            particleObject.transform.localPosition = new Vector3(0f, 0f, 5f);
            introParticles = particleObject.AddComponent<ParticleSystem>();
            var main = introParticles.main;
            main.loop = true;
            main.playOnAwake = true;
            main.simulationSpace = ParticleSystemSimulationSpace.Local;
            main.maxParticles = 28;
            main.startLifetime = 6f;
            main.startSpeed = 0.05f;
            main.startSize = 0.06f;
            main.startColor = new Color(0.35f, 0.85f, 1f, 0.75f);
            var emission = introParticles.emission;
            emission.rateOverTime = 6f;
            var shape = introParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Box;
            shape.scale = new Vector3(12f, 7f, 0.5f);
            var velocity = introParticles.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.Local;
            // Unity exige que os três eixos usem o mesmo modo de curva.
            // TwoConstants evita a mistura entre Constant e TwoConstants em batchmode/IL2CPP.
            velocity.x = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocity.y = new ParticleSystem.MinMaxCurve(0.12f, 0.28f);
            velocity.z = new ParticleSystem.MinMaxCurve(0f, 0f);
            introParticles.Play();
        }

        private Text CreateIntroText(Transform parent, string value, int size, FontStyle style, Vector2 anchorMin, Vector2 anchorMax)
        {
            var textObject = new GameObject("Intro Text");
            textObject.transform.SetParent(parent, false);
            var rect = textObject.AddComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            var text = textObject.AddComponent<Text>();
            text.text = value;
            // Unity 6 removed Arial.ttf from the built-in runtime resources.
            // LegacyRuntime.ttf is the supported headless/mobile-safe font.
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = size;
            text.fontStyle = style;
            text.alignment = TextAnchor.MiddleCenter;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            text.color = Color.white;
            return text;
        }

        private void RefreshModernIntro()
        {
            if (!modernUiReady || introGroup == null) return;
            bool visible = showMenu || showIntro;
            introGroup.gameObject.SetActive(visible);
            if (!visible) return;
            introGroup.alpha = 1f;
            introSubtitle.text = showMenu ? "Reconstrua a conexao entre as ilhas." : "Observe a fonte, siga o canal e restaure o fluxo.";
            introActionLabel.text = showMenu ? "INICIAR MISSAO" : "ENTRAR NA ILHA";
        }

        private void BeginModernMission()
        {
            showMenu = false;
            if (showIntro)
            {
                mission.SkipIntro();
                showIntro = false;
                introElapsed = 0f;
                if (introTransition != null) StopCoroutine(introTransition);
                introTransition = StartCoroutine(FadeIntoMission());
                return;
            }
            RefreshModernIntro();
        }

        private void HandleStartButtonPressed()
        {
            if (introButtonRect != null) StartCoroutine(PunchButton(introButtonRect));
            BeginModernMission();
        }

        private IEnumerator PunchButton(RectTransform target)
        {
            Vector3 baseScale = Vector3.one;
            float duration = 0.28f;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                float pulse = Mathf.Sin(t * Mathf.PI) * 0.12f;
                target.localScale = baseScale * (1f + pulse);
                yield return null;
            }
            target.localScale = baseScale;
        }

        private IEnumerator FadeIntroIn()
        {
            if (introGroup == null) yield break;
            introGroup.alpha = 0f;
            float elapsed = 0f;
            while (elapsed < 0.55f)
            {
                elapsed += Time.unscaledDeltaTime;
                introGroup.alpha = EaseInOut(Mathf.Clamp01(elapsed / 0.55f));
                yield return null;
            }
            introGroup.alpha = 1f;
        }

        private IEnumerator FadeIntoMission()
        {
            if (introGroup == null) yield break;
            introGroup.interactable = false;
            introGroup.blocksRaycasts = false;
            float startAlpha = introGroup.alpha;
            float elapsed = 0f;
            while (elapsed < 0.5f)
            {
                elapsed += Time.unscaledDeltaTime;
                introGroup.alpha = Mathf.Lerp(startAlpha, 0f, EaseInOut(Mathf.Clamp01(elapsed / 0.5f)));
                yield return null;
            }
            PreloadGraphics();
            yield return null;
            EnsureWorldBuilt();
            if (audioDirector != null) audioDirector.PlayDiscovery();
            introGroup.alpha = 0f;
            introGroup.gameObject.SetActive(false);
            if (introParticles != null) introParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            introTransition = null;
        }

        private static float EaseInOut(float value)
        {
            return value * value * (3f - 2f * value);
        }

        private void BuildWorld()
        {
            RenderSettings.ambientLight = NexoVisualTheme.Sky;
            EnsureGameplayLight();
            MakeCube("Island", new Vector3(0, -0.5f, 0), new Vector3(18, 1, 18), new Color(0.3f, 0.45f, 0.3f));
            InitializeInteractable(MakeCube("Source", new Vector3(-5, 0.4f, 4), new Vector3(1.4f, 0.8f, 1.4f), NexoVisualTheme.Water, true), ClueId.Source, "Fonte alta: siga as marcas de umidade.");
            InitializeInteractable(MakeCube("Channel", new Vector3(-1, 0.25f, 2), new Vector3(3.5f, 0.3f, 0.5f), NexoVisualTheme.Water, true), ClueId.Channel, "O canal aponta para o reservat?rio.");
            InitializeInteractable(MakeCube("Valve", new Vector3(2, 0.7f, 1), new Vector3(0.8f, 1.2f, 0.8f), NexoVisualTheme.Signal, true), ClueId.Valve, "Alinhe entrada e sa?da; h? tr?s posi??es.");
            string initialPortalName = activeStage == 1 ? "GreenDoor" : (activeStage == 2 ? "EnergyPortal" : "LogicPortal");
            Color initialPortalColor = activeStage == 1 ? new Color(0.18f, 0.85f, 0.42f) : (activeStage == 2 ? new Color(0.2f, 0.65f, 1f) : new Color(0.68f, 0.32f, 1f));
            var greenDoor = MakeCube(initialPortalName, new Vector3(6, 1.2f, -1), new Vector3(1.2f, 2.4f, 0.35f), initialPortalColor, true);
            if (greenDoor != null)
            {
                challengeDoorTransform = greenDoor.transform;
                challengeDoorRenderer = greenDoor.GetComponent<Renderer>();
                activeChallengeDoor = greenDoor.AddComponent<NexoChallengeDoor>();
                if (activeChallengeDoor != null) activeChallengeDoor.stage = activeStage;
                else Debug.LogWarning("Nexo: porta verde sem componente de desafio.");
            }
            MakeCube("Reservoir", new Vector3(4, 1, 3), new Vector3(3, 2, 2), NexoVisualTheme.Stone, true);
            MakeCube("ExitBridge", new Vector3(7, 0, 3), new Vector3(4, 0.25f, 2), NexoVisualTheme.Wood);
            GameObject avatar = MakeCube("Explorer", new Vector3(-6, 1, -5), new Vector3(0.7f, 1.4f, 0.7f), NexoVisualTheme.Signal);
            if (avatar == null)
            {
                Debug.LogError("Nexo: avatar n?o p?de ser criado; gameplay continuar? sem movimento.");
                return;
            }
            playerTransform = avatar.transform;
            if (view != null)
            {
                view.transform.position = new Vector3(-6, 6, -10);
                view.transform.LookAt(avatar.transform);
            }
        }

        private static void EnsureGameplayLight()
        {
            var lights = FindObjectsByType<Light>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            for (var i = 0; i < lights.Length; i++)
                if (lights[i] != null && lights[i].type == LightType.Directional) return;
            var lightObject = new GameObject("DynamicDirectionalLight");
            if (lightObject == null) return;
            var light = lightObject.AddComponent<Light>();
            if (light == null) return;
            light.type = LightType.Directional;
            light.color = Color.white;
            light.intensity = 1.2f;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        }

        private static Mesh CreateCubeMesh()
        {
            var mesh = new Mesh { name = "Nexo Visual Cube Mesh" };
            mesh.vertices = new[]
            {
                new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-0.5f, 0.5f, 0.5f)
            };
            mesh.triangles = new[] { 0, 2, 1, 0, 3, 2, 4, 5, 6, 4, 6, 7, 0, 1, 5, 0, 5, 4, 2, 3, 7, 2, 7, 6, 0, 4, 7, 0, 7, 3, 1, 2, 6, 1, 6, 5 };
            mesh.RecalculateNormals();
            return mesh;
        }

        private static void InitializeInteractable(GameObject item, ClueId clue, string description)
        {
            if (item == null) return;
            var interactable = item.GetComponent<NexoInteractable>();
            if (interactable == null) interactable = item.AddComponent<NexoInteractable>();
            if (interactable != null) interactable.Initialize(clue, description);
        }

        private GameObject MakeCube(string name, Vector3 position, Vector3 scale, Color color, bool addCollider = false)
        {
            var item = new GameObject(name);
            if (item == null) return null;
            item.name = name;
            if (item.transform == null) return item;
            item.transform.position = position;
            item.transform.localScale = scale;
            var filter = item.AddComponent<MeshFilter>();
            var mesh = CreateCubeMesh();
            if (filter != null && mesh != null) filter.sharedMesh = mesh;
            else Debug.LogError("Nexo: MeshFilter ou malha n?o p?de ser criado para " + name);
            var renderer = item.AddComponent<MeshRenderer>();
            if (renderer != null)
            {
                if (defaultWorldMaterial != null) renderer.sharedMaterial = new Material(defaultWorldMaterial);
                if (renderer.sharedMaterial != null) renderer.sharedMaterial.color = color;
            }
            else Debug.LogError("Nexo: MeshRenderer n?o p?de ser criado para " + name);
            if (addCollider && item.AddComponent<BoxCollider>() == null)
                Debug.LogError("Nexo: BoxCollider n?o p?de ser criado para " + name);
            return item;
        }

        private void Update()
        {
            if (showMenu || showIntro)
            {
                introElapsed += Time.unscaledDeltaTime;
                if (modernUiReady && introGroup != null)
                    introGroup.alpha = Mathf.Lerp(0.86f, 1f, Mathf.Clamp01(introElapsed / 0.6f));
                if (modernUiReady && introTitle != null)
                {
                    float glow = 0.5f + 0.5f * Mathf.Sin(Time.unscaledTime * 2.4f);
                    introTitle.color = Color.Lerp(Color.white, new Color(0.42f, 0.86f, 1f, 1f), glow);
                }
            }
            if (mission == null || playerTransform == null) return;
            if (challengeOpen) return;
            if (showMenu || showDiary || showSettings) return;
            if (mission.StateMachine.State == GameState.Paused) return;
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            input += new Vector3(touchMove.x, 0, touchMove.y);
            input = Vector3.ClampMagnitude(input, 1f);
            playerTransform.position += input * 3f * Time.deltaTime;
            view.transform.position = Vector3.Lerp(view.transform.position, playerTransform.position + new Vector3(0, 5, -8), 8f * Time.deltaTime);
            view.transform.LookAt(playerTransform.position + Vector3.up);
            if (Input.GetKeyDown(KeyCode.E)) Interact();
        }

        private void Interact()
        {
            if (view == null) return;
            Ray ray = view.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 4f))
            {
                if (hit.collider == null) return;
                NexoChallengeDoor door = hit.collider.GetComponent<NexoChallengeDoor>();
                NexoInteractable target = hit.collider.GetComponent<NexoInteractable>();
                if (door != null && door.unlocked && door.stage == activeStage)
                {
                    OpenChallenge();
                }
                else if (target != null)
                {
                    mission.RecordClue(target.clue);
                    prompt = target.description;
                }
                else if (hit.collider.name == "Reservoir") mission.Allocate(WaterAllocation.Reservoir);
            }
        }

        private void OpenChallenge()
        {
            if (challengeOpen || mission == null || mission.StateMachine.State != GameState.Exploring) return;
            challengeOpen = true;
            challengeAnswered = false;
            challengeCorrect = false;
            challengeFeedback = GetChallengePrompt();
            if (challengeDoorTransform != null)
            {
                if (challengePulse != null) StopCoroutine(challengePulse);
                challengePulse = StartCoroutine(PulseTransform(challengeDoorTransform, 0.12f));
            }
        }

        private void ResolveChallenge(int answer)
        {
            if (!challengeOpen || challengeAnswered) return;
            challengeAnswered = true;
            challengeCorrect = answer == GetExpectedAnswer();
            if (challengeCorrect)
            {
                score++;
                if (score > highScore)
                {
                    highScore = score;
                    PlayerPrefs.SetInt(HighScoreKey, highScore);
                    PlayerPrefs.Save();
                }
                challengeFeedback = GetSuccessFeedback();
                if (challengeDoorRenderer != null && challengeDoorRenderer.material != null)
                    challengeDoorRenderer.material.color = new Color(0.35f, 1f, 0.58f);
                if (challengeDoorTransform != null)
                    challengePulse = StartCoroutine(PulseTransform(challengeDoorTransform, 0.2f));
                SpawnSuccessParticles(challengeDoorTransform == null ? Vector3.zero : challengeDoorTransform.position);
                UnlockNextStage();
            }
            else
            {
                challengeFeedback = "Ainda nao. Repare que duas unidades ficam no canal antes de chegar ao reservatorio.";
                if (challengeDoorTransform != null)
                {
                    challengePulse = StartCoroutine(PulseTransform(challengeDoorTransform, 0.08f));
                    StartCoroutine(ShakeTransform(challengeDoorTransform));
                }
            }
        }

        private string GetChallengePrompt()
        {
            if (activeStage == 2) return "Energia: 3 unidades sao produzidas e 1 e perdida no cabo. Quantas chegam?";
            if (activeStage == 3) return "Logica: a sequencia dobra a cada passo: 2, 4, 8. Qual e o proximo numero?";
            return "Fluxo: 6 unidades saem da fonte e 2 ficam no canal. Quantas chegam?";
        }

        private int GetExpectedAnswer()
        {
            if (activeStage == 2) return 2;
            if (activeStage == 3) return 16;
            return 4;
        }

        private int[] GetChallengeOptions()
        {
            if (activeStage == 2) return new[] { 2, 3, 4 };
            if (activeStage == 3) return new[] { 16, 8, 12 };
            return new[] { 4, 6, 8 };
        }

        private string GetSuccessFeedback()
        {
            if (activeStage == 2) return "Acerto! 3 - 1 = 2. Voce encontrou a perda de energia.";
            if (activeStage == 3) return "Acerto! 2, 4, 8, 16. Voce reconheceu a regra da sequencia.";
            return "Acerto! 6 - 2 = 4. Voce identificou a perda no canal.";
        }

        private void UnlockNextStage()
        {
            if (activeChallengeDoor != null) activeChallengeDoor.unlocked = false;
            if (activeStage >= 3)
            {
                challengeFeedback = challengeFeedback + " Todas as fases iniciais foram restauradas. O arquipelago pode avancar.";
                return;
            }
            activeStage++;
            PlayerPrefs.SetInt(StageKey, activeStage);
            PlayerPrefs.Save();
            Color portalColor = activeStage == 2 ? new Color(0.2f, 0.65f, 1f) : new Color(0.68f, 0.32f, 1f);
            string portalName = activeStage == 2 ? "EnergyPortal" : "LogicPortal";
            var portal = MakeCube(portalName, new Vector3(6 + activeStage, 1.2f, -1), new Vector3(1.2f, 2.4f, 0.35f), portalColor, true);
            if (portal != null)
            {
                challengeDoorTransform = portal.transform;
                challengeDoorRenderer = portal.GetComponent<Renderer>();
                activeChallengeDoor = portal.AddComponent<NexoChallengeDoor>();
                if (activeChallengeDoor != null) activeChallengeDoor.stage = activeStage;
                challengeFeedback = challengeFeedback + (activeStage == 2 ? " Novo portal: eficiencia energetica." : " Novo portal: logica de programacao.");
            }
        }

        private IEnumerator PulseTransform(Transform target, float amount)
        {
            if (target == null) yield break;
            Vector3 original = target.localScale;
            float elapsed = 0f;
            while (elapsed < 0.32f)
            {
                elapsed += Time.unscaledDeltaTime;
                float wave = Mathf.Sin(Mathf.Clamp01(elapsed / 0.32f) * Mathf.PI);
                target.localScale = original * (1f + wave * amount);
                yield return null;
            }
            target.localScale = original;
            challengePulse = null;
        }

        private IEnumerator ShakeTransform(Transform target)
        {
            if (target == null) yield break;
            Vector3 original = target.localPosition;
            float elapsed = 0f;
            while (elapsed < 0.22f)
            {
                elapsed += Time.unscaledDeltaTime;
                float strength = 0.08f * (1f - Mathf.Clamp01(elapsed / 0.22f));
                target.localPosition = original + Vector3.right * Mathf.Sin(elapsed * 95f) * strength;
                yield return null;
            }
            target.localPosition = original;
        }

        private void SpawnSuccessParticles(Vector3 position)
        {
            Shader shader = defaultWorldMaterial == null ? null : defaultWorldMaterial.shader;
            if (shader == null) return;
            var particleObject = new GameObject("Nexo Challenge Success FX");
            if (particleObject == null) return;
            particleObject.transform.position = position;
            var particles = particleObject.AddComponent<ParticleSystem>();
            var main = particles.main;
            main.duration = 0.8f;
            main.startLifetime = new ParticleSystem.MinMaxCurve(0.45f, 0.9f);
            main.startSpeed = new ParticleSystem.MinMaxCurve(0.4f, 1.4f);
            main.startSize = new ParticleSystem.MinMaxCurve(0.06f, 0.14f);
            main.startColor = new Color(0.35f, 1f, 0.62f, 1f);
            main.maxParticles = 18;
            main.loop = false;
            var emission = particles.emission;
            emission.enabled = false;
            var particleRenderer = particleObject.GetComponent<ParticleSystemRenderer>();
            if (particleRenderer != null)
            {
                particleRenderer.material = new Material(shader);
                if (particleRenderer.material != null) particleRenderer.material.color = new Color(0.35f, 1f, 0.62f, 1f);
            }
            particles.Emit(12);
            Destroy(particleObject, 1.2f);
        }

        private void OnGUI()
        {
            if (mission == null) return;
            if (modernUiReady && (showMenu || showIntro)) return;
            Rect safe = Screen.safeArea;
            if (challengeOpen)
            {
                DrawChallenge(safe);
                return;
            }
            GUI.Label(new Rect(safe.x + 16, safe.y + 48, 360, 30), "Pontuacao: " + score + " | Recorde: " + highScore + " | Fase: " + activeStage + "/3");
            if (showMenu)
            {
                GUI.Box(new Rect(safe.x + 24, safe.y + 40, safe.width - 48, 220), "NEXO");
                GUI.Label(new Rect(safe.x + 55, safe.y + 90, safe.width - 110, 60), "O arquipélago perdeu a conexão. Restaure a primeira ilha.");
                if (GUI.Button(new Rect(safe.x + 55, safe.y + 165, 190, 44), "Iniciar missão")) showMenu = false;
                if (GUI.Button(new Rect(safe.x + 255, safe.y + 165, 190, 44), "Configurações")) showSettings = true;
                if (showSettings) DrawSettings(safe);
                return;
            }
            GUI.Label(new Rect(safe.x + 16, safe.y + 16, safe.width - 32, 32), "NEXO | " + mission.StateMachine.State + " | Água: " + mission.Save.activeMission.water);
            if (showIntro)
            {
                float introAlpha = Mathf.Clamp01(introElapsed / 0.45f);
                GUI.color = new Color(1f, 1f, 1f, introAlpha);
                GUI.Box(new Rect(safe.x + 30, safe.y + 70, safe.width - 60, 180), "O reservatório silencioso");
                GUI.Label(new Rect(safe.x + 55, safe.y + 110, safe.width - 110, 60), "Observe a fonte, siga o canal e restaure o fluxo.");
                if (GUI.Button(new Rect(safe.x + 55, safe.y + 180, 180, 42), "Começar (pular prólogo)")) { mission.SkipIntro(); showIntro = false; introElapsed = 0f; EnsureWorldBuilt(); audioDirector.PlayDiscovery(); }
                GUI.color = Color.white;
                return;
            }
            if (GUI.Button(new Rect(safe.x + 16, safe.yMax - 62, 70, 44), "Pausa")) { mission.Pause(); }
            if (GUI.Button(new Rect(safe.xMax - 180, safe.yMax - 62, 70, 44), "Interagir")) Interact();
            if (GUI.Button(new Rect(safe.xMax - 95, safe.yMax - 62, 70, 44), "Ajuda")) prompt = "Compare as marcas do canal e da válvula.";
            if (GUI.Button(new Rect(safe.x + 96, safe.yMax - 62, 70, 44), "Diário")) showDiary = true;
            if (!string.IsNullOrEmpty(prompt)) GUI.Box(new Rect(safe.x + 16, safe.y + 55, safe.width - 32, 55), prompt);
            if (showDiary) DrawDiary(safe);
            if (mission.Save.activeMission.discoveredClueIds != null &&
                System.Array.IndexOf(mission.Save.activeMission.discoveredClueIds, ClueId.Valve.ToString()) >= 0)
            {
                GUI.Label(new Rect(16, 118, 300, 28), "Válvula: compare os marcadores");
                if (GUI.Button(new Rect(16, 148, 70, 36), "Pos. 1")) mission.EvaluateValve(0);
                if (GUI.Button(new Rect(92, 148, 70, 36), "Pos. 2")) mission.EvaluateValve(1);
                if (GUI.Button(new Rect(168, 148, 70, 36), "Pos. 3")) mission.EvaluateValve(2);
            }
            if (mission.Save.activeMission.allocation == WaterAllocation.Reservoir &&
                mission.Save.activeMission.valveIndex == MissionRuleEngine.ReservoirValveIndex &&
                !mission.Save.activeMission.completed &&
                GUI.Button(new Rect(250, 148, 170, 36), "Ativar reservatório"))
            {
                mission.Complete();
            }
            if (mission.Save.activeMission.completed)
                GUI.Box(new Rect(30, 250, Screen.width - 60, 100), "Missão concluída — nova área desbloqueada: o sinal distante.");
            if (mission.Save.activeMission.completed && !transferAnswered && GUI.Button(new Rect(250, 360, 220, 36), "Demonstrar compreensao")) showTransfer = true;
            if (showTransfer) DrawTransfer(safe);
            if (mission.StateMachine.State == GameState.Feedback)
            {
                GUI.Box(new Rect(safe.x + 30, safe.y + safe.height / 2 - 70, safe.width - 60, 140), mission.LastFeedbackId);
                GUI.Label(new Rect(safe.x + 55, safe.y + safe.height / 2 - 25, safe.width - 110, 45), mission.LastExplanation);
                if (GUI.Button(new Rect(safe.x + 55, safe.y + safe.height / 2 + 25, 180, 40), "Continuar")) mission.StateMachine.TryTransition(GameSignal.InspectionFinished);
            }
            if (mission.StateMachine.State == GameState.Paused)
            {
                GUI.Box(new Rect(safe.x + 40, safe.y + 100, safe.width - 80, 160), "Pausado");
                if (GUI.Button(new Rect(safe.x + 70, safe.y + 160, 160, 42), "Retomar")) mission.Resume();
                if (GUI.Button(new Rect(safe.x + 250, safe.y + 160, 160, 42), "Sair e salvar")) { mission.Pause(); Application.Quit(); }
            }
        }

        private void DrawChallenge(Rect safe)
        {
            GUI.Box(new Rect(safe.x + 30, safe.y + 80, safe.width - 60, 300), "PORTA VERDE | DESAFIO RAPIDO");
            GUI.Label(new Rect(safe.x + 55, safe.y + 130, safe.width - 110, 70), challengeFeedback);
            if (!challengeAnswered)
            {
                int[] options = GetChallengeOptions();
                GUI.color = new Color(0.55f, 1f, 0.68f, 1f);
                if (GUI.Button(new Rect(safe.x + 55, safe.y + 215, safe.width - 110, 42), options[0] + " unidades")) ResolveChallenge(options[0]);
                GUI.color = new Color(1f, 0.72f, 0.48f, 1f);
                if (GUI.Button(new Rect(safe.x + 55, safe.y + 265, safe.width - 110, 42), options[1] + " unidades")) ResolveChallenge(options[1]);
                if (GUI.Button(new Rect(safe.x + 55, safe.y + 315, safe.width - 110, 42), options[2] + " unidades")) ResolveChallenge(options[2]);
                GUI.color = Color.white;
            }
            else
            {
                GUI.color = challengeCorrect ? new Color(0.45f, 1f, 0.62f, 1f) : new Color(1f, 0.38f, 0.38f, 1f);
                if (GUI.Button(new Rect(safe.x + 55, safe.y + 315, safe.width - 110, 42), challengeCorrect ? "Continuar" : "Tentar novamente"))
            {
                challengeOpen = false;
                challengeAnswered = false;
                challengeFeedback = "";
                GUI.color = Color.white;
            }
            GUI.color = Color.white;
            }
        }

        private void DrawDiary(Rect safe)
        {
            GUI.Box(new Rect(safe.x + 30, safe.y + 90, safe.width - 60, 210), "Diário de descobertas");
            string clues = string.Join(", ", mission.Save.activeMission.discoveredClueIds ?? new string[0]);
            GUI.Label(new Rect(safe.x + 55, safe.y + 135, safe.width - 110, 80), "Pistas encontradas: " + clues);
            GUI.Label(new Rect(safe.x + 55, safe.y + 205, safe.width - 110, 60), "Hipótese: a água segue uma rede que atravessa as ilhas.");
            if (GUI.Button(new Rect(safe.x + 55, safe.y + 255, 120, 36), "Fechar")) showDiary = false;
        }

        private bool HasTransferEvidence()
        {
            MasteryEvidence[] evidence = mission == null ? null : mission.Save.masteryEvidence;
            if (evidence == null) return false;
            for (int i = 0; i < evidence.Length; i++)
                if (evidence[i].objectiveId == VerticalSliceMission.TransferObjectiveId) return true;
            return false;
        }

        private void DrawTransfer(Rect safe)
        {
            GUI.Box(new Rect(safe.x + 25, safe.y + 105, safe.width - 50, 210), "Transferencia: rota de energia");
            GUI.Label(new Rect(safe.x + 45, safe.y + 145, safe.width - 90, 50), "Uma estacao tem 4 unidades e duas rotas. Qual escolha mantem o sinal?");
            if (GUI.Button(new Rect(safe.x + 45, safe.y + 215, safe.width - 90, 36), "Priorizar a rota critica e verificar a evidencia"))
            {
                mission.RecordTransfer(true); transferAnswered = true; showTransfer = false; prompt = "Resposta registrada: compare custo, efeito e evidencia.";
            }
            if (GUI.Button(new Rect(safe.x + 45, safe.y + 260, safe.width - 90, 36), "Escolher sem comparar as consequencias"))
            {
                mission.RecordTransfer(false); transferAnswered = true; showTransfer = false; prompt = "Tentativa registrada. Reveja custo, efeito e evidencia.";
            }
        }

        private void DrawSettings(Rect safe)
        {
                GUI.Box(new Rect(safe.x + 40, safe.y + 60, safe.width - 80, 390), "Configurações");
            GUI.Label(new Rect(safe.x + 70, safe.y + 105, 250, 32), "Áudio: " + (mission.Save.audio.muted ? "silenciado" : "ativo"));
            if (GUI.Button(new Rect(safe.x + 70, safe.y + 145, 190, 40), mission.Save.audio.muted ? "Ativar áudio" : "Silenciar áudio"))
            {
                mission.Save.audio.muted = !mission.Save.audio.muted;
                audioDirector.Configure(mission.Save.audio);
            }
            GUI.Label(new Rect(safe.x + 70, safe.y + 205, 400, 32), "Legendas: sempre disponíveis");
            if (!confirmDelete && GUI.Button(new Rect(safe.x + 220, safe.y + 255, 190, 36), "Apagar dados locais")) confirmDelete = true;
            if (confirmDelete)
            {
                GUI.Label(new Rect(safe.x + 70, safe.y + 290, safe.width - 120, 30), "Isso remove o save e o backup deste dispositivo.");
                if (GUI.Button(new Rect(safe.x + 70, safe.y + 325, 120, 36), "Confirmar"))
                {
                    dataControls.DeleteAll();
                    PlayerPrefs.DeleteKey(HighScoreKey);
                    PlayerPrefs.DeleteKey(StageKey);
                    PlayerPrefs.Save();
                    score = 0;
                    highScore = 0;
                    activeStage = 1;
                    mission = new VerticalSliceMission(new JsonFileSaveStore());
                    mission.Initialize();
                    showIntro = true;
                    confirmDelete = false;
                    showSettings = false;
                    showMenu = true;
                }
                if (GUI.Button(new Rect(safe.x + 205, safe.y + 325, 120, 36), "Cancelar")) confirmDelete = false;
            }
            if (GUI.Button(new Rect(safe.x + 70, safe.y + 255, 120, 36), "Fechar")) showSettings = false;
        }
    }

    public sealed class NexoChallengeDoor : MonoBehaviour
    {
        public int stage = 1;
        public bool unlocked = true;
    }

    public sealed class NexoInteractable : MonoBehaviour
    {
        public ClueId clue;
        public string description;
        public void Initialize(ClueId value, string text) { clue = value; description = text; }
    }
}
