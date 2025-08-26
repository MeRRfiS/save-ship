using System;
using UnityEngine;

namespace Assets.Project.Scripts.ShipFeature.Models
{
    public class ShipModel
    {
        private int _passengerCount;
        private float _weight;

        public int PassengerCount
        {
            get { return _passengerCount; }
            set
            {
                _passengerCount = value;
                OnPassengerChanged?.Invoke(_passengerCount);
                PlayerPrefs.SetInt("PassengerCount", _passengerCount);
            }
        }
        public float ShipWeight 
        {
            get { return _weight; }
            set 
            {
                _weight = (float)Math.Round(value, 1);
                OnWeightChanged?.Invoke(_weight);
                PlayerPrefs.SetFloat("ShipWeight", _weight);
            }
        }


        public event Action<int> OnPassengerChanged;
        public event Action<float> OnWeightChanged;
    }
}