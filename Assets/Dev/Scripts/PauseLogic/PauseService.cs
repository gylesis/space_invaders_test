using System.Collections.Generic;

namespace Dev.PauseLogic
{
    public class PauseService
    {
        public static PauseService Instance;

        private HashSet<IPauseListener> _listeners = new HashSet<IPauseListener>();

        public PauseService()
        {
            Instance = this;
        }

        public void RegisterListener(IPauseListener pauseListener)
        {
            _listeners.Add(pauseListener);
        }

        public void RemoveListener(IPauseListener pauseListener)
        {
            _listeners.Remove(pauseListener);
        }

        public void SetPause(bool isPause)
        {
            foreach (IPauseListener pauseListener in _listeners)
            {
                pauseListener.OnPause(isPause);
            }
        }
    }
}