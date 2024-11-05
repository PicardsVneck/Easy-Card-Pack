using System.Collections.Generic;
using UnityEngine;

namespace EasyCardPack
{

public delegate void OnEasyCardClicked(EasyCardEventHits hits);
public delegate void OnEasyCardReleased(EasyCardEventHits hits);
public delegate void OnHoverEnter(EasyCard card);
public delegate void OnHoverExit(EasyCard card);
public delegate void OnEasyCardDrag(List<EasyCard> cards, EasyCardCollection collection);
public delegate void OnEasyCardDrop(List<EasyCard> cards, EasyCardCollection collection, EasyCardCollection originalCollection);
public delegate void OnEasyCardAdded(EasyCard card, EasyCardCollection collection);
public delegate void OnEasyCardRemoved(EasyCard card, EasyCardCollection collection);

public class EasyCardEvents
{
    public static event OnEasyCardClicked OnClickedEvent;
    public static event OnEasyCardReleased OnReleasedEvent;
    public static event OnHoverEnter OnHoverEnterEvent;
    public static event OnHoverExit OnHoverExitEvent;
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

    public static void OnHoverEnter(EasyCard card)
    {
        OnHoverEnterEvent?.Invoke(card);
    }

    public static void OnHoverExit(EasyCard card)
    {
        OnHoverExitEvent?.Invoke(card);
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
