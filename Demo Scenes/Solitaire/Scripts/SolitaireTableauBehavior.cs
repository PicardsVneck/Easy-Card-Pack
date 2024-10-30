using EasyCard.Deck52;
using UnityEngine;

namespace EasyCard.Solitaire
{
public class SolitaireTableauBehavior : EasyCardCollectionBehavior
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
            if(card52.rank == Rank.King)
            {
                return true;
            }
            return false;
        }

        EasyCard52URP topCard52 = topCard.GetComponent<EasyCard52URP>();
        
        bool isTopCardRed = topCard52.suit == Suit.Diamonds || topCard52.suit == Suit.Hearts;
        bool isCardRed = card52.suit == Suit.Diamonds || card52.suit == Suit.Hearts;
        bool isCardNextRank = topCard52.rank == card52.rank + 1;
        bool isCardDifferentColor = isTopCardRed != isCardRed;

        return isCardNextRank && isCardDifferentColor;

    }

    public override bool CanRemoveCard(EasyCard card, EasyCardCollection collection)
    {
        return true;
    }
}

}
