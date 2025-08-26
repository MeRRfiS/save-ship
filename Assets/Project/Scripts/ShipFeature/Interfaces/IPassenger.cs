using System;

namespace Assets.Project.Scripts.ShipFeature.Interfaces
{
    public interface IPassenger
    {
        public event Action<IPassenger> OnPassengerThrown;
    }
}
