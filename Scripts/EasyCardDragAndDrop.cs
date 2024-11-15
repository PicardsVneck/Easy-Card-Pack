using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace EasyCardPack
{

[RequireComponent(typeof(EasyCardEventSystem))]
[AddComponentMenu("Easy Card Pack/Drag-and-Drop Manager")]
public class EasyCardDragAndDrop : EasyCardEventHandler
{
    public Vector3 interactionPlaneNormal = Vector3.forward;
    public Vector3 interactionPlanePosition = Vector3.zero;

    private List<EasyCard> dragCards = null;
    private List<Vector3> dragCardOffsets = null;
    private EasyCardCollection originalCardCollection = null;
    private int originalIndex;
    private Plane plane;

    void Awake()
    {
        plane = new Plane(interactionPlaneNormal, interactionPlanePosition);
        dragCards = new List<EasyCard>();
        dragCardOffsets = new List<Vector3>();
    }

    void Update()
    {
        if (dragCards.Count == 0)
        {
            return;
        }

        foreach (EasyCard card in dragCards)
        {
            card.MoveTo(GetMousePositionInteractionPlane() + dragCardOffsets[dragCards.IndexOf(card)], Quaternion.identity);
            card.EnableHighLight(true);
        }
    }

    Vector3 GetMousePositionInteractionPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float distance);
        Vector3 point = ray.GetPoint(distance);
        return point;
    }

    protected override void OnCardClicked(EasyCardEventHits hits)
    {
        dragCards.Clear();
        dragCardOffsets.Clear();

        if (hits.hitCards.Count == 0)
        {
            return;
        }

        EasyCard clickedCard = hits.hitCards[0];
        originalCardCollection = clickedCard.Collection;

        if (originalCardCollection == null)
        {
            PickupCard(null, clickedCard, clickedCard.transform.position, 0);
            return;
        }

        originalIndex = originalCardCollection.GetCardIndex(clickedCard);
        int endIndex = originalCardCollection.cards.Count;

        if(!originalCardCollection.multiCardSelect)
        {
            endIndex = originalIndex + 1;
        }

        for (int i = originalIndex; i < endIndex; i++)
        {
            PickupCard(originalCardCollection, null, clickedCard.transform.position, i);
        }

        for (int i = originalIndex; i < endIndex; i++)
        {
            originalCardCollection.RemoveCardAt(originalIndex);
        }

        EasyCardEvents.OnDrag(dragCards, originalCardCollection);

    }

    protected override void OnCardReleased(EasyCardEventHits hits)
    {
        if (dragCards.Count == 0)
        {
            return;
        }

        if(hits.hitCollections.Count == 0)
        {
            ReturnCards();
            return;
        }

        if (hits.hitCollections.Count != 0)
        {
            EasyCardCollection newCardCollection = hits.hitCollections[0];
            int addCardIndex = newCardCollection.GetClosestCardIndexByPosition(dragCards[0].transform.position);
            bool canAddAllCards = true;

            EasyCardEvents.OnDrop(dragCards, newCardCollection, originalCardCollection);
            for (int i = 0; i < dragCards.Count; i++)
            {
                bool canAddCard = newCardCollection.AddCard(dragCards[i], i + addCardIndex);
                if(i != 0)
                {
                    dragCards[i].EnableHighLight(false);
                }
                
                if(!canAddCard)
                {
                    canAddAllCards = false;
                }
            }

            if (!canAddAllCards)
            {
                ReturnCards();
            }

            dragCards.Clear();

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

    private void PickupCard(EasyCardCollection collection, EasyCard card, Vector3 clickedCardPosition, int index)
    {
        if(collection == null)
        {
            originalCardCollection = null;
            dragCardOffsets.Add(Vector3.zero);
            dragCards.Add(card);
            card.EnableHighLight(true);
            return;
        }

        card = collection.GetCard(index);
        if(collection.CanRemoveCard(card))
        {
            dragCardOffsets.Add(card.transform.position - clickedCardPosition);
            dragCards.Add(card);
            card.EnableHighLight(true);
            return;
        }
    }

    private void ReturnCards()
    {
        if (originalCardCollection == null)
        {
            dragCards.Clear();
            return;
        }

        for (int i = 0; i < dragCards.Count; i++)
        {
            ReturnCard(dragCards[i]);
        }
        dragCards.Clear();
    }

    private void ReturnCard(EasyCard card)
    {
        if (originalCardCollection == null)
        {
            return;
        }
        if(card.Collection != null)
        {
            card.Collection.RemoveCard(card);
            card.EnableHighLight(false);
        }
        originalCardCollection.AddCard(card, originalIndex, force : true);
        originalIndex++;
    }
}

}
