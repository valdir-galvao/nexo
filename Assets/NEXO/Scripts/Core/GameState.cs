namespace Nexo.Core
{
    public enum GameState
    {
        Initializing,
        Intro,
        Exploring,
        Inspecting,
        PuzzleActive,
        Feedback,
        MissionComplete,
        Paused
    }

    public enum GameSignal
    {
        Initialized,
        IntroFinished,
        SkipIntro,
        InspectRequested,
        InspectionFinished,
        PuzzleRequested,
        AttemptResolved,
        MissionFinished,
        PauseRequested,
        ResumeRequested,
        QuitRequested
    }
}
