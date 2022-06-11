using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Shadow : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Projector _projector;

    private Material _material;

    private Material Material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(_projector.material);
                _projector.material = _material;
            }
            return _material;
        }
    }

    public void OnValidate()
    {
        Material.SetTexture("_MainTex", _sprite.texture);
    }
}
