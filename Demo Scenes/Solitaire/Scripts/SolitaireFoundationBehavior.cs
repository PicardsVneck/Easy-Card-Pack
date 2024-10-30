using UnityEngine;
using EasyCard.Deck52;

namespace EasyCard.Solitaire
{
public class SolitaireFoundationBehavior : EasyCardCollectionBehavior
{
    public override bool CanAddCard(EasyCard card, int index, EasyCardCollection collection)
    {
        if (card == null)
        {
            return false;
        }

        EasyCard topCard = collection.GetTopCard();
        EasyCard52URP card52 = card.GetComponent<EasyCard52URP>();

        if(!topCard)
        {
            if(card52.rank == Rank.Ace)
            {
                return true;
            }
            return false;
        }

        EasyCard52URP topCard52 = topCard.GetComponent<EasyCard52URP>();
        
        bool isCardNextRank = topCard52.rank + 1 == card52.rank;
        bool isSameSuit = card52.suit == topCard52.suit;

        return isCardNextRank && isSameSuit ;

    }

    public override bool CanRemoveCard(EasyCard card, EasyCardCollection collection)
    {
        return true;
    }
}

}
