using Gameplay.Klonoa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBehaviour : MonoBehaviour
{
    [SerializeField] private KlonoaBehaviour _klonoa;
    [SerializeField] private float _triggerDistance;
    [SerializeField] private float _autoFadeTime;
    [SerializeField] private Animator _animator;

    private bool _triggered;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        if (!_triggered)
        {
            float distance = (_klonoa.transform.position - transform.position).magnitude;
            _timer += Time.deltaTime;
            if (distance < _triggerDistance || _timer >= _autoFadeTime)
            {
                _animator.SetTrigger("Activate");
                _triggered = true;
            }
        }
    }
}
