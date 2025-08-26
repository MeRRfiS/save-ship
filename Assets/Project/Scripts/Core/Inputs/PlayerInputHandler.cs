using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Project.Scripts.Core.Inputs
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _input;

        public UnityEvent OnAct = new UnityEvent();
        public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void OnEnable()
        {
            _input.PLayer.Moving.performed += HandleMove;
            _input.PLayer.Moving.canceled += HandleStopMoving;
            _input.PLayer.Act.performed += HandleAct;

            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.PLayer.Moving.performed -= HandleMove;
            _input.PLayer.Moving.canceled -= HandleStopMoving;
            _input.PLayer.Act.performed -= HandleAct;
        }

        private void HandleMove(InputAction.CallbackContext callback)
        {
            var inputValue = callback.ReadValue<Vector2>();
            var direction = inputValue.normalized;

            OnMove?.Invoke(direction);
        }

        private void HandleStopMoving(InputAction.CallbackContext callback)
        {
            OnMove?.Invoke(Vector2.zero);
        }

        private void HandleAct(InputAction.CallbackContext callback)
        {
            OnAct?.Invoke();
        }
    }
}