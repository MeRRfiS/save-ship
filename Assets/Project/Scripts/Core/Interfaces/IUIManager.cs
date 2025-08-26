using UnityEngine;

namespace Assets.Project.Scripts.Core.Interfaces
{
    public interface IUIManager
    {
        public void UpdateWeightText(float weight);
        public void UpdatePassengerText(int amount);
    }
}