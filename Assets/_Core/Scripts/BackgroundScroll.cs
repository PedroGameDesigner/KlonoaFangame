using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _directionReference;

    private Vector2 _offset;
    private float _tiling;
    private MeshRenderer _meshRenderer;
    private Material _material;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        _tiling = _material.GetTextureScale("_MainTex").x;
    }

    private void Update()
    {
        _offset -= (Time.deltaTime * _speed * GetDirection()) /_tiling;
        _material.SetTextureOffset("_MainTex", _offset);
    }

    private Vector2 GetDirection()
    {
        return _directionReference.localPosition.normalized;
    }
}
