using UnityEngine;

namespace EasyCardPack
{
    public abstract class EasyCardDefinition : ScriptableObject
    {
        public virtual EasyCard CreateCard()
        {
            return null;
        }
    }
}