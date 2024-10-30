using EasyCard;
using UnityEngine;

public class EasyCardBehaviorExample : EasyCardCollectionBehavior
{
    public override bool CanAddCard(EasyCard.EasyCard card, int index, EasyCardCollection collection)
    {
        return true;
    }

    public override bool CanRemoveCard(EasyCard.EasyCard card, EasyCardCollection collection)
    {
        return true;
    }
}
