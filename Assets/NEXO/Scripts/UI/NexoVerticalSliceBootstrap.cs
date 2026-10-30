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

        public GameState CurrentState { get { return mission == null ? GameState.Initializing : mission.StateMachine.State; } }
        public bool GameplayMaterialReady { get { return defaultWorldMaterial != null && defaultWorldMaterial.shader != null; } }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateIfMissing()
        {
            if (FindFirstObjectByType<NexoVerticalSliceBootstrap>() == null)
                new GameObject("Nexo Vertical Slice Runtime").AddComponent<NexoVerticalSliceBootstrap>();
        }

        private void Start()
        {
            PreloadGraphics();
            EnsureMobileCanvas();
            EnsurePresentationCamera();
            BuildIntroParticles();
            mission = new VerticalSliceMission(new JsonFileSaveStore());
            mission.Initialize();
            audioDirector = gameObject.AddComponent<NexoAudioDirector>();
            audioDirector.Configure(mission.Save.audio);
            dataControls = new LocalDataControls();
            showIntro = mission.StateMachine.State == GameState.Intro;
            introElapsed = 0f;
            transferAnswered = HasTransferEvidence();
            RefreshModernIntro();
            if (modernUiReady) StartCoroutine(FadeIntroIn());
            // Keep the intro path UI-only. Build gameplay physics only after entry.
            if (!showIntro) EnsureWorldBuilt();
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
            if (resourceMaterial != null && resourceMaterial.shader == null && worldShader != null)
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
            MakeCube("Source", new Vector3(-5, 0.4f, 4), new Vector3(1.4f, 0.8f, 1.4f), NexoVisualTheme.Water, true).AddComponent<NexoInteractable>().Initialize(ClueId.Source, "Fonte alta: siga as marcas de umidade.");
            MakeCube("Channel", new Vector3(-1, 0.25f, 2), new Vector3(3.5f, 0.3f, 0.5f), NexoVisualTheme.Water, true).AddComponent<NexoInteractable>().Initialize(ClueId.Channel, "O canal aponta para o reservatório.");
            MakeCube("Valve", new Vector3(2, 0.7f, 1), new Vector3(0.8f, 1.2f, 0.8f), NexoVisualTheme.Signal, true).AddComponent<NexoInteractable>().Initialize(ClueId.Valve, "Alinhe entrada e saída; há três posições.");
            MakeCube("Reservoir", new Vector3(4, 1, 3), new Vector3(3, 2, 2), NexoVisualTheme.Stone, true);
            MakeCube("ExitBridge", new Vector3(7, 0, 3), new Vector3(4, 0.25f, 2), NexoVisualTheme.Wood);
            GameObject avatar = MakeCube("Explorer", new Vector3(-6, 1, -5), new Vector3(0.7f, 1.4f, 0.7f), NexoVisualTheme.Signal);
            playerTransform = avatar.transform;
            view.transform.position = new Vector3(-6, 6, -10);
            view.transform.LookAt(avatar.transform);
        }

        private static void EnsureGameplayLight()
        {
            var lights = FindObjectsByType<Light>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            for (var i = 0; i < lights.Length; i++)
                if (lights[i] != null && lights[i].type == LightType.Directional) return;
            var lightObject = new GameObject("DynamicDirectionalLight");
            var light = lightObject.AddComponent<Light>();
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

        private GameObject MakeCube(string name, Vector3 position, Vector3 scale, Color color, bool addCollider = false)
        {
            GameObject item = new GameObject(name);
            item.name = name; item.transform.position = position; item.transform.localScale = scale;
            var filter = item.AddComponent<MeshFilter>();
            filter.sharedMesh = CreateCubeMesh();
            var renderer = item.AddComponent<MeshRenderer>();
            if (defaultWorldMaterial != null) renderer.sharedMaterial = new Material(defaultWorldMaterial);
            if (renderer.sharedMaterial != null) renderer.sharedMaterial.color = color;
            if (addCollider) item.AddComponent<BoxCollider>();
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
                NexoInteractable target = hit.collider.GetComponent<NexoInteractable>();
                if (target != null)
                {
                    mission.RecordClue(target.clue);
                    prompt = target.description;
                }
                else if (hit.collider.name == "Reservoir") mission.Allocate(WaterAllocation.Reservoir);
            }
        }

        private void OnGUI()
        {
            if (mission == null) return;
            if (modernUiReady && (showMenu || showIntro)) return;
            Rect safe = Screen.safeArea;
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

    public sealed class NexoInteractable : MonoBehaviour
    {
        public ClueId clue;
        public string description;
        public void Initialize(ClueId value, string text) { clue = value; description = text; }
    }
}
