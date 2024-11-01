using System.Collections.Generic;
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

        EasyCard clickedCard = hits.hitCards[0];
        originalCardCollection = clickedCard.Collection;

        if (originalCardCollection == null)
        {
            Debug.Log(clickedCard.name + " is not in a collection.");
            PickupCard(null, clickedCard, clickedCard, 0);
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
            PickupCard(originalCardCollection, null, clickedCard, originalIndex);
        }

        EasyCardEvents.OnDrag(dragCards, originalCardCollection);

    }

    protected override void OnCardReleased(EasyCardEventHits hits)
    {
        if (dragCards.Count == 0)
        {
            return;
        }
        
        if (hits.hitCollections.Count != 0)
        {
            EasyCardCollection newCardCollection = hits.hitCollections[0];
            int addCardIndex = newCardCollection.GetClosestCardIndexByPosition(dragCards[0].transform.position);
            for (int i = 0; i < dragCards.Count; i++)
            {
                EasyCard card = dragCards[i];
                bool wasAdded = newCardCollection.AddCard(card, i + addCardIndex);
                if (!wasAdded)
                {
                    ReturnCard(card);
                }
            }
            EasyCardEvents.OnDrop(dragCards, newCardCollection, originalCardCollection);
        }
        
        if (dragCards[0].Collection == null)
        {
            ReturnCards();
        }
        
        dragCards.Clear();
    }

    private void PickupCard(EasyCardCollection collection, EasyCard card, EasyCard clickedCard, int index)
    {
        if(collection != null)
        {
            card = collection.RemoveCardAt(index);
        }

        if(card == null)
        {
            return;
        }

        dragCards.Add(card);
        dragCardOffsets.Add(card.transform.position - clickedCard.transform.position);
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
        originalCardCollection.AddCard(card, originalIndex, force : true);
    }
}

}
