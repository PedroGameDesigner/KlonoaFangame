using PlatformerRails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlonoaBehaviour : MonoBehaviour
{
    [SerializeField] private float _accelaration = 20f;
    [SerializeField] private float _drag = 5f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private float _gravity = 15f;
    [Space]
    [SerializeField] private float _groundDistance = 0.5f;
    [SerializeField] private float _groundCheckLength = 0.05f;
    [Space]
    [SerializeField] private float _minWalkSpeed = 0.1f;

    MoverOnRails _controller;

    public bool Grounded => CheckGroundDistance() != null;
    public bool Walking => Mathf.Abs(EffectiveSpeed.z) > _minWalkSpeed && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0;
    public Vector3 EffectiveSpeed => _controller.Velocity;
    public float Facing { get; private set; }

    void Awake()
    {
        _controller = GetComponent<MoverOnRails>();
    }

    void FixedUpdate()
    {
        //To make X value 0 means locate the character just above the rail
        _controller.Velocity.x = -_controller.Position.x * 5f;
        //Changing Z value in local position means moving toward rail direction
        _controller.Velocity.z += Input.GetAxisRaw("Horizontal") * _accelaration * Time.fixedDeltaTime;
        _controller.Velocity.z -= _controller.Velocity.z * _drag * Time.fixedDeltaTime;
        if (Walking) Facing = _controller.Velocity.z;

        //Y+ axis = Upwoard (depends on rail rotation)
        var distance = CheckGroundDistance();
        if (distance != null)
        {
            _controller.Velocity.y = (_groundDistance - distance.Value) / Time.fixedDeltaTime; //ths results for smooth move on slopes
            if (Input.GetButtonDown("Jump"))
                _controller.Velocity.y = _jumpSpeed + _groundDistance - distance.Value;
        }
        else
            _controller.Velocity.y -= _gravity * Time.fixedDeltaTime;
    }

    float? CheckGroundDistance()
    {
        RaycastHit info;
        var hit = Physics.Raycast(transform.position, -transform.up, out info, _groundDistance + _groundCheckLength);
        if (hit)
            return info.distance;
        else
            return null;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, Vector3.down * (_groundDistance + _groundCheckLength));
        Gizmos.DrawWireCube(Vector3.down * _groundDistance, Vector3.right / 2 + Vector3.forward / 2);
        Gizmos.matrix = Matrix4x4.identity;
    }
#endif
}

