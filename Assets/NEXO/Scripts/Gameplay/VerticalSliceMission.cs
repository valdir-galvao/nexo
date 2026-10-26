using System;
using Nexo.Core;
using Nexo.Persistence;
using Nexo.Progression;

namespace Nexo.Gameplay
{
    public sealed class VerticalSliceMission
    {
        public const string MissionId = "mission-silent-reservoir";
        public const int StartingWater = 6;
        private readonly ISaveStore saveStore;
        private readonly MissionStateMachine stateMachine;
        private readonly SaveData save;

        public MissionStateMachine StateMachine { get { return stateMachine; } }
        public SaveData Save { get { return save; } }
        public string LastFeedbackId { get; private set; }
        public string LastExplanation { get; private set; }

        public VerticalSliceMission(ISaveStore store)
        {
            saveStore = store;
            save = store.Load() ?? new SaveData();
            if (save.activeMission == null || save.activeMission.missionId != MissionId)
                save.activeMission = NewState();
            stateMachine = new MissionStateMachine();
        }

        public bool Initialize()
        {
            if (!stateMachine.TryTransition(GameSignal.Initialized)) return false;
            save.activeMission.started = true;
            save.activeMission.checkpoint = "intro";
            return SaveNow();
        }

        public bool SkipIntro()
        {
            return stateMachine.TryTransition(GameSignal.SkipIntro) && Checkpoint("exploration");
        }

        public bool RecordClue(ClueId clue)
        {
            if (stateMachine.State != GameState.Exploring && stateMachine.State != GameState.Inspecting) return false;
            string value = clue.ToString();
            string[] current = save.activeMission.discoveredClueIds ?? new string[0];
            for (int i = 0; i < current.Length; i++) if (current[i] == value) return true;
            string[] updated = new string[current.Length + 1];
            Array.Copy(current, updated, current.Length);
            updated[current.Length] = value;
            save.activeMission.discoveredClueIds = updated;
            return Checkpoint("clue-" + value.ToLowerInvariant());
        }

        public bool EvaluateValve(int valveIndex)
        {
            if (!stateMachine.TryTransition(GameSignal.PuzzleRequested)) return false;
            RuleFeedback feedback = VerticalSliceRules.EvaluateValve(new WaterAttempt { valveIndex = valveIndex });
            save.activeMission.valveIndex = valveIndex;
            LastFeedbackId = feedback.feedbackId;
            LastExplanation = feedback.explanation;
            stateMachine.TryTransition(GameSignal.AttemptResolved);
            return Checkpoint(feedback.success ? "valve-repaired" : "valve-attempt");
        }

        public bool Allocate(WaterAllocation allocation)
        {
            if (!MissionRuleEngine.CanAllocate(save.activeMission.water, allocation)) return false;
            save.activeMission.water = MissionRuleEngine.ApplyAllocation(save.activeMission.water, allocation);
            save.activeMission.allocation = allocation;
            RuleFeedback feedback = VerticalSliceRules.EvaluateWater(new WaterAttempt { allocation = allocation });
            LastFeedbackId = feedback.feedbackId;
            LastExplanation = feedback.explanation;
            return Checkpoint("allocation-confirmed");
        }

        public bool Complete()
        {
            if (save.activeMission.completed || !MissionRuleEngine.CanCompleteReservoir(save.activeMission.water, save.activeMission.allocation, save.activeMission.valveIndex)) return false;
            if (!stateMachine.TryTransition(GameSignal.MissionFinished)) return false;
            save.activeMission.completed = true;
            save.activeMission.checkpoint = "complete";
            save.completedMissionIds = new[] { MissionId };
            return SaveNow();
        }

        public bool Pause() { return stateMachine.TryTransition(GameSignal.PauseRequested) && SaveNow(); }
        public bool Resume() { return stateMachine.TryTransition(GameSignal.ResumeRequested); }

        private MissionRuntimeState NewState()
        {
            return new MissionRuntimeState { missionId = MissionId, phase = MissionPhase.Arrival, water = StartingWater, valveIndex = 0 };
        }

        private bool Checkpoint(string checkpoint)
        {
            save.activeMission.checkpoint = checkpoint;
            return SaveNow();
        }

        private bool SaveNow() { return saveStore.Save(save); }
    }
}
