using Assets.Project.Scripts.ShipFeature.Interfaces;

namespace Assets.Project.Scripts.Core.Interfaces
{
    public interface IShipManager
    {
        public float TotalWeight { get; }

        public void AddWeightObject(IWeight weightObj);
        public void AddPassenger(IPassenger passenger);
    }
}
