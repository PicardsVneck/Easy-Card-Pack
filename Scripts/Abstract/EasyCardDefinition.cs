using UnityEngine;

namespace EasyCard
{
    public abstract class EasyCardDefinition : ScriptableObject
    {
        public virtual EasyCard CreateCard()
        {
            return null;
        }
    }
}