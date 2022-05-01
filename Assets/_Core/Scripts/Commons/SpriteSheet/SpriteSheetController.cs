using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteSheets
{
    [ExecuteAlways]
    public abstract class SpriteSheetController : MonoBehaviour
    {
        [SerializeField] protected SpriteSheet spriteSheet;
        [SerializeField] protected new SpriteRenderer renderer;

        [Space]
        [SerializeField] protected int groupIndex;

        protected int previousGroupIndex;
        protected SpriteSheet previousSheet;
        protected SpriteGroup previousGroup;

        protected SpriteGroup CurrentGroup => spriteSheet.GetGroup(groupIndex);
        protected abstract int SpriteID{ get; }
        protected abstract int PreviousSpriteID { get; set; }

        private void Start()
        {
            Initialize();
            AssignFirstSprite();
        }

        public void ChangeSpriteSheet(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            Start();
        }

        protected virtual void Initialize()
        {
            previousSheet = spriteSheet;
            previousGroup = previousSheet != null ? previousSheet.GetGroup(groupIndex) : null;

            previousGroupIndex = groupIndex;
        }

        private void AssignFirstSprite()
        {
            if (renderer != null)
                renderer.sprite = previousGroup != null ?
                    previousGroup.GetSprite(SpriteID) : null;
        }

        private void Update()
        {
            if (renderer != null)
                UpdateSprites();
        }

        private void UpdateSprites()
        {
            OnNullSpriteSheet();
            OnSpriteSheetChange();
            OnGroupChange();
            OnSpriteChange();
        }

        private void OnNullSpriteSheet()
        {
            if (spriteSheet == null)
            {
                renderer.sprite = null;
                previousGroupIndex = -1;
                PreviousSpriteID = -1;
            }
        }

        private void OnSpriteSheetChange()
        {
            if (previousSheet != spriteSheet)
            {
                previousSheet = spriteSheet;
                previousGroupIndex = -1;
            }
        }

        private void OnGroupChange()
        {
            if (previousGroupIndex != groupIndex)
            {
                previousGroup = spriteSheet.GetGroup(groupIndex);
                previousGroupIndex = groupIndex;
                PreviousSpriteID = -1;
            }
        }

        protected abstract void OnSpriteChange();
    }
}