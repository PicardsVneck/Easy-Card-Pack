using System.Collections.Generic;
using UnityEngine;

namespace EasyCard
{

public delegate void OnEasyCardClicked(EasyCardEventHits hits);
public delegate void OnEasyCardReleased(EasyCardEventHits hits);
public delegate void OnEasyCardDrag(List<EasyCard> cards, EasyCardCollection collection);
public delegate void OnEasyCardDrop(List<EasyCard> cards, EasyCardCollection collection, EasyCardCollection originalCollection);
public delegate void OnEasyCardAdded(EasyCard card, EasyCardCollection collection);
public delegate void OnEasyCardRemoved(EasyCard card, EasyCardCollection collection);

public class EasyCardEvents
{
    public static event OnEasyCardClicked OnClickedEvent;
    public static event OnEasyCardReleased OnReleasedEvent;
    public static event OnEasyCardDrag OnDragEvent;
    public static event OnEasyCardDrop OnDropEvent;
    public static event OnEasyCardAdded OnCardAddedEvent;
    public static event OnEasyCardRemoved OnCardRemovedEvent;

    public static void OnClicked(EasyCardEventHits hits)
    {
        OnClickedEvent?.Invoke(hits);
    }

    public static void OnReleased(EasyCardEventHits hits)
    {
        OnReleasedEvent?.Invoke(hits);
    }

    public static void OnDrag(List<EasyCard> cards, EasyCardCollection collection)
    {
        OnDragEvent?.Invoke(cards, collection);
    }

    public static void OnDrop(List<EasyCard> cards, EasyCardCollection collection, EasyCardCollection originalCollection)
    {
        OnDropEvent?.Invoke(cards, collection, originalCollection);
    }

    public static void OnCardAdded(EasyCard card, EasyCardCollection collection)
    {
        OnCardAddedEvent?.Invoke(card, collection);
    }

    public static void OnCardRemoved(EasyCard card, EasyCardCollection collection)
    {
        OnCardRemovedEvent?.Invoke(card, collection);
    }

}

}
