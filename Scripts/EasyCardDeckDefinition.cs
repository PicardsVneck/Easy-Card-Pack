using System.Collections.Generic;
using UnityEngine;

namespace EasyCard
{

[CreateAssetMenu(fileName = "DeckDefinition", menuName = "Easy Card Pack/Deck Definition", order = 1)]
public class EasyCardDeckDefinition : ScriptableObject
{
    public List<EasyCardDefinition> CardDefinitions;

    public List<EasyCard> buildDeck()
    {
        List<EasyCard> deck = new List<EasyCard>();
        foreach (EasyCardDefinition cardDefinition in CardDefinitions)
        {
            if(cardDefinition == null)
            {
                continue;
            }

            EasyCard newCard = cardDefinition.CreateCard();
            if(newCard != null)
            {
                deck.Add(newCard);
            }
        }
        return deck;
    }

}

}
