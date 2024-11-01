using UnityEngine;
using EasyCardPack.Playing;

namespace EasyCardPack.Solitaire
{

[AddComponentMenu("Easy Card Pack/Solitaire/Tableau Behavior")]
public class SolitaireTableauBehavior : EasyCardCollectionBehavior
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
            if(card52.rank == Rank.King)
            {
                return true;
            }
            return false;
        }

        EasyPlayingCardURP topCard52 = topCard.GetComponent<EasyPlayingCardURP>();
        
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
