using System;

namespace Nexo.Core
{
    public sealed class MissionStateMachine
    {
        public GameState State { get; private set; }
        public event Action<GameState, GameState> StateChanged;

        public MissionStateMachine()
        {
            State = GameState.Initializing;
        }

        public bool TryTransition(GameSignal signal)
        {
            GameState next;
            if (!TryGetNext(State, signal, out next)) return false;
            if (next == State) return true;
            GameState previous = State;
            State = next;
            if (StateChanged != null) StateChanged(previous, next);
            return true;
        }

        public static bool TryGetNext(GameState current, GameSignal signal, out GameState next)
        {
            next = current;
            switch (current)
            {
                case GameState.Initializing:
                    if (signal == GameSignal.Initialized) next = GameState.Intro;
                    break;
                case GameState.Intro:
                    if (signal == GameSignal.IntroFinished || signal == GameSignal.SkipIntro) next = GameState.Exploring;
                    else if (signal == GameSignal.PauseRequested) next = GameState.Paused;
                    break;
                case GameState.Exploring:
                    if (signal == GameSignal.InspectRequested) next = GameState.Inspecting;
                    else if (signal == GameSignal.PuzzleRequested) next = GameState.PuzzleActive;
                    else if (signal == GameSignal.PauseRequested) next = GameState.Paused;
                    break;
                case GameState.Inspecting:
                    if (signal == GameSignal.InspectionFinished) next = GameState.Exploring;
                    else if (signal == GameSignal.PuzzleRequested) next = GameState.PuzzleActive;
                    else if (signal == GameSignal.PauseRequested) next = GameState.Paused;
                    break;
                case GameState.PuzzleActive:
                    if (signal == GameSignal.AttemptResolved) next = GameState.Feedback;
                    else if (signal == GameSignal.PauseRequested) next = GameState.Paused;
                    break;
                case GameState.Feedback:
                    if (signal == GameSignal.MissionFinished) next = GameState.MissionComplete;
                    else if (signal == GameSignal.InspectionFinished) next = GameState.Exploring;
                    else if (signal == GameSignal.PuzzleRequested) next = GameState.PuzzleActive;
                    else if (signal == GameSignal.PauseRequested) next = GameState.Paused;
                    break;
                case GameState.MissionComplete:
                    break;
                case GameState.Paused:
                    if (signal == GameSignal.ResumeRequested) next = GameState.Exploring;
                    break;
            }
            return next != current || (current == GameState.Intro && signal == GameSignal.PauseRequested) ||
                   (current == GameState.Exploring && signal == GameSignal.PauseRequested) ||
                   (current == GameState.Inspecting && signal == GameSignal.PauseRequested) ||
                   (current == GameState.PuzzleActive && signal == GameSignal.PauseRequested) ||
                   (current == GameState.Feedback && signal == GameSignal.PauseRequested);
        }
    }
}
