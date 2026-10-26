using System;
using System.Collections.Generic;
using Nexo.Core;
using Nexo.Learning;

namespace Nexo.Progression
{
    public enum MissionPhase { Arrival, Exploration, Interaction, Feedback, Completion }
    public enum WaterAllocation { None, Reservoir, Garden }

    [Serializable]
    public sealed class MissionDefinition : IMissionDefinition
    {
        public string id;
        public string[] objectiveIds = new string[0];
        public string[] clueIds = new string[0];

        public string Id { get { return id ?? string.Empty; } }
        public IReadOnlyList<string> ObjectiveIds { get { return objectiveIds; } }
    }

    [Serializable]
    public sealed class MissionRuntimeState : IMissionRuntimeState
    {
        public string missionId;
        public MissionPhase phase;
        public int water;
        public string[] discoveredClueIds = new string[0];
        public WaterAllocation allocation;
        public int valveIndex;
        public string checkpoint;
        public bool started;
        public bool completed;

        public string MissionId { get { return missionId ?? string.Empty; } }
        public int Water { get { return water; } }
    }

    [Serializable]
    public sealed class SaveData : ISaveData
    {
        public int schemaVersion = 1;
        public ContentVersion contentVersion = new ContentVersion();
        public string[] completedMissionIds = new string[0];
        public MissionRuntimeState activeMission;
        public MasteryEvidence[] masteryEvidence = new MasteryEvidence[0];
        public ReviewItem[] scheduledReviews = new ReviewItem[0];
        public AccessibilitySettings accessibility = new AccessibilitySettings();
        public string checksum;

        public int SchemaVersion { get { return schemaVersion; } }
        public ContentVersion ContentVersion { get { return contentVersion; } }
    }

    [Serializable]
    public sealed class ReviewItem
    {
        public string objectiveId;
        public int dueAfterMission;
    }

    [Serializable]
    public sealed class AccessibilitySettings
    {
        public int textScale = 1;
        public bool subtitles = true;
        public bool reducedMotion;
        public bool vibration = true;
    }

    public interface IMissionDefinition
    {
        string Id { get; }
        IReadOnlyList<string> ObjectiveIds { get; }
    }

    public interface IMissionRuntimeState
    {
        string MissionId { get; }
        int Water { get; }
    }

    public interface ISaveData
    {
        int SchemaVersion { get; }
        ContentVersion ContentVersion { get; }
    }
}
