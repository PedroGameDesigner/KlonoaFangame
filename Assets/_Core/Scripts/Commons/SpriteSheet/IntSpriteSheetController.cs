using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteSheets
{
    [ExecuteAlways]
    public class IntSpriteSheetController : SpriteSheetController
    {
        [SerializeField] private int spriteID;

        protected override int SpriteID => Mathf.Clamp(spriteID, 0, CurrentGroup.Length);
        protected override int PreviousSpriteID { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            PreviousSpriteID = spriteID;
        }

        protected override void OnSpriteChange()
        {
            if (spriteID != PreviousSpriteID)
            {
                spriteID %= previousGroup.Length;
                renderer.sprite = previousGroup.GetSprite(spriteID);
                PreviousSpriteID = spriteID;
            }
        }
    }
}