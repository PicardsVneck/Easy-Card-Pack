using System.Collections.Generic;
using UnityEngine;

namespace EasyCardPack
{

public abstract class EasyCardEventHandler : MonoBehaviour
{
    void OnEnable()
    {
        EasyCardEvents.OnClickedEvent += OnCardClicked;
        EasyCardEvents.OnReleasedEvent += OnCardReleased;
        EasyCardEvents.OnDragEvent += OnCardDrag;
        EasyCardEvents.OnDropEvent += OnCardDrop;
        EasyCardEvents.OnCardAddedEvent += OnCardAdded;
        EasyCardEvents.OnCardRemovedEvent += OnCardRemoved;

    }

    void OnDisable()
    {
        EasyCardEvents.OnClickedEvent -= OnCardClicked;
        EasyCardEvents.OnReleasedEvent -= OnCardReleased;
        EasyCardEvents.OnDragEvent -= OnCardDrag;
        EasyCardEvents.OnDropEvent -= OnCardDrop;
        EasyCardEvents.OnCardAddedEvent -= OnCardAdded;
        EasyCardEvents.OnCardRemovedEvent -= OnCardRemoved;
    }

    protected virtual void OnCardClicked(EasyCardEventHits hits){}
    protected virtual void OnCardReleased(EasyCardEventHits hits){}
    protected virtual void OnCardDrag(List<EasyCard> card, EasyCardCollection collection){}
    protected virtual void OnCardDrop(List<EasyCard> card, EasyCardCollection collection, EasyCardCollection originalCollection){}
    protected virtual void OnCardAdded(EasyCard card, EasyCardCollection collection){}
    protected virtual void OnCardRemoved(EasyCard card, EasyCardCollection collection){}


}

}
