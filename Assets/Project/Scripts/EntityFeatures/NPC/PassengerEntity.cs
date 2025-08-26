using Assets.Project.Scripts.Core.Managers;
using Assets.Project.Scripts.ShipFeature.Interfaces;
using System;

namespace Assets.Project.Scripts.EntityFeatures.NPC
{
    public class PassengerEntity : NPCEntity, IPassenger
    {
        public event Action<IPassenger> OnPassengerThrown;

        protected override void Start()
        {
            base.Start();
            _shipManager.AddPassenger(this);
        }

        protected override void OnDestroy()
        {
            if (UIManager.IsChangingScene)
            {
                return;
            }
            base.OnDestroy();
            OnPassengerThrown?.Invoke(this);
        }
    }
}