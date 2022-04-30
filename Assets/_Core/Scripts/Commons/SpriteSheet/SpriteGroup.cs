using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteSheets
{
    [Serializable]
    public class SpriteGroup
    {
        [SerializeField] private string _tag;
        [SerializeField] private int _index;
        [SerializeField] private Sprite[] _sprites;

        public string Tag => _tag;
        public int Index => _index;
        public int Length => _sprites.Length;

        public Sprite GetSprite(int spriteID)
        {
            return _sprites[spriteID % _sprites.Length];
        }

        public Sprite GetSprite(float floatID)
        {
            int intId = floatID >= 1 ? _sprites.Length - 1 :
                Mathf.FloorToInt(floatID * _sprites.Length);
            return _sprites[intId];
        }
    }
}