using UnityEngine;
using Sirenix;

namespace SpriteSheets
{
    [CreateAssetMenu(fileName = "New Sprite Sheet", menuName = "Sprite Sheet", order = 1)]
    public class SpriteSheet : ScriptableObject
    {
        [SerializeField] private SpriteGroup[] groups;
        [Space]
        [SerializeField] private string defaultTag;
        
        public SpriteGroup GetGroup(string tag)
        {
            foreach (SpriteGroup group in groups)
            {
                if (tag == group.Tag)
                    return group;
            }

            return GetGroup(defaultTag);
        }

        public SpriteGroup GetGroup(int index)
        {
            foreach (SpriteGroup group in groups)
            {
                if (index == group.Index)
                    return group;
            }

            return GetGroup(defaultTag);
        }


        private Sprite GetSprite(SpriteGroup group, int spriteIndex)
        {
            if (group == null)
                return GetSprite(GetGroup(defaultTag), spriteIndex);
            else
                return group.GetSprite(spriteIndex);
        }
    }
}
