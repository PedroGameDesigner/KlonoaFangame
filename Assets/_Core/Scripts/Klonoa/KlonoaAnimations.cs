using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlonoaAnimations : MonoBehaviour
{
    [SerializeField] private KlonoaBehaviour _behaviour = null;
    [Header("Animator Parameter")]
    [SerializeField] private string _groundedParameter = "Grounded";
    [SerializeField] private string _walkingParameter = "Walking";
    [SerializeField] private string _ySpeedParameter = "YSpeed";

    private SpriteRenderer _renderer = null;
    private Animator _animator = null;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(_groundedParameter, _behaviour.Grounded);
        _animator.SetBool(_walkingParameter, _behaviour.Walking);
        _animator.SetFloat(_ySpeedParameter, _behaviour.EffectiveSpeed.y);
        _renderer.flipX = _behaviour.Facing < 0;
    }
}
