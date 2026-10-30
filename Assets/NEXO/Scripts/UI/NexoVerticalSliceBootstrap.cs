using UnityEngine;
using Nexo.Core;
using Nexo.Audio;
using Nexo.Gameplay;
using Nexo.Persistence;
using Nexo.Progression;
using Nexo.Safety;
using Nexo.Learning;
using UnityEngine.UI;

namespace Nexo.UI
{
    /// <summary>Bootstrap temporário: cria uma ilha jogável sem depender de prefabs.</summary>
    public sealed class NexoVerticalSliceBootstrap : MonoBehaviour
    {
        private VerticalSliceMission mission;
        private CharacterController player;
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

        public GameState CurrentState { get { return mission == null ? GameState.Initializing : mission.StateMachine.State; } }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateIfMissing()
        {
            if (FindFirstObjectByType<NexoVerticalSliceBootstrap>() == null)
                new GameObject("Nexo Vertical Slice Runtime").AddComponent<NexoVerticalSliceBootstrap>();
        }

        private void Start()
        {
            EnsureMobileCanvas();
            mission = new VerticalSliceMission(new JsonFileSaveStore());
            mission.Initialize();
            audioDirector = gameObject.AddComponent<NexoAudioDirector>();
            audioDirector.Configure(mission.Save.audio);
            dataControls = new LocalDataControls();
            showIntro = mission.StateMachine.State == GameState.Intro;
            introElapsed = 0f;
            transferAnswered = HasTransferEvidence();
            // Keep the intro path UI-only. Build gameplay physics only after entry.
            if (!showIntro) EnsureWorldBuilt();
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

            var scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler == null) scaler = canvas.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
        }

        private void BuildWorld()
        {
            RenderSettings.ambientLight = NexoVisualTheme.Sky;
            MakePrimitive(PrimitiveType.Cube, "Island", new Vector3(0, -0.5f, 0), new Vector3(18, 1, 18), new Color(0.3f, 0.45f, 0.3f));
            MakePrimitive(PrimitiveType.Cube, "Source", new Vector3(-5, 0.4f, 4), new Vector3(1.4f, 0.8f, 1.4f), NexoVisualTheme.Water).AddComponent<NexoInteractable>().Initialize(ClueId.Source, "Fonte alta: siga as marcas de umidade.");
            MakePrimitive(PrimitiveType.Cube, "Channel", new Vector3(-1, 0.25f, 2), new Vector3(3.5f, 0.3f, 0.5f), NexoVisualTheme.Water).AddComponent<NexoInteractable>().Initialize(ClueId.Channel, "O canal aponta para o reservatório.");
            MakePrimitive(PrimitiveType.Cube, "Valve", new Vector3(2, 0.7f, 1), new Vector3(0.8f, 1.2f, 0.8f), NexoVisualTheme.Signal).AddComponent<NexoInteractable>().Initialize(ClueId.Valve, "Alinhe entrada e saída; há três posições.");
            MakePrimitive(PrimitiveType.Cube, "Reservoir", new Vector3(4, 1, 3), new Vector3(3, 2, 2), NexoVisualTheme.Stone);
            MakePrimitive(PrimitiveType.Cube, "ExitBridge", new Vector3(7, 0, 3), new Vector3(4, 0.25f, 2), NexoVisualTheme.Wood);
            GameObject avatar = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            avatar.name = "Explorer"; avatar.transform.position = new Vector3(-6, 1, -5);
            player = avatar.AddComponent<CharacterController>();
            Destroy(avatar.GetComponent<Collider>());
            GameObject cameraObject = new GameObject("Nexo Camera");
            view = cameraObject.AddComponent<Camera>();
            view.transform.position = new Vector3(-6, 6, -10);
            view.transform.LookAt(avatar.transform);
        }

        private GameObject MakePrimitive(PrimitiveType type, string name, Vector3 position, Vector3 scale, Color color)
        {
            GameObject item = GameObject.CreatePrimitive(type);
            item.name = name; item.transform.position = position; item.transform.localScale = scale;
            Renderer renderer = item.GetComponent<Renderer>();
            if (renderer != null) renderer.material.color = color;
            return item;
        }

        private void Update()
        {
            if (mission == null || player == null) return;
            if (showIntro) introElapsed += Time.unscaledDeltaTime;
            if (showMenu || showDiary || showSettings) return;
            if (mission.StateMachine.State == GameState.Paused) return;
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            input += new Vector3(touchMove.x, 0, touchMove.y);
            input = Vector3.ClampMagnitude(input, 1f);
            player.Move(input * 3f * Time.deltaTime);
            view.transform.position = Vector3.Lerp(view.transform.position, player.transform.position + new Vector3(0, 5, -8), 8f * Time.deltaTime);
            view.transform.LookAt(player.transform.position + Vector3.up);
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
