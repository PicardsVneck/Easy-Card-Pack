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
            return;
        }

        if (originalCardCollection.multiCardSelect)
        {
            if (!originalCardCollection.CanRemoveCard(clickedCard))
            {
                return;
            }

            originalIndex = originalCardCollection.GetCardIndex(clickedCard);
            for (int i = originalIndex; i < originalCardCollection.cards.Count; i++)
            {
                EasyCard card = originalCardCollection.cards[i];
                dragCards.Add(card);
                dragCardOffsets.Add(card.transform.position - clickedCard.transform.position);
                originalCardCollection.RemoveCard(card);
                i--;
            }
            EasyCardEvents.OnDrag(dragCards, originalCardCollection);
        }
        else
        {
            originalIndex = originalCardCollection.GetCardIndex(clickedCard);
            if (!originalCardCollection.CanRemoveCard(clickedCard))
            {
                return;
            }
            dragCards.Add(clickedCard);
            dragCardOffsets.Add(Vector3.zero);
            originalCardCollection.RemoveCard(clickedCard);
            EasyCardEvents.OnDrag(dragCards, originalCardCollection);
        }
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
                newCardCollection.AddCard(card, i + addCardIndex);
            }
            EasyCardEvents.OnDrop(dragCards, newCardCollection, originalCardCollection);
        }
        
        if (dragCards[0].Collection == null)
        {
            ReturnCard();
        }
        
        dragCards.Clear();
    }

    private void ReturnCard()
    {
        Debug.Log("Return Card");
        if (originalCardCollection != null)
        {
            for (int i = 0; i < dragCards.Count; i++)
            {
                EasyCard card = dragCards[i];
                originalCardCollection.AddCard(card, originalIndex + i, force : true);
            }
        }
        dragCards.Clear();
    }
}

}
