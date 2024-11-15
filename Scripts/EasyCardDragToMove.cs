using System.Collections.Generic;
using UnityEngine;

namespace EasyCardPack
{
[RequireComponent(typeof(EasyCardEventSystem))]
[AddComponentMenu("Easy Card Pack/Drag-to-Move Manager")]

public class EasyCardDragToMove : EasyCardEventHandler
{
    EasyCard selectedCard = null;

    void Awake()
    {

    }

    void Update()
    {

    }

    protected override void OnCardClicked(EasyCardEventHits hits)
    {
        selectedCard = hits.hitCards[0];

        if(!selectedCard)
        {
            return;
        }


    }

    protected override void OnCardReleased(EasyCardEventHits hits)
    {
        if(!selectedCard)
        {
            return;
        }

        EasyCardCollection originalCollection = selectedCard.Collection;
        EasyCardCollection newCollection = hits.hitCollections[0];

        if(originalCollection == null || newCollection == null || originalCollection == newCollection)
        {
            selectedCard = null;
            return;
        }
        
        if(originalCollection.CanRemoveCard(selectedCard) && newCollection.CanAddCard(selectedCard, 0))
        {
            originalCollection.RemoveCard(selectedCard);
            newCollection.AddCard(selectedCard);
        }
    }

    protected override void OnCardHoverEnter(EasyCard card)
    { 
        EasyCardCollection collection = card.Collection;

        if(collection == null)
        {
            card.EnableHighLight(true);
            return;
        } 

        if(collection.multiCardSelect)
        {
            int index = collection.GetCardIndex(card);
            for (int i = index; i < collection.cards.Count; i++)
            {
                collection.GetCard(i).EnableHighLight(true);
            }
            return;
        }

        if(collection.canRemoveCards && collection.onlyRemoveTopCard && collection.GetTopCard() == card)
        {
            collection.GetTopCard().EnableHighLight(true);
            return;
        }
        
        if(collection.canRemoveCards && !collection.onlyRemoveTopCard)
        {
            card.EnableHighLight(true);
            return;
        }
    }

    protected override void OnCardHoverExit(EasyCard card)
    {
        EasyCardCollection collection = card.Collection;

        if(collection == null)
        {
            card.EnableHighLight(false);
            return;
        } 

        foreach (EasyCard c in collection.cards)
        {
            c.EnableHighLight(false);
        }
    }

}

}
