using InputControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Klonoa
{
    public class KlonoaInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private KlonoaBehaviour _mainBehaviour;
        private PlayerControl _control;
        private InputAction _moveInput;


        private void Awake()
        {
            _control = new PlayerControl();
            _mainBehaviour = GetComponent<KlonoaBehaviour>();
        }

        private void OnEnable()
        {
            _moveInput = _control.Player.Move;
            _control.Player.Jump.started += OnJumpStarted;
            _control.Player.Jump.canceled += OnJumpCanceled;

            _control.Player.Attack.started += OnAttackStarted;
            _control.Player.Attack.canceled += OnAttackCanceled;

            _control.Player.Enable();
        }

        private void Update()
        {
            Vector2 correctDirection = GetCorrectDirection(_moveInput.ReadValue<Vector2>());
            _mainBehaviour.MoveDirection = correctDirection;
        }

        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            _mainBehaviour.StartJump();
        }

        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            _mainBehaviour.EndJump();
        }

        private void OnAttackStarted(InputAction.CallbackContext context)
        {
            _mainBehaviour.StartAttack();
        }

        private void OnAttackCanceled(InputAction.CallbackContext context)
        {
            _mainBehaviour.StopAttack();
        }

        private Vector2 GetCorrectDirection(Vector2 input)
        {
            Vector3 forward = _camera.transform.forward;
            Vector3 right = _camera.transform.right;
            Vector3 correctedDirection = forward * input.y + right * input.x;
            return (new Vector2(correctedDirection.x, correctedDirection.z)).normalized;
        }

        private void OnDisable()
        {
            _control.Player.Jump.started -= OnJumpStarted;
            _control.Player.Jump.canceled -= OnJumpCanceled;

            _control.Player.Attack.started -= OnAttackStarted;
            _control.Player.Attack.canceled -= OnAttackCanceled;

            _control.Player.Disable();
        }
    }
}