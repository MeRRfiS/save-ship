using Assets.Project.Scripts.ActFeatures.Interfaces;

namespace Assets.Project.Scripts.ActFeatures
{
    public class ActableIndicatorHandler
    {
        private IActable _current;

        public IActable Current => _current;

        public void SetCurrent(IActable newActable)
        {
            if (_current == newActable) return;

            if (_current != null)
            {
                _current.StopIndicate();
                _current.OnDestroyObject -= ClearCurrent;
            }

            _current = newActable;

            if (_current != null)
            {
                _current.OnDestroyObject += ClearCurrent;
                _current.IndicateInteractable();
            }
        }

        private void ClearCurrent()
        {
            SetCurrent(null);
        }
    }
}
