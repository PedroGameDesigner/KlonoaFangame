using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class LayerMaskExtension
    {
        public static bool CheckLayer(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}
