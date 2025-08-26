using Assets.Project.Scripts.Core.Interfaces;
using Assets.Project.Scripts.ShipFeature.Interfaces;
using Assets.Project.Scripts.ShipFeature.Models;
using UnityEngine;
using Zenject;

namespace Assets.Project.Scripts.Core.Managers
{
    public class ShipManager : MonoBehaviour, IShipManager
    {
        [Inject] private ShipModel _shipModel;

        [Inject] private IUIManager _uiManager;

        public float TotalWeight { get { return _shipModel.ShipWeight; } }

        private void Awake()
        {
            _shipModel.OnWeightChanged += _uiManager.UpdateWeightText;
            _shipModel.OnPassengerChanged += _uiManager.UpdatePassengerText;

            _shipModel.ShipWeight = 1000;
            _shipModel.PassengerCount = 0;
        }

        public void AddWeightObject(IWeight weightObj)
        {
            _shipModel.ShipWeight += weightObj.Weight;
            weightObj.OnWeightChanged += ReduceWeight;
        }

        private void ReduceWeight(IWeight weightObj)
        {
            _shipModel.ShipWeight -= weightObj.Weight;
            weightObj.OnWeightChanged -= ReduceWeight;
        }

        public void AddPassenger(IPassenger passenger)
        {
            _shipModel.PassengerCount++;
            passenger.OnPassengerThrown += ReducePassenger;
        }

        private void ReducePassenger(IPassenger passenger)
        {
            passenger.OnPassengerThrown -= ReducePassenger;
            _shipModel.PassengerCount--;
        }
    }
}