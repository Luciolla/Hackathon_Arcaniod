using System;

namespace _Scripts.UI
{
    public class UIModel
    {
        public event Action OnGameStarted;
        public event Action OnMenuEnter;
        public event Action OnGameStopped;
        
        public bool IsGameRunning { get; private set; }

        public void StartGame()
        {
            IsGameRunning = true;
            OnGameStarted?.Invoke();
        }
        
        public void EnterMenu()
        {
            IsGameRunning = false;
            OnMenuEnter?.Invoke();
        }

        public void StopGame()
        {
            IsGameRunning = false;
            OnGameStopped?.Invoke();
        }
    }
}