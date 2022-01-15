using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] SplineCharacterController _controller;
    [SerializeField] float _walkSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector2 speed = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _controller.Speed = speed.normalized * _walkSpeed;
    }
}
