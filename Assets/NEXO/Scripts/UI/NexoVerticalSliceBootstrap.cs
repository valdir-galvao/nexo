using System.Collections;
using System.Collections.Generic;
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
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif

namespace Nexo.UI
{
    /// <summary>Bootstrap temporÃ¡rio: cria uma ilha jogÃ¡vel sem depender de prefabs.</summary>
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
        private NexoScenePresentation scenePresentation;
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
        private GameObject gameplayHudRoot;
        private Text gameplayHudStatus;
        private Text gameplayHudObjective;
        private Text gameplayHudPrompt;
        private Text gameplayHudStage;
        private Button gameplayInteractButton;
        private GameObject gameplayControlsRoot;
        private GameObject gameplayGuidePanel;
        private Text gameplayGuideText;
        private Button gameplayGuideDismissButton;
        private bool gameplayGuideVisible;
        private float gameplayGuideTime;
        private RectTransform introButtonRect;
        private ParticleSystem introParticles;
        private bool modernUiReady;
        private Coroutine introTransition;
        private Material defaultWorldMaterial;
        private float timeScaleBeforePause = 1f;
        private Transform challengeDoorTransform;
        private Renderer challengeDoorRenderer;
        private Color challengeDoorBaseColor = Color.white;
        private Vector3 challengeDoorBaseScale = Vector3.one;
        private float gameplayPulseTime;
        private float gameplayWorldTime;
        private float cameraRevealTime;
        private Transform channelTransform;
        private Vector3 channelBaseScale = Vector3.one;
        private Transform avatarTransform;
        private Transform avatarBodyTransform;
        private Transform avatarHeadTransform;
        private Transform avatarLeftLegTransform;
        private Transform avatarRightLegTransform;
        private Light gameplayLight;
        private Light portalLight;
        private ParticleSystem portalParticles;
        private Transform sourceTransform;
        private Transform valveTransform;
        private Transform objectiveTarget;
        private GameObject objectiveMarker;
        private readonly List<Transform> waterRipples = new List<Transform>();
        private Transform waterSurfaceTransform;
        private Vector3 waterSurfaceBaseScale = Vector3.one;
        private Renderer waterSurfaceRenderer;
        private Color waterSurfaceBaseColor = Color.cyan;
        private readonly List<Transform> landscapeDecor = new List<Transform>();
        private int objectiveStep;
        private Color valveBaseColor = Color.white;
        private bool challengeOpen;
        private bool challengeAnswered;
        private bool challengeCorrect;
        private bool missionTransitionRequested;
        private int score;
        private string challengeFeedback = "";
        private Coroutine challengePulse;
        private GameObject challengeCanvasPanel;
        private Text challengeCanvasPrompt;
        private Button[] challengeCanvasButtons;
        private RectTransform uiSafeArea;
        private NexoChallengeDoor activeChallengeDoor;
        private int activeStage = 1;
        private int highScore;
        private const string HighScoreKey = "NEXO_High_Score";
        private const string StageKey = "NEXO_Unlocked_Stage";

        public GameState CurrentState { get { return mission == null ? GameState.Initializing : mission.StateMachine.State; } }
        public bool GameplayMaterialReady { get { return defaultWorldMaterial != null && defaultWorldMaterial.shader != null; } }

        private void Awake()
        {
            Debug.Log("[NEXO_BOOT] Awake: bootstrap instance created.");
            EnsureRuntimeEventSystem();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateIfMissing()
        {
            try
            {
                EnsureRuntimeEventSystem();
                if (FindFirstObjectByType<NexoVerticalSliceBootstrap>() == null)
                {
                    var runtimeObject = new GameObject("Nexo Vertical Slice Runtime");
                    if (runtimeObject != null && runtimeObject.AddComponent<NexoVerticalSliceBootstrap>() == null)
                        Debug.LogError("Nexo: n?o foi poss?vel criar o componente de bootstrap.");
                }
            }
            catch (System.Exception exception) { Debug.LogException(exception); }
        }

        private static void EnsureRuntimeEventSystem()
        {
            try
            {
                var eventSystem = Object.FindAnyObjectByType<EventSystem>();
                if (eventSystem == null)
                {
                    var eventSystemObject = new GameObject("Nexo EventSystem");
                    if (eventSystemObject == null) return;
                    eventSystem = eventSystemObject.AddComponent<EventSystem>();
                }
                if (eventSystem == null) return;
                eventSystem.gameObject.SetActive(true);
                ConfigureEventSystemInput(eventSystem);
            }
            catch (System.Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private void Start()
        {
            try
            {
                Debug.Log("[NEXO_BOOT] Start: initializing camera, light, Canvas and mission.");
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
            NexoQualityProfile.ApplyAutomatic();
            ConfigureMobileRuntime();
            PreloadGraphics();
            EnsurePresentationCamera();
            EnsureGameplayLight();
            EnsureMobileCanvas();
            BuildIntroParticles();
            highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
            activeStage = Mathf.Clamp(PlayerPrefs.GetInt(StageKey, 1), 1, 3);
            mission = new VerticalSliceMission(new JsonFileSaveStore());
            mission.Initialize();
            // Initialize the gameplay manager and world on the first executable
            // frame. The intro UI remains on top, but the interactive portals,
            // score state and raycast targets are already available underneath.
            EnsureWorldBuilt();
            scenePresentation = gameObject.GetComponent<NexoScenePresentation>();
            if (scenePresentation == null) scenePresentation = gameObject.AddComponent<NexoScenePresentation>();
            if (scenePresentation != null) scenePresentation.Initialize();
            audioDirector = gameObject.AddComponent<NexoAudioDirector>();
            if (audioDirector != null) audioDirector.Configure(mission.Save != null ? mission.Save.audio : null);
            dataControls = new LocalDataControls();
            showIntro = mission.StateMachine.State == GameState.Intro;
            introElapsed = 0f;
            transferAnswered = HasTransferEvidence();
            EnsureInteractiveHudVisible();
            RefreshModernIntro();
            if (modernUiReady) StartCoroutine(FadeIntroIn());
        }

        private void EnsureInteractiveHudVisible()
        {
            if (uiCanvas == null) return;
            uiCanvas.gameObject.SetActive(true);
            uiCanvas.enabled = true;
            uiCanvas.transform.SetAsLastSibling();
            if (uiSafeArea != null)
            {
                uiSafeArea.gameObject.SetActive(true);
                uiSafeArea.SetAsLastSibling();
            }
            var raycaster = uiCanvas.GetComponent<GraphicRaycaster>();
            if (raycaster != null) raycaster.enabled = true;
            if (introGroup != null)
            {
                introGroup.gameObject.SetActive(showMenu || showIntro);
                introGroup.interactable = showMenu || showIntro;
                introGroup.blocksRaycasts = showMenu || showIntro;
            }
        }

        private void EnsurePresentationCameraSafe()
        {
            try
            {
                if (view == null) view = Camera.main;
                if (view == null)
                {
                    var cameraObject = new GameObject("Nexo Safe Camera");
                    if (cameraObject != null)
                    {
                        cameraObject.tag = "MainCamera";
                        view = cameraObject.AddComponent<Camera>();
                    }
                }
                if (view != null)
                {
                    view.transform.position = new Vector3(0f, 0f, -10f);
                    view.transform.LookAt(Vector3.zero);
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
                    if (audioDirector != null) audioDirector.SetPaused(true);
                }
                else
                {
                    Time.timeScale = timeScaleBeforePause > 0f ? timeScaleBeforePause : 1f;
                    if (introParticles != null && (showIntro || showMenu)) introParticles.Play(true);
                    if (audioDirector != null) audioDirector.SetPaused(false);
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
                    if (audioDirector != null) audioDirector.SetPaused(true);
                }
                else
                {
                    if (introParticles != null && (showIntro || showMenu) && Time.timeScale > 0f)
                        introParticles.Play(true);
                    if (audioDirector != null) audioDirector.SetPaused(false);
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

            // Prefer shaders guaranteed by the built-in mobile path. URP remains
            // supported, but is never required for the procedural world.
            Shader worldShader = Shader.Find("Unlit/Color");
            if (worldShader == null) worldShader = Shader.Find("UI/Default");
            if (worldShader == null) worldShader = Shader.Find("Universal Render Pipeline/Unlit");
            if (worldShader == null) worldShader = Shader.Find("Sprites/Default");
            Shader fallbackShader = Shader.Find("UI/Default");
            if (fallbackShader == null) fallbackShader = Shader.Find("Unlit/Color");
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
                canvasObject.AddComponent<GraphicRaycaster>();
            }

            uiCanvas = canvas;
            // Bind the runtime canvas to the guaranteed boot camera. This keeps
            // procedural UI visible on mobile while retaining an explicit camera
            // reference for render ordering and diagnostics.
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = view != null ? view : Camera.main;
            if (canvas.worldCamera == null)
            {
                // A missing camera must never make the HUD disappear. Overlay is
                // the safe last-resort path for emulators with incomplete EGL.
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            canvas.planeDistance = 10f;
            canvas.gameObject.SetActive(true);
            canvas.transform.SetAsLastSibling();
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
            canvas.sortingOrder = 10;
            EnsureSafeAreaRoot(canvas);
            var canvasRaycaster = canvas.GetComponent<GraphicRaycaster>();
            if (canvasRaycaster == null) canvasRaycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
            if (canvasRaycaster != null) canvasRaycaster.enabled = true;
            EnsureEventSystem();
            BuildModernIntro(canvas);
            BuildGameplayHud(canvas);
            BuildChallengeCanvas(canvas);
        }

        private void EnsureSafeAreaRoot(Canvas canvas)
        {
            if (canvas == null) return;
            if (uiSafeArea == null)
            {
                var safeObject = new GameObject("Nexo Safe Area UI");
                safeObject.transform.SetParent(canvas.transform, false);
                uiSafeArea = safeObject.AddComponent<RectTransform>();
            }
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            if (uiSafeArea == null || Screen.width <= 0 || Screen.height <= 0) return;
            Rect safe = Screen.safeArea;
            uiSafeArea.anchorMin = new Vector2(safe.x / Screen.width, safe.y / Screen.height);
            uiSafeArea.anchorMax = new Vector2(safe.xMax / Screen.width, safe.yMax / Screen.height);
            uiSafeArea.offsetMin = Vector2.zero;
            uiSafeArea.offsetMax = Vector2.zero;
        }

        private void EnsureEventSystem()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null)
            {
                var eventSystemObject = new GameObject("Nexo UI EventSystem");
                eventSystem = eventSystemObject.AddComponent<EventSystem>();
            }
            if (eventSystem == null) return;
            eventSystem.gameObject.SetActive(true);
            ConfigureEventSystemInput(eventSystem);
        }

        private static void ConfigureEventSystemInput(EventSystem eventSystem)
        {
            if (eventSystem == null) return;
            var legacy = eventSystem.GetComponent<StandaloneInputModule>();
            if (legacy == null) legacy = eventSystem.gameObject.AddComponent<StandaloneInputModule>();

#if ENABLE_INPUT_SYSTEM
            var modern = eventSystem.GetComponent<InputSystemUIInputModule>();
            if (modern == null) modern = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            if (modern != null)
            {
                modern.enabled = true;
                if (legacy != null) legacy.enabled = false;
            }
            else if (legacy != null) legacy.enabled = true;
#else
            if (legacy != null) legacy.enabled = true;
#endif
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
            if (view == null) return;
            view.transform.position = new Vector3(0f, 0f, -10f);
            view.transform.LookAt(Vector3.zero);
            view.clearFlags = CameraClearFlags.SolidColor;
            view.backgroundColor = new Color(0.025f, 0.055f, 0.12f, 1f);
            view.enabled = true;
            var physicsRaycaster = view.GetComponent<PhysicsRaycaster>();
            if (physicsRaycaster == null) physicsRaycaster = view.gameObject.AddComponent<PhysicsRaycaster>();
            if (physicsRaycaster != null) physicsRaycaster.enabled = true;
        }

        private void BuildModernIntro(Canvas canvas)
        {
            if (canvas == null || introGroup != null) return;
            var root = new GameObject("Nexo Intro Design System");
            root.transform.SetParent(uiSafeArea == null ? canvas.transform : uiSafeArea, false);
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

        private void BuildChallengeCanvas(Canvas canvas)
        {
            if (canvas == null || challengeCanvasPanel != null) return;
            challengeCanvasPanel = new GameObject("Nexo Challenge Canvas Panel");
            challengeCanvasPanel.transform.SetParent(uiSafeArea == null ? canvas.transform : uiSafeArea, false);
            var panelRect = challengeCanvasPanel.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.06f, 0.10f);
            panelRect.anchorMax = new Vector2(0.94f, 0.90f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;
            var panelImage = challengeCanvasPanel.AddComponent<Image>();
            if (panelImage != null) panelImage.color = new Color(0.03f, 0.08f, 0.16f, 0.98f);
            challengeCanvasPrompt = CreateIntroText(challengeCanvasPanel.transform, "", 30, FontStyle.Bold, new Vector2(0.08f, 0.58f), new Vector2(0.92f, 0.88f));
            if (challengeCanvasPrompt != null) challengeCanvasPrompt.color = NexoVisualTheme.SafeText;
            challengeCanvasButtons = new Button[3];
            for (int i = 0; i < challengeCanvasButtons.Length; i++)
            {
                var buttonObject = new GameObject("Challenge Answer " + i);
                buttonObject.transform.SetParent(challengeCanvasPanel.transform, false);
                var buttonRect = buttonObject.AddComponent<RectTransform>();
                buttonRect.anchorMin = new Vector2(0.14f, 0.12f + i * 0.14f);
                buttonRect.anchorMax = new Vector2(0.86f, 0.23f + i * 0.14f);
                buttonRect.offsetMin = Vector2.zero;
                buttonRect.offsetMax = Vector2.zero;
                var image = buttonObject.AddComponent<Image>();
                if (image != null) image.color = new Color(0.10f, 0.42f, 0.68f, 1f);
                var button = buttonObject.AddComponent<Button>();
                if (button != null)
                {
                    button.targetGraphic = image;
                    button.interactable = true;
                    challengeCanvasButtons[i] = button;
                }
                var label = CreateIntroText(buttonObject.transform, "", 26, FontStyle.Bold, Vector2.zero, Vector2.one);
                if (label != null) label.alignment = TextAnchor.MiddleCenter;
            }
            challengeCanvasPanel.SetActive(false);
        }

        private void BuildGameplayHud(Canvas canvas)
        {
            if (canvas == null || gameplayHudRoot != null) return;
            gameplayHudRoot = new GameObject("Nexo Gameplay HUD");
            gameplayHudRoot.transform.SetParent(uiSafeArea == null ? canvas.transform : uiSafeArea, false);
            var rootRect = gameplayHudRoot.AddComponent<RectTransform>();
            rootRect.anchorMin = Vector2.zero;
            rootRect.anchorMax = Vector2.one;
            rootRect.offsetMin = Vector2.zero;
            rootRect.offsetMax = Vector2.zero;

            gameplayHudStatus = CreateIntroText(gameplayHudRoot.transform, "NEXO", 28, FontStyle.Bold,
                new Vector2(0.04f, 0.91f), new Vector2(0.96f, 0.99f));
            if (gameplayHudStatus != null)
            {
                gameplayHudStatus.alignment = TextAnchor.UpperLeft;
                gameplayHudStatus.color = new Color(0.72f, 0.9f, 1f, 1f);
            }

            gameplayHudObjective = CreateIntroText(gameplayHudRoot.transform, "Objetivo: investigue a ilha.", 24, FontStyle.Normal,
                new Vector2(0.04f, 0.83f), new Vector2(0.96f, 0.91f));
            if (gameplayHudObjective != null)
            {
                gameplayHudObjective.alignment = TextAnchor.UpperLeft;
                gameplayHudObjective.color = Color.white;
            }

            gameplayHudPrompt = CreateIntroText(gameplayHudRoot.transform, "Toque em Interagir para investigar pistas.", 22, FontStyle.Italic,
                new Vector2(0.04f, 0.04f), new Vector2(0.96f, 0.12f));
            if (gameplayHudPrompt != null)
            {
                gameplayHudPrompt.alignment = TextAnchor.LowerLeft;
                gameplayHudPrompt.color = new Color(0.75f, 0.95f, 0.78f, 1f);
            }

            gameplayHudStage = CreateIntroText(gameplayHudRoot.transform, "FASE 1/3", 24, FontStyle.Bold,
                new Vector2(0.70f, 0.91f), new Vector2(0.96f, 0.99f));
            if (gameplayHudStage != null)
            {
                gameplayHudStage.alignment = TextAnchor.UpperRight;
                gameplayHudStage.color = new Color(1f, 0.84f, 0.35f, 1f);
            }

            gameplayControlsRoot = new GameObject("Nexo Mobile Controls");
            gameplayControlsRoot.transform.SetParent(gameplayHudRoot.transform, false);
            var controlsRect = gameplayControlsRoot.AddComponent<RectTransform>();
            controlsRect.anchorMin = new Vector2(0.04f, 0.12f);
            controlsRect.anchorMax = new Vector2(0.96f, 0.42f);
            controlsRect.offsetMin = Vector2.zero;
            controlsRect.offsetMax = Vector2.zero;

            CreateHudButton(gameplayControlsRoot.transform, "UP", "â–²", new Vector2(0.04f, 0.50f), new Vector2(0.16f, 0.96f), () => MovePlayer(Vector3.forward));
            CreateHudButton(gameplayControlsRoot.transform, "LEFT", "â—€", new Vector2(0.00f, 0.04f), new Vector2(0.12f, 0.50f), () => MovePlayer(Vector3.left));
            CreateHudButton(gameplayControlsRoot.transform, "DOWN", "â–¼", new Vector2(0.04f, 0.04f), new Vector2(0.16f, 0.50f), () => MovePlayer(Vector3.back));
            CreateHudButton(gameplayControlsRoot.transform, "RIGHT", "â–¶", new Vector2(0.16f, 0.04f), new Vector2(0.28f, 0.50f), () => MovePlayer(Vector3.right));
            gameplayInteractButton = CreateHudButton(gameplayControlsRoot.transform, "INTERACT", "INVESTIGAR", new Vector2(0.68f, 0.08f), new Vector2(0.98f, 0.52f), Interact);

            gameplayGuidePanel = new GameObject("Nexo First Mission Guide");
            gameplayGuidePanel.transform.SetParent(gameplayHudRoot.transform, false);
            var guideRect = gameplayGuidePanel.AddComponent<RectTransform>();
            guideRect.anchorMin = new Vector2(0.27f, 0.52f);
            guideRect.anchorMax = new Vector2(0.73f, 0.78f);
            guideRect.offsetMin = Vector2.zero;
            guideRect.offsetMax = Vector2.zero;
            var guideImage = gameplayGuidePanel.AddComponent<Image>();
            if (guideImage != null)
            {
                guideImage.color = new Color(0.025f, 0.10f, 0.18f, 0.94f);
                guideImage.raycastTarget = false;
            }
            gameplayGuideText = CreateIntroText(gameplayGuidePanel.transform,
                "COMO JOGAR\n\n1. Mova o explorador com as setas\n2. Toque em INVESTIGAR para registrar a pista\n3. Siga o marcador pulsante\n4. Resolva o desafio para ativar o portal",
                24, FontStyle.Bold, new Vector2(0.06f, 0.06f), new Vector2(0.94f, 0.94f));
            if (gameplayGuideText != null)
            {
                gameplayGuideText.alignment = TextAnchor.MiddleCenter;
                gameplayGuideText.color = new Color(0.85f, 0.96f, 1f, 1f);
                gameplayGuideText.raycastTarget = false;
            }
            gameplayGuideDismissButton = CreateHudButton(gameplayGuidePanel.transform, "GUIDE_DISMISS", "ENTENDI", new Vector2(0.32f, 0.02f), new Vector2(0.68f, 0.22f), DismissGameplayGuide);
            gameplayGuidePanel.SetActive(false);
            gameplayHudRoot.SetActive(false);
        }

        private void DismissGameplayGuide()
        {
            gameplayGuideVisible = false;
            gameplayGuideTime = 0f;
            if (gameplayGuidePanel != null) gameplayGuidePanel.SetActive(false);
            prompt = "Agora investigue a fonte azul marcada pelo alvo pulsante.";
            if (audioDirector != null) audioDirector.PlayClick();
        }

        private Button CreateHudButton(Transform parent, string name, string label, Vector2 anchorMin, Vector2 anchorMax, UnityAction action)
        {
            if (parent == null) return null;
            var buttonObject = new GameObject("Nexo " + name + " Button");
            buttonObject.transform.SetParent(parent, false);
            var rect = buttonObject.AddComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = new Vector2(4f, 4f);
            rect.offsetMax = new Vector2(-4f, -4f);
            var image = buttonObject.AddComponent<Image>();
            if (image != null) image.color = new Color(0.06f, 0.32f, 0.48f, 0.92f);
            var button = buttonObject.AddComponent<Button>();
            if (button == null) return null;
            button.targetGraphic = image;
            button.onClick.AddListener(action);
            var text = CreateIntroText(buttonObject.transform, label, name == "INTERACT" ? 20 : 30, FontStyle.Bold, Vector2.zero, Vector2.one);
            if (text != null)
            {
                text.alignment = TextAnchor.MiddleCenter;
                text.color = Color.white;
            }
            return button;
        }

        private void RefreshGameplayHud()
        {
            if (gameplayHudRoot == null || mission == null) return;
            bool visible = !showMenu && !showIntro;
            gameplayHudRoot.SetActive(visible);
            if (!visible) return;
            if (gameplayGuidePanel != null)
            {
                gameplayGuidePanel.SetActive(gameplayGuideVisible && !challengeOpen);
                gameplayGuideTime += Time.unscaledDeltaTime;
                float guidePulse = 1f + Mathf.Sin(gameplayGuideTime * 2.2f) * 0.025f;
                gameplayGuidePanel.transform.localScale = Vector3.one * guidePulse;
            }
            int water = mission.Save != null && mission.Save.activeMission != null ? mission.Save.activeMission.water : 0;
            if (gameplayHudStatus != null)
                gameplayHudStatus.text = "NEXO | " + mission.StateMachine.State + " | Agua: " + water +
                    " | Pontuacao: " + score + " | Recorde: " + highScore;
            if (gameplayHudObjective != null)
                gameplayHudObjective.text = activeStage == 1
                    ? "Objetivo: investigue a fonte, o canal e a valvula."
                    : (activeStage == 2 ? "Objetivo: resolva o portal de eficiencia energetica." : "Objetivo: resolva o portal de logica.");
            if (gameplayHudStage != null) gameplayHudStage.text = "FASE " + activeStage + "/3";
            if (gameplayHudPrompt != null)
                gameplayHudPrompt.text = string.IsNullOrEmpty(prompt)
                    ? (challengeOpen ? challengeFeedback : "Mova-se com as setas e toque em INVESTIGAR para abrir o portal.")
                    : prompt;
        }

        private void RefreshChallengeCanvas()
        {
            if (challengeCanvasPanel == null || challengeCanvasPrompt == null || challengeCanvasButtons == null) return;
            challengeCanvasPanel.SetActive(challengeOpen);
            if (!challengeOpen) return;
            challengeCanvasPrompt.text = challengeFeedback;
            if (!challengeAnswered)
            {
                int[] options = GetChallengeOptions();
                for (int i = 0; i < challengeCanvasButtons.Length; i++)
                {
                    var button = challengeCanvasButtons[i];
                    if (button == null) continue;
                    button.gameObject.SetActive(true);
                    button.onClick.RemoveAllListeners();
                    int answer = options[i];
                    button.onClick.AddListener(() => ResolveChallenge(answer));
                    var image = button.targetGraphic as Image;
                    if (image != null) image.color = i == 0 ? new Color(0.12f, 0.62f, 0.38f, 1f) : new Color(0.10f, 0.42f, 0.68f, 1f);
                    var label = button.GetComponentInChildren<Text>();
                    if (label != null) label.text = answer + " unidades";
                }
            }
            else
            {
                var first = challengeCanvasButtons[0];
                if (first != null)
                {
                    first.gameObject.SetActive(true);
                    first.onClick.RemoveAllListeners();
                    first.onClick.AddListener(CloseChallenge);
                    var image = first.targetGraphic as Image;
                    if (image != null) image.color = challengeCorrect ? new Color(0.12f, 0.65f, 0.38f, 1f) : new Color(0.70f, 0.16f, 0.18f, 1f);
                    var label = first.GetComponentInChildren<Text>();
                    if (label != null) label.text = challengeCorrect ? "CONTINUAR" : "TENTAR NOVAMENTE";
                }
                for (int i = 1; i < challengeCanvasButtons.Length; i++)
                    if (challengeCanvasButtons[i] != null) challengeCanvasButtons[i].gameObject.SetActive(false);
            }
        }

        private void CloseChallenge()
        {
            challengeOpen = false;
            challengeAnswered = false;
            challengeFeedback = "";
            if (challengeCanvasPanel != null) challengeCanvasPanel.SetActive(false);
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
            // Unity exige que os trÃªs eixos usem o mesmo modo de curva.
            // TwoConstants evita a mistura entre Constant e TwoConstants em batchmode/IL2CPP.
            velocity.x = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocity.y = new ParticleSystem.MinMaxCurve(0.12f, 0.28f);
            velocity.z = new ParticleSystem.MinMaxCurve(0f, 0f);
            // Do not depend on the platform's default particle material. On
            // Android/SwiftShader that implicit material can resolve to a
            // missing particle shader and render magenta squares. Reuse the
            // already validated mobile-safe world material instead.
            var particleRenderer = particleObject.GetComponent<ParticleSystemRenderer>();
            if (particleRenderer != null && defaultWorldMaterial != null && defaultWorldMaterial.shader != null)
            {
                particleRenderer.material = new Material(defaultWorldMaterial);
                if (particleRenderer.material != null)
                    particleRenderer.material.color = new Color(0.35f, 0.85f, 1f, 0.75f);
            }
            else if (particleRenderer != null)
            {
                particleRenderer.enabled = false;
                Debug.LogWarning("Nexo: particle material unavailable; floating lights disabled to avoid magenta fallback.");
            }
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
            introGroup.interactable = visible;
            introGroup.blocksRaycasts = visible;
            if (!visible) return;
            introGroup.alpha = 1f;
            introSubtitle.text = showMenu ? "Reconstrua a conexao entre as ilhas." : "Observe a fonte, siga o canal e restaure o fluxo.";
            introActionLabel.text = showMenu ? "INICIAR MISSAO" : "ENTRAR NA ILHA";
        }

        private void BeginModernMission()
        {
            if (missionTransitionRequested) return;
            missionTransitionRequested = true;
            Debug.Log("[NEXO_INPUT] BeginModernMission requested; transitioning from intro.");
            showMenu = false;
            EnsureWorldBuilt();
            if (showIntro)
            {
                mission.SkipIntro();
                showIntro = false;
                introElapsed = 0f;
                gameplayGuideVisible = true;
                RefreshGameplayHud();
                if (introTransition != null) StopCoroutine(introTransition);
                introTransition = StartCoroutine(FadeIntoMission());
                return;
            }
            RefreshModernIntro();
        }

        private void HandleStartButtonPressed()
        {
            Debug.Log("[NEXO_INPUT] Start Mission button activated.");
            if (audioDirector != null) audioDirector.PlayClick();
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
            Debug.Log("[NEXO_GAMEPLAY] FadeIntoMission started; gameplay HUD activated.");
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
            if (introParticles != null)
            {
                var emission = introParticles.emission;
                emission.rateOverTime = 2.5f;
                introParticles.Play(true);
            }
            gameplayGuideVisible = true;
            gameplayGuideTime = 0f;
            cameraRevealTime = 0f;
            introTransition = null;
            Debug.Log("[NEXO_GAMEPLAY] Mission gameplay active; objective source ready.");
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
            var waterSurface = MakeCube("Water Surface", new Vector3(-1.5f, 0.06f, 2.8f), new Vector3(6.5f, 0.08f, 3.8f), new Color(0.04f, 0.38f, 0.82f));
            if (waterSurface != null)
            {
                waterSurfaceTransform = waterSurface.transform;
                waterSurfaceBaseScale = waterSurfaceTransform.localScale;
                waterSurfaceRenderer = waterSurface.GetComponent<Renderer>();
                waterSurfaceBaseColor = new Color(0.04f, 0.38f, 0.82f, 1f);
            }
            var source = MakeCube("Source", new Vector3(-5, 0.4f, 4), new Vector3(1.4f, 0.8f, 1.4f), NexoVisualTheme.Water, true);
            InitializeInteractable(source, ClueId.Source, "Fonte: siga as marcas de umidade atÃ© o canal.");
            sourceTransform = source == null ? null : source.transform;
            InitializeInteractable(MakeCube("Channel", new Vector3(-1, 0.25f, 2), new Vector3(3.5f, 0.3f, 0.5f), NexoVisualTheme.Water, true), ClueId.Channel, "O canal aponta para o reservat?rio.");
            channelTransform = GameObject.Find("Channel") == null ? null : GameObject.Find("Channel").transform;
            if (channelTransform != null) channelBaseScale = channelTransform.localScale;
            CreateWaterRipples();
            var valve = MakeCube("Valve", new Vector3(2, 0.7f, 1), new Vector3(0.8f, 1.2f, 0.8f), NexoVisualTheme.Signal, true);
            InitializeInteractable(valve, ClueId.Valve, "Valvula: alinhe entrada e saida para restaurar o fluxo.");
            valveTransform = valve == null ? null : valve.transform;
            valveBaseColor = NexoVisualTheme.Signal;
            string initialPortalName = activeStage == 1 ? "GreenDoor" : (activeStage == 2 ? "EnergyPortal" : "LogicPortal");
            Color initialPortalColor = activeStage == 1 ? new Color(0.18f, 0.85f, 0.42f) : (activeStage == 2 ? new Color(0.2f, 0.65f, 1f) : new Color(0.68f, 0.32f, 1f));
            var greenDoor = MakeCube(initialPortalName, new Vector3(6, 1.2f, -1), new Vector3(1.2f, 2.4f, 0.35f), initialPortalColor, true);
            if (greenDoor != null)
            {
                challengeDoorTransform = greenDoor.transform;
                challengeDoorBaseScale = challengeDoorTransform.localScale;
                challengeDoorRenderer = greenDoor.GetComponent<Renderer>();
                challengeDoorBaseColor = initialPortalColor;
                activeChallengeDoor = greenDoor.AddComponent<NexoChallengeDoor>();
                if (activeChallengeDoor != null) activeChallengeDoor.stage = activeStage;
                else Debug.LogWarning("Nexo: porta verde sem componente de desafio.");
                CreatePortalFeedback(greenDoor.transform, initialPortalColor);
            }
            CreateObjectiveMarker();
            SetObjectiveStep(0);
            MakeCube("Reservoir", new Vector3(4, 1, 3), new Vector3(3, 2, 2), NexoVisualTheme.Stone, true);
            MakeCube("ExitBridge", new Vector3(7, 0, 3), new Vector3(4, 0.25f, 2), NexoVisualTheme.Wood);
            CreateLandscapeDecor();
            GameObject avatar = CreateExplorerAvatar(new Vector3(-4, 1, -1));
            if (avatar == null)
            {
                Debug.LogError("Nexo: avatar n?o p?de ser criado; gameplay continuar? sem movimento.");
                return;
            }
            playerTransform = avatar.transform;
            avatarTransform = playerTransform;
            if (view != null)
            {
                view.transform.position = new Vector3(-6, 6, -10);
                view.transform.LookAt(avatar.transform);
            }
        }

        private GameObject CreateExplorerAvatar(Vector3 position)
        {
            // Prefer the imported, humanoid test asset when the Unity Editor
            // setup step has generated it. This is the production path; the
            // procedural model remains an explicit fallback for environments
            // that cannot import FBX assets, and is reported in the log.
            var authoredPrefab = Resources.Load<GameObject>("Characters/CapBuddy");
            if (authoredPrefab == null) authoredPrefab = Resources.Load<GameObject>("Characters/NexoExplorer_FreeTest");
            if (authoredPrefab != null)
            {
                var authored = Instantiate(authoredPrefab, position, Quaternion.identity);
                if (authored != null)
                {
                    authored.name = "Explorer";
                    var authoredAnimator = authored.GetComponentInChildren<Animator>(true);
                    if (authoredAnimator != null && authoredAnimator.runtimeAnimatorController != null)
                        authoredAnimator.Play("Idle", 0, 0f);
                    Debug.Log("[NEXO_AVATAR] Authored prefab loaded: " + authoredPrefab.name);
                    return authored;
                }
            }

            Debug.LogWarning("[NEXO_AVATAR] Authored avatar prefab unavailable; using procedural fallback. Run CapBuddySetup in Unity before release builds.");
            var root = new GameObject("Explorer");
            if (root == null) return null;
            root.transform.position = position;
            GameObject body = CreateVisualPart(root.transform, "Explorer Body", new Vector3(0f, 0.15f, 0f), new Vector3(0.85f, 1.0f, 0.62f), new Color(0.08f, 0.25f, 0.42f));
            GameObject head = CreateVisualPart(root.transform, "Explorer Head", new Vector3(0f, 0.82f, 0f), new Vector3(0.72f, 0.55f, 0.66f), new Color(0.16f, 0.68f, 0.88f));
            CreateVisualPart(root.transform, "Explorer Visor", new Vector3(0f, 0.84f, -0.34f), new Vector3(0.48f, 0.16f, 0.06f), new Color(0.55f, 0.95f, 1f));
            CreateVisualPart(root.transform, "Explorer Backpack", new Vector3(0f, 0.18f, 0.38f), new Vector3(0.52f, 0.72f, 0.22f), new Color(0.95f, 0.55f, 0.18f));
            GameObject leftLeg = CreateVisualPart(root.transform, "Explorer Left Leg", new Vector3(-0.22f, -0.46f, 0f), new Vector3(0.22f, 0.42f, 0.26f), new Color(0.12f, 0.40f, 0.58f));
            GameObject rightLeg = CreateVisualPart(root.transform, "Explorer Right Leg", new Vector3(0.22f, -0.46f, 0f), new Vector3(0.22f, 0.42f, 0.26f), new Color(0.12f, 0.40f, 0.58f));
            avatarBodyTransform = body == null ? null : body.transform;
            avatarHeadTransform = head == null ? null : head.transform;
            avatarLeftLegTransform = leftLeg == null ? null : leftLeg.transform;
            avatarRightLegTransform = rightLeg == null ? null : rightLeg.transform;
            return root;
        }

        private void CreateLandscapeDecor()
        {
            landscapeDecor.Clear();
            CreateTree(new Vector3(-7f, 0f, 1.5f), 1.1f);
            CreateTree(new Vector3(5.5f, 0f, 5f), 0.85f);
            CreateRock(new Vector3(-6f, 0f, 6f), new Vector3(1.2f, 0.7f, 1.4f));
            CreateRock(new Vector3(7f, 0f, -4f), new Vector3(1.4f, 0.8f, 0.9f));
        }

        private void CreateTree(Vector3 position, float scale)
        {
            var trunk = MakeCube("Landscape Tree Trunk", position + Vector3.up * (0.8f * scale), new Vector3(0.35f, 1.6f, 0.35f) * scale, new Color(0.34f, 0.18f, 0.08f));
            var canopy = MakeCube("Landscape Tree Canopy", position + Vector3.up * (2.0f * scale), new Vector3(1.6f, 1.4f, 1.6f) * scale, new Color(0.08f, 0.52f, 0.26f));
            if (trunk != null) landscapeDecor.Add(trunk.transform);
            if (canopy != null) landscapeDecor.Add(canopy.transform);
        }

        private void CreateRock(Vector3 position, Vector3 scale)
        {
            var rock = MakeCube("Landscape Rock", position + Vector3.up * (scale.y * 0.5f), scale, new Color(0.30f, 0.36f, 0.42f));
            if (rock != null) landscapeDecor.Add(rock.transform);
        }

        private GameObject CreateVisualPart(Transform parent, string name, Vector3 localPosition, Vector3 scale, Color color)
        {
            if (parent == null || defaultWorldMaterial == null || defaultWorldMaterial.shader == null) return null;
            var part = new GameObject(name);
            if (part == null) return null;
            part.transform.SetParent(parent, false);
            part.transform.localPosition = localPosition;
            part.transform.localScale = scale;
            var filter = part.AddComponent<MeshFilter>();
            var renderer = part.AddComponent<MeshRenderer>();
            if (filter == null || renderer == null) return part;
            filter.sharedMesh = CreateCubeMesh();
            renderer.material = new Material(defaultWorldMaterial);
            if (renderer.material != null) renderer.material.color = color;
            return part;
        }

        private void CreateWaterRipples()
        {
            waterRipples.Clear();
            if (channelTransform == null) return;
            for (int i = 0; i < 3; i++)
            {
                var ripple = CreateVisualPart(channelTransform, "Water Ripple " + i,
                    new Vector3(-1.0f + i * 0.9f, 0.22f, 0f), new Vector3(0.45f, 0.035f, 0.16f), new Color(0.45f, 0.95f, 1f));
                if (ripple != null) waterRipples.Add(ripple.transform);
            }
        }

        private void CreateObjectiveMarker()
        {
            if (objectiveMarker != null) return;
            objectiveMarker = new GameObject("Nexo Objective Marker");
            if (objectiveMarker == null) return;
            var markerRenderer = objectiveMarker.AddComponent<MeshRenderer>();
            var markerFilter = objectiveMarker.AddComponent<MeshFilter>();
            if (markerRenderer == null || markerFilter == null) return;
            markerFilter.sharedMesh = CreateCubeMesh();
            markerRenderer.material = defaultWorldMaterial == null ? null : new Material(defaultWorldMaterial);
            if (markerRenderer.material != null) markerRenderer.material.color = new Color(1f, 0.82f, 0.22f, 1f);
            objectiveMarker.transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);
        }

        private void SetObjectiveStep(int step)
        {
            objectiveStep = Mathf.Clamp(step, 0, 3);
            if (objectiveStep == 0) objectiveTarget = sourceTransform;
            else if (objectiveStep == 1) objectiveTarget = channelTransform;
            else if (objectiveStep == 2) objectiveTarget = valveTransform;
            else objectiveTarget = challengeDoorTransform;
            if (objectiveTarget != null && objectiveMarker != null)
            {
                objectiveMarker.SetActive(true);
                objectiveMarker.transform.position = objectiveTarget.position + Vector3.up * 1.5f;
            }
            if (objectiveStep == 0) prompt = "1/3: investigue a FONTE azul.";
            else if (objectiveStep == 1) prompt = "2/3: siga o CANAL de agua ate a valvula.";
            else if (objectiveStep == 2) prompt = "3/3: investigue a VALVULA para ativar o portal.";
            else prompt = "Portal ativo: toque em INVESTIGAR para resolver o desafio.";
        }

        private void EnsureGameplayLight()
        {
            var lights = FindObjectsByType<Light>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            for (var i = 0; i < lights.Length; i++)
            {
                if (lights[i] != null && lights[i].type == LightType.Directional)
                {
                    gameplayLight = lights[i];
                    return;
                }
            }
            var lightObject = new GameObject("DynamicDirectionalLight");
            if (lightObject == null) return;
            var light = lightObject.AddComponent<Light>();
            if (light == null) return;
            gameplayLight = light;
            light.type = LightType.Directional;
            light.color = Color.white;
            light.intensity = 1.2f;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        }

        private void CreatePortalFeedback(Transform portal, Color color)
        {
            if (portal == null) return;
            var lightObject = new GameObject("Nexo Portal Light");
            if (lightObject != null)
            {
                lightObject.transform.position = portal.position + Vector3.back * 0.8f;
                portalLight = lightObject.AddComponent<Light>();
                if (portalLight != null)
                {
                    portalLight.type = LightType.Point;
                    portalLight.color = color;
                    portalLight.range = 7f;
                    portalLight.intensity = 1.6f;
                }
            }
            if (defaultWorldMaterial == null || defaultWorldMaterial.shader == null) return;
            var fxObject = new GameObject("Nexo Portal Energy FX");
            if (fxObject == null) return;
            fxObject.transform.SetParent(portal, false);
            fxObject.transform.localPosition = new Vector3(0f, 0f, -0.4f);
            portalParticles = fxObject.AddComponent<ParticleSystem>();
            var main = portalParticles.main;
            main.loop = true;
            main.startLifetime = 1.4f;
            main.startSpeed = 0.25f;
            main.startSize = new ParticleSystem.MinMaxCurve(0.04f, 0.10f);
            main.startColor = new Color(color.r, color.g, color.b, 0.85f);
            main.maxParticles = 20;
            var emission = portalParticles.emission;
            emission.rateOverTime = 8f;
            var shape = portalParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Circle;
            shape.radius = 0.65f;
            var velocity = portalParticles.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.Local;
            velocity.x = new ParticleSystem.MinMaxCurve(-0.08f, 0.08f);
            velocity.y = new ParticleSystem.MinMaxCurve(0.10f, 0.25f);
            velocity.z = new ParticleSystem.MinMaxCurve(0f, 0f);
            var renderer = fxObject.GetComponent<ParticleSystemRenderer>();
            if (renderer != null)
            {
                renderer.material = new Material(defaultWorldMaterial);
                if (renderer.material != null) renderer.material.color = new Color(color.r, color.g, color.b, 0.85f);
            }
            portalParticles.Play(true);
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
                if (defaultWorldMaterial != null)
                {
                    var material = new Material(defaultWorldMaterial);
                    material.color = color;
                    renderer.sharedMaterial = material;
                }
            }
            else Debug.LogError("Nexo: MeshRenderer n?o p?de ser criado para " + name);
            if (addCollider && item.AddComponent<BoxCollider>() == null)
                Debug.LogError("Nexo: BoxCollider n?o p?de ser criado para " + name);
            return item;
        }

        private void Update()
        {
            ApplySafeArea();
            RefreshGameplayHud();
            AnimateGameplayWorld();
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
            if (mission == null) return;
            Vector2 pointerPosition;
            if (TryGetPrimaryPointerDown(out pointerPosition))
            {
                Debug.Log("[NEXO_INPUT] Pointer detected at " + pointerPosition + "; intro=" + (showMenu || showIntro));
                if (showMenu || showIntro)
                {
                    HandleStartButtonPressed();
                    return;
                }
                if (!challengeOpen && !showDiary && !showSettings)
                    InteractAtScreenPoint(pointerPosition);
            }
            if (playerTransform == null) return;
            if (challengeOpen) return;
            if (showMenu || showDiary || showSettings) return;
            if (mission.StateMachine.State == GameState.Paused) return;
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            input += new Vector3(touchMove.x, 0, touchMove.y);
            input = Vector3.ClampMagnitude(input, 1f);
            playerTransform.position += input * 3f * Time.deltaTime;
            cameraRevealTime += Time.unscaledDeltaTime;
            if (cameraRevealTime < 3.5f)
            {
                Vector3 overview = new Vector3(0f, 8f, -13f);
                view.transform.position = Vector3.Lerp(view.transform.position, overview, 3.5f * Time.deltaTime);
                view.transform.LookAt(new Vector3(0f, 0.8f, 1.5f));
            }
            else
            {
                view.transform.position = Vector3.Lerp(view.transform.position, playerTransform.position + new Vector3(0, 6, -10), 6f * Time.deltaTime);
                view.transform.LookAt(playerTransform.position + Vector3.up * 0.8f);
            }
            if (Input.GetKeyDown(KeyCode.E)) Interact();
        }

        private void Interact()
        {
            Debug.Log("[NEXO_GAMEPLAY] Interact requested; objectiveStep=" + objectiveStep);
            if (gameplayGuideVisible) DismissGameplayGuide();
            if (objectiveStep < 3)
            {
                InvestigateCurrentObjective();
                return;
            }
            if (activeChallengeDoor != null && activeChallengeDoor.unlocked && activeChallengeDoor.stage == activeStage)
            {
                OpenChallenge();
                return;
            }
            if (view == null) return;
            InteractAtScreenPoint(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        }

        private void MovePlayer(Vector3 direction)
        {
            if (playerTransform == null || showMenu || showIntro || challengeOpen) return;
            if (gameplayGuideVisible) DismissGameplayGuide();
            playerTransform.position += direction.normalized * 0.8f;
            prompt = "Explorando a ilha... toque em INVESTIGAR para abrir o desafio.";
            if (audioDirector != null) audioDirector.PlayClick();
        }

        private void AnimateGameplayWorld()
        {
            if (showMenu || showIntro || challengeOpen || challengeDoorTransform == null) return;
            gameplayPulseTime += Time.unscaledDeltaTime;
            gameplayWorldTime += Time.unscaledDeltaTime;
            float pulse = 1f + Mathf.Sin(gameplayPulseTime * 2.4f) * 0.10f;
            challengeDoorTransform.localScale = challengeDoorBaseScale * pulse;
            if (challengeDoorRenderer != null && challengeDoorRenderer.material != null)
            {
                float glow = 0.82f + Mathf.Sin(gameplayPulseTime * 3.2f) * 0.18f;
                challengeDoorRenderer.material.color = new Color(challengeDoorBaseColor.r * glow, challengeDoorBaseColor.g * glow, challengeDoorBaseColor.b * glow, 1f);
            }
            if (channelTransform != null)
            {
                float wave = 1f + Mathf.Sin(gameplayWorldTime * 2.8f) * 0.10f;
                channelTransform.localScale = new Vector3(channelBaseScale.x, channelBaseScale.y * wave, channelBaseScale.z);
            }
            if (avatarTransform != null)
            {
                Vector3 p = avatarTransform.localPosition;
                p.y = 1f + Mathf.Sin(gameplayWorldTime * 3.0f) * 0.06f;
                avatarTransform.localPosition = p;
            }
            float walkWave = Mathf.Sin(gameplayWorldTime * 5.5f);
            if (avatarBodyTransform != null) avatarBodyTransform.localRotation = Quaternion.Euler(0f, walkWave * 2.5f, 0f);
            if (avatarHeadTransform != null) avatarHeadTransform.localRotation = Quaternion.Euler(0f, -walkWave * 4f, 0f);
            if (avatarLeftLegTransform != null) avatarLeftLegTransform.localRotation = Quaternion.Euler(walkWave * 12f, 0f, 0f);
            if (avatarRightLegTransform != null) avatarRightLegTransform.localRotation = Quaternion.Euler(-walkWave * 12f, 0f, 0f);
            if (waterSurfaceTransform != null)
            {
                float waterWave = 1f + Mathf.Sin(gameplayWorldTime * 2.2f) * 0.025f;
                waterSurfaceTransform.localScale = new Vector3(waterSurfaceBaseScale.x * waterWave, waterSurfaceBaseScale.y, waterSurfaceBaseScale.z);
            }
            if (waterSurfaceRenderer != null && waterSurfaceRenderer.material != null)
            {
                float waterGlow = 0.88f + Mathf.Sin(gameplayWorldTime * 2.6f) * 0.12f;
                waterSurfaceRenderer.material.color = waterSurfaceBaseColor * waterGlow;
            }
            for (int i = 0; i < landscapeDecor.Count; i++)
            {
                Transform decor = landscapeDecor[i];
                if (decor == null) continue;
                float sway = Mathf.Sin(gameplayWorldTime * 1.4f + i) * 1.5f;
                decor.localRotation = Quaternion.Euler(0f, sway, sway * 0.35f);
            }
            if (gameplayLight != null)
                gameplayLight.intensity = 1.05f + Mathf.Sin(gameplayWorldTime * 1.6f) * 0.15f;
            if (portalLight != null)
                portalLight.intensity = 1.25f + Mathf.Sin(gameplayWorldTime * 3.0f) * 0.55f;
            if (portalParticles != null)
                portalParticles.transform.localRotation = Quaternion.Euler(0f, gameplayWorldTime * 35f, 0f);
            for (int i = 0; i < waterRipples.Count; i++)
            {
                Transform ripple = waterRipples[i];
                if (ripple == null) continue;
                Vector3 position = ripple.localPosition;
                position.x += Mathf.Sin(gameplayWorldTime * 2.5f + i) * 0.0025f;
                ripple.localPosition = position;
            }
            if (objectiveMarker != null && objectiveTarget != null && objectiveMarker.activeSelf)
            {
                objectiveMarker.transform.position = objectiveTarget.position + Vector3.up * (1.35f + Mathf.Sin(gameplayWorldTime * 3.5f) * 0.18f);
                objectiveMarker.transform.Rotate(Vector3.up, 90f * Time.unscaledDeltaTime, Space.World);
            }
            if (valveTransform != null && valveTransform.GetComponent<Renderer>() != null)
            {
                var valveRenderer = valveTransform.GetComponent<Renderer>();
                if (valveRenderer.material != null)
                {
                    float signal = objectiveStep == 2 ? 0.75f + Mathf.Sin(gameplayWorldTime * 4f) * 0.25f : 1f;
                    valveRenderer.material.color = valveBaseColor * signal;
                }
            }
        }

        private void InvestigateCurrentObjective()
        {
            if (mission == null) return;
            Debug.Log("[NEXO_GAMEPLAY] Investigating objective step " + objectiveStep);
            if (objectiveStep == 0)
            {
                mission.RecordClue(ClueId.Source);
                SetObjectiveStep(1);
                prompt = "Fonte registrada. Agora siga o canal azul.";
                if (audioDirector != null) audioDirector.PlayDiscovery();
            }
            else if (objectiveStep == 1)
            {
                mission.RecordClue(ClueId.Channel);
                SetObjectiveStep(2);
                prompt = "Canal localizado. Investigue a valvula laranja.";
                if (audioDirector != null) audioDirector.PlayDiscovery();
            }
            else if (objectiveStep == 2)
            {
                mission.RecordClue(ClueId.Valve);
                mission.EvaluateValve(0);
                mission.Allocate(WaterAllocation.Reservoir);
                SetObjectiveStep(3);
                prompt = "Fluxo restaurado. Toque em INVESTIGAR para abrir o portal.";
                if (audioDirector != null) audioDirector.PlayPortalUnlock();
            }
        }

        private void InteractAtScreenPoint(Vector2 screenPoint)
        {
            if (view == null || challengeOpen) return;
            Ray ray = view.ScreenPointToRay(screenPoint);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 40f) || hit.collider == null) return;
            NexoChallengeDoor door = hit.collider.GetComponent<NexoChallengeDoor>();
            NexoInteractable target = hit.collider.GetComponent<NexoInteractable>();
            if (door != null && door.unlocked && door.stage == activeStage)
            {
                OpenChallenge();
            }
            else if (target != null)
            {
                if ((objectiveStep == 0 && target.clue != ClueId.Source) ||
                    (objectiveStep == 1 && target.clue != ClueId.Channel) ||
                    (objectiveStep == 2 && target.clue != ClueId.Valve))
                {
                    prompt = objectiveStep == 0 ? "Primeiro investigue a fonte azul." : (objectiveStep == 1 ? "Siga o canal azul antes da valvula." : "Agora investigue a valvula laranja.");
                    if (audioDirector != null) audioDirector.PlayFeedback(false);
                    return;
                }
                InvestigateCurrentObjective();
                mission.RecordClue(target.clue);
                prompt = target.description;
                if (audioDirector != null) audioDirector.PlayClick();
            }
            else if (hit.collider.name == "Reservoir")
            {
                mission.Allocate(WaterAllocation.Reservoir);
                if (audioDirector != null) audioDirector.PlayClick();
            }
        }

        private bool TryGetPrimaryPointerDown(out Vector2 position)
        {
            position = Vector2.zero;
#if ENABLE_INPUT_SYSTEM
            if (Touchscreen.current != null)
            {
                var touch = Touchscreen.current.primaryTouch;
                if (touch.press.wasPressedThisFrame)
                {
                    position = touch.position.ReadValue();
                    return true;
                }
            }
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                position = Mouse.current.position.ReadValue();
                return true;
            }
#endif
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);
                if (touch.phase == UnityEngine.TouchPhase.Began || touch.phase == UnityEngine.TouchPhase.Moved || touch.phase == UnityEngine.TouchPhase.Stationary)
                {
                    position = touch.position;
                    return true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                position = Input.mousePosition;
                return true;
            }
            return false;
        }

        private void OpenChallenge()
        {
            if (challengeOpen || mission == null || mission.StateMachine.State != GameState.Exploring) return;
            challengeOpen = true;
            gameplayGuideVisible = false;
            challengeAnswered = false;
            challengeCorrect = false;
            challengeFeedback = GetChallengePrompt();
            if (audioDirector != null) audioDirector.PlayClick();
            RefreshChallengeCanvas();
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
            if (audioDirector != null) audioDirector.PlayFeedback(challengeCorrect);
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
            RefreshChallengeCanvas();
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
            if (audioDirector != null) audioDirector.PlayPortalUnlock();
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
                challengeDoorBaseScale = challengeDoorTransform.localScale;
                challengeDoorRenderer = portal.GetComponent<Renderer>();
                challengeDoorBaseColor = portalColor;
                activeChallengeDoor = portal.AddComponent<NexoChallengeDoor>();
                if (activeChallengeDoor != null) activeChallengeDoor.stage = activeStage;
                CreatePortalFeedback(portal.transform, portalColor);
                SetObjectiveStep(3);
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
                if (challengeCanvasPanel == null) DrawChallenge(safe);
                return;
            }
            GUI.Label(new Rect(safe.x + 16, safe.y + 48, 360, 30), "Pontuacao: " + score + " | Recorde: " + highScore + " | Fase: " + activeStage + "/3");
            if (showMenu)
            {
                GUI.Box(new Rect(safe.x + 24, safe.y + 40, safe.width - 48, 220), "NEXO");
                GUI.Label(new Rect(safe.x + 55, safe.y + 90, safe.width - 110, 60), "O arquipÃ©lago perdeu a conexÃ£o. Restaure a primeira ilha.");
                if (GUI.Button(new Rect(safe.x + 55, safe.y + 165, 190, 44), "Iniciar missÃ£o")) showMenu = false;
                if (GUI.Button(new Rect(safe.x + 255, safe.y + 165, 190, 44), "ConfiguraÃ§Ãµes")) showSettings = true;
                if (showSettings) DrawSettings(safe);
                return;
            }
            GUI.Label(new Rect(safe.x + 16, safe.y + 16, safe.width - 32, 32), "NEXO | " + mission.StateMachine.State + " | Ãgua: " + mission.Save.activeMission.water);
            if (showIntro)
            {
                float introAlpha = Mathf.Clamp01(introElapsed / 0.45f);
                GUI.color = new Color(1f, 1f, 1f, introAlpha);
                GUI.Box(new Rect(safe.x + 30, safe.y + 70, safe.width - 60, 180), "O reservatÃ³rio silencioso");
                GUI.Label(new Rect(safe.x + 55, safe.y + 110, safe.width - 110, 60), "Observe a fonte, siga o canal e restaure o fluxo.");
                if (GUI.Button(new Rect(safe.x + 55, safe.y + 180, 180, 42), "ComeÃ§ar (pular prÃ³logo)")) { mission.SkipIntro(); showIntro = false; introElapsed = 0f; EnsureWorldBuilt(); audioDirector.PlayDiscovery(); }
                GUI.color = Color.white;
                return;
            }
            if (GUI.Button(new Rect(safe.x + 16, safe.yMax - 62, 70, 44), "Pausa")) { mission.Pause(); }
            if (GUI.Button(new Rect(safe.xMax - 180, safe.yMax - 62, 70, 44), "Interagir")) Interact();
            if (GUI.Button(new Rect(safe.xMax - 95, safe.yMax - 62, 70, 44), "Ajuda")) prompt = "Compare as marcas do canal e da vÃ¡lvula.";
            if (GUI.Button(new Rect(safe.x + 96, safe.yMax - 62, 70, 44), "DiÃ¡rio")) showDiary = true;
            if (!string.IsNullOrEmpty(prompt)) GUI.Box(new Rect(safe.x + 16, safe.y + 55, safe.width - 32, 55), prompt);
            if (showDiary) DrawDiary(safe);
            if (mission.Save.activeMission.discoveredClueIds != null &&
                System.Array.IndexOf(mission.Save.activeMission.discoveredClueIds, ClueId.Valve.ToString()) >= 0)
            {
                GUI.Label(new Rect(16, 118, 300, 28), "VÃ¡lvula: compare os marcadores");
                if (GUI.Button(new Rect(16, 148, 70, 36), "Pos. 1")) mission.EvaluateValve(0);
                if (GUI.Button(new Rect(92, 148, 70, 36), "Pos. 2")) mission.EvaluateValve(1);
                if (GUI.Button(new Rect(168, 148, 70, 36), "Pos. 3")) mission.EvaluateValve(2);
            }
            if (mission.Save.activeMission.allocation == WaterAllocation.Reservoir &&
                mission.Save.activeMission.valveIndex == MissionRuleEngine.ReservoirValveIndex &&
                !mission.Save.activeMission.completed &&
                GUI.Button(new Rect(250, 148, 170, 36), "Ativar reservatÃ³rio"))
            {
                mission.Complete();
            }
            if (mission.Save.activeMission.completed)
                GUI.Box(new Rect(30, 250, Screen.width - 60, 100), "MissÃ£o concluÃ­da â€” nova Ã¡rea desbloqueada: o sinal distante.");
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
                CloseChallenge();
                if (audioDirector != null) audioDirector.PlayClick();
                GUI.color = Color.white;
            }
            GUI.color = Color.white;
            }
        }

        private void DrawDiary(Rect safe)
        {
            GUI.Box(new Rect(safe.x + 30, safe.y + 90, safe.width - 60, 210), "DiÃ¡rio de descobertas");
            string clues = string.Join(", ", mission.Save.activeMission.discoveredClueIds ?? new string[0]);
            GUI.Label(new Rect(safe.x + 55, safe.y + 135, safe.width - 110, 80), "Pistas encontradas: " + clues);
            GUI.Label(new Rect(safe.x + 55, safe.y + 205, safe.width - 110, 60), "HipÃ³tese: a Ã¡gua segue uma rede que atravessa as ilhas.");
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
                GUI.Box(new Rect(safe.x + 40, safe.y + 60, safe.width - 80, 390), "ConfiguraÃ§Ãµes");
            GUI.Label(new Rect(safe.x + 70, safe.y + 105, 250, 32), "Ãudio: " + (mission.Save.audio.muted ? "silenciado" : "ativo"));
            if (GUI.Button(new Rect(safe.x + 70, safe.y + 145, 190, 40), mission.Save.audio.muted ? "Ativar Ã¡udio" : "Silenciar Ã¡udio"))
            {
                mission.Save.audio.muted = !mission.Save.audio.muted;
                audioDirector.Configure(mission.Save.audio);
            }
            GUI.Label(new Rect(safe.x + 70, safe.y + 205, 400, 32), "Legendas: sempre disponÃ­veis");
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
