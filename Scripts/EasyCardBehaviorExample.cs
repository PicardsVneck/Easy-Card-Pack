using UnityEngine;
using EasyCardPack;

[AddComponentMenu("Easy Card Pack/Example Collection Behavior")]
public class EasyCardBehaviorExample : EasyCardCollectionBehavior
{
    public override bool CanAddCard(EasyCardPack.EasyCard card, int index, EasyCardCollection collection)
    {
        return true;
    }

    public override bool CanRemoveCard(EasyCardPack.EasyCard card, EasyCardCollection collection)
    {
        return true;
    }
}
