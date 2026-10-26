using UnityEngine;
using Nexo.Core;
using Nexo.Gameplay;
using Nexo.Persistence;
using Nexo.Progression;

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

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateIfMissing()
        {
            if (FindObjectOfType<NexoVerticalSliceBootstrap>() == null)
                new GameObject("Nexo Vertical Slice Runtime").AddComponent<NexoVerticalSliceBootstrap>();
        }

        private void Start()
        {
            mission = new VerticalSliceMission(new JsonFileSaveStore());
            mission.Initialize();
            BuildWorld();
            showIntro = mission.StateMachine.State == GameState.Intro;
        }

        private void BuildWorld()
        {
            RenderSettings.ambientLight = new Color(0.55f, 0.62f, 0.7f);
            MakePrimitive(PrimitiveType.Cube, "Island", new Vector3(0, -0.5f, 0), new Vector3(18, 1, 18), new Color(0.3f, 0.45f, 0.3f));
            MakePrimitive(PrimitiveType.Cube, "Source", new Vector3(-5, 0.4f, 4), new Vector3(1.4f, 0.8f, 1.4f), new Color(0.1f, 0.55f, 0.9f)).AddComponent<NexoInteractable>().Initialize(ClueId.Source, "Fonte alta: siga as marcas de umidade.");
            MakePrimitive(PrimitiveType.Cube, "Channel", new Vector3(-1, 0.25f, 2), new Vector3(3.5f, 0.3f, 0.5f), new Color(0.2f, 0.65f, 0.75f)).AddComponent<NexoInteractable>().Initialize(ClueId.Channel, "O canal aponta para o reservatório.");
            MakePrimitive(PrimitiveType.Cube, "Valve", new Vector3(2, 0.7f, 1), new Vector3(0.8f, 1.2f, 0.8f), new Color(0.8f, 0.65f, 0.2f)).AddComponent<NexoInteractable>().Initialize(ClueId.Valve, "Alinhe entrada e saída; há três posições.");
            MakePrimitive(PrimitiveType.Cube, "Reservoir", new Vector3(4, 1, 3), new Vector3(3, 2, 2), new Color(0.45f, 0.5f, 0.58f));
            MakePrimitive(PrimitiveType.Cube, "ExitBridge", new Vector3(7, 0, 3), new Vector3(4, 0.25f, 2), new Color(0.65f, 0.55f, 0.35f));
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
            GUI.Label(new Rect(16, 16, 520, 32), "NEXO | " + mission.StateMachine.State + " | Água: " + mission.Save.activeMission.water);
            if (showIntro)
            {
                GUI.Box(new Rect(30, 70, Screen.width - 60, 180), "O reservatório silencioso");
                GUI.Label(new Rect(55, 110, Screen.width - 110, 60), "Observe a fonte, siga o canal e restaure o fluxo.");
                if (GUI.Button(new Rect(55, 180, 180, 42), "Começar (pular prólogo)")) { mission.SkipIntro(); showIntro = false; }
                return;
            }
            if (GUI.Button(new Rect(16, Screen.height - 62, 70, 44), "Pausa")) { mission.Pause(); }
            if (GUI.Button(new Rect(Screen.width - 180, Screen.height - 62, 70, 44), "Interagir")) Interact();
            if (GUI.Button(new Rect(Screen.width - 95, Screen.height - 62, 70, 44), "Ajuda")) prompt = "Compare as marcas do canal e da válvula.";
            if (!string.IsNullOrEmpty(prompt)) GUI.Box(new Rect(16, 55, Screen.width - 32, 55), prompt);
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
            if (mission.StateMachine.State == GameState.Feedback)
            {
                GUI.Box(new Rect(30, Screen.height / 2 - 70, Screen.width - 60, 140), mission.LastFeedbackId);
                GUI.Label(new Rect(55, Screen.height / 2 - 25, Screen.width - 110, 45), mission.LastExplanation);
                if (GUI.Button(new Rect(55, Screen.height / 2 + 25, 180, 40), "Continuar")) mission.StateMachine.TryTransition(GameSignal.InspectionFinished);
            }
            if (mission.StateMachine.State == GameState.Paused)
            {
                GUI.Box(new Rect(40, 100, Screen.width - 80, 160), "Pausado");
                if (GUI.Button(new Rect(70, 160, 160, 42), "Retomar")) mission.Resume();
                if (GUI.Button(new Rect(250, 160, 160, 42), "Sair e salvar")) { mission.Pause(); Application.Quit(); }
            }
        }
    }

    public sealed class NexoInteractable : MonoBehaviour
    {
        public ClueId clue;
        public string description;
        public void Initialize(ClueId value, string text) { clue = value; description = text; }
    }
}
