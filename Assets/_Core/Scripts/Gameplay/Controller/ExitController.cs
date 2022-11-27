using InputControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Controller
{
    public class ExitController : MonoBehaviour
    {
        [SerializeField] private float _exitTime = 5f;
        [SerializeField] private float _revertMultiplier = 2.5f;

        private PlayerControl _control;
        private bool _exiting;
        private bool _exitComplete;
        private float _exitingTimer;

        public float ExitingPercent => _exitingTimer / _exitTime;


        private void Awake()
        {
            _control = new PlayerControl();
        }

        private void OnEnable()
        {
            _control.Menu.Exit.started += OnExitPressed;
            _control.Menu.Exit.canceled += OnExitExit;

            _control.Menu.Enable();
        }

        private void Update()
        {
            if (!_exitComplete)
            {
                float deltaTime = Time.deltaTime;
                if (_exiting) UpdateExit(deltaTime);
                else if (_exitingTimer > 0) UpdateRevertTimer(deltaTime);
            }
        }

        private void UpdateExit(float deltaTime)
        {
            _exitingTimer += deltaTime;

            if (_exitingTimer >= _exitTime)
            {
                _exitComplete = true;
                Application.Quit();
            }
        }

        private void UpdateRevertTimer(float deltaTime)
        {
            _exitingTimer -= deltaTime * _revertMultiplier;
        }

        private void OnExitPressed(InputAction.CallbackContext context)
        {
            _exiting = true;
        }

        private void OnExitExit(InputAction.CallbackContext context)
        {
            _exiting = false;
        }

        private void OnDisable()
        {
            _control.Menu.Exit.started -= OnExitPressed;
            _control.Menu.Exit.canceled -= OnExitExit;

            _control.Menu.Disable();
        }
    }
}