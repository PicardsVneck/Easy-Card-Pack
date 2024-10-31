using System.Collections.Generic;
using UnityEngine;

namespace EasyCard
{

public class EasyCardDeckPopulator : MonoBehaviour
{
    public EasyCardDeckDefinition deckDefinition;
    public bool shuffled = true;

    private void Awake()
    {
        InitializeDeck();

        if (shuffled)
        {
            Shuffle();
        }
    }

    public void InitializeDeck()
    {
        EasyCardCollection collection = GetComponent<EasyCardCollection>();

        if (deckDefinition == null)
        {
            Debug.LogError("EasyCard Deck Definition required");
            return;
        }

        if (collection == null)
        {
            Debug.LogError("No EasyCardCollection found on GameObject");
            return;
        }

        List<EasyCard> cards = deckDefinition.buildDeck();
        foreach (EasyCard card in cards)
        {
            collection.AddCard(card, force : true, instantanious : true);
        }
        collection.UpdateCardPositions(instantanious: true);
    }

    public void Shuffle()
    {
        EasyCardCollection collection = GetComponent<EasyCardCollection>();

        if (collection == null)
        {
            Debug.LogError("No EasyCardCollection found on GameObject");
            return;
        }
        
        collection.Shuffle();   
    }
}

}
