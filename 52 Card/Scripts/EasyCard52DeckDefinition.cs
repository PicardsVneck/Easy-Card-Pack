using System.Collections.Generic;
using UnityEngine;

namespace EasyCard.Deck52
{

[CreateAssetMenu(fileName = "52DeckDefinition", menuName = "Easy Card Pack/52 Deck Definition", order = 1)]
public class EasyCard52DeckDefinition : ScriptableObject, IEasyCardDeckDefinition
{
    public List<EasyCard52Definition> cardDefinitions;
    public EasyCard cardPrefab;
    public Material cardBackMaterial { get; internal set; }

    public List<EasyCard> buildDeck()
    {
        List<EasyCard> deck = new List<EasyCard>();
        foreach (EasyCard52Definition cardDefinition in cardDefinitions)
        {
            EasyCard newCard = Instantiate(cardPrefab);
            newCard.name = cardDefinition.name;
            EasyCard52URP newCard52 = newCard.GetComponent<EasyCard52URP>();

            newCard52.Initialize(cardDefinition);

            deck.Add(newCard);
        }
        return deck;
    }

}

}
