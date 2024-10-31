using UnityEngine;
using EasyCard.Playing;

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
        EasyPlayingCardURP card52 = card.GetComponent<EasyPlayingCardURP>();

        if(!topCard)
        {
            if(card52.rank == Rank.Ace)
            {
                return true;
            }
            return false;
        }

        EasyPlayingCardURP topCard52 = topCard.GetComponent<EasyPlayingCardURP>();
        
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
