using Assets.Project.Scripts.ShipFeature.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Project.Scripts
{
    public class WinWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _passengerCountText;
        [SerializeField] private TMP_Text _shipWeightText;

        [Inject] private ShipModel _shipModel;

        private void Start()
        {
            _passengerCountText.text = string.Format(_passengerCountText.text, _shipModel.PassengerCount);
            _shipWeightText.text = string.Format(_shipWeightText.text, _shipModel.ShipWeight);
        }
    }
}