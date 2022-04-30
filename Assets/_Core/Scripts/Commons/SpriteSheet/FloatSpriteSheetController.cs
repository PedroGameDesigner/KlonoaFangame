using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteSheets
{
    [ExecuteAlways]
    public class FloatSpriteSheetController : SpriteSheetController
    {        
        [Range(0, 1), SerializeField] private float spriteID;

        private float prevSpriteID;

        protected override int SpriteID => 
            spriteID >= 1 ? 
            CurrentGroup.Length - 1 : 
            Mathf.FloorToInt(spriteID * CurrentGroup.Length);

        protected override int PreviousSpriteID
        {
            get { return Mathf.FloorToInt(prevSpriteID * CurrentGroup.Length); }
            set { prevSpriteID = value; }
        }

        protected override void Initialize()
        {
            base.Initialize();

            prevSpriteID = spriteID;
        }

        protected override void OnSpriteChange()
        {
            if (spriteID != prevSpriteID)
            {
                spriteID = spriteID % previousGroup.Length;
                renderer.sprite = previousGroup.GetSprite(spriteID);
                prevSpriteID = spriteID;
            }
        }
    }
}