using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace EasyCard.Solitaire
{
public class SolitaireGameManager : EasyCardEventHandler
{
    [SerializeField] private EasyCardCollection _deck;
    [SerializeField] private EasyCardCollection _wastePile;
    [SerializeField] private List<EasyCardCollection> _faceUpTableaus;
    [SerializeField] private List<EasyCardCollection> _faceDownTableaus;
    [SerializeField] private List<EasyCardCollection> _foundationPiles;

    private bool _areCardsDealt = false;
    private float _timeBetweenDeals = 0.2f;

    private void Start()
    {
        StartCoroutine(DealCards());
    }

    IEnumerator DealCards()
    {
        for (int i = 0; i <= 6; i++)
        {
            for (int j = i; j <= 6; j++)
            {
                EasyCard card = _deck.GetTopCard();
                if (j == i)
                {
                    _deck.RemoveCard(card, force: true);
                    AddCardToTableau(card, j);
                    
                    yield return new WaitForSeconds(_timeBetweenDeals);
                }
                else
                {
                    _deck.RemoveCard(card, force: true);
                    _faceDownTableaus[j].AddCard(card, force: true);

                    yield return new WaitForSeconds(_timeBetweenDeals);
                }
            }
        }
        _areCardsDealt = true;
    }

    IEnumerator DealWastePile()
    {
        // Move all cards from waste pile back to deck
        int cardsInWastePile = _wastePile.cards.Count;
        for (int i = 0; i < cardsInWastePile; i++)
        {
            EasyCard card = _wastePile.GetTopCard();
            _wastePile.RemoveCard(card, force: true);
            _deck.AddCard(card, 0, force: true);
        }
        yield return new WaitForSeconds(_timeBetweenDeals);

        // Deal 3 cards from deck to waste pile
        for (int i = 0; i < 3; i++)
        {
            EasyCard card = _deck.GetTopCard();
            _deck.RemoveCard(card, force: true);
            _wastePile.AddCard(card, force: true);

            yield return new WaitForSeconds(_timeBetweenDeals);
        }
    }

    protected override void OnCardAdded(EasyCard card, EasyCardCollection collection)
    {
        if (!_areCardsDealt) { return; }
        
        for (int i = 0; i < _faceUpTableaus.Count; i++)
        {
            if (_faceUpTableaus[i].cards.Count == 0 && _faceDownTableaus[i].cards.Count > 0)
            {
                EasyCard topCard = _faceDownTableaus[i].GetTopCard();
                _faceDownTableaus[i].RemoveCard(topCard, force: true);
                AddCardToTableau(topCard, i);
            }
        }
    }

    protected override void OnCardClicked(EasyCardEventHits hits)
    {
        if (!_areCardsDealt) { return; }

        if (hits.hitCollections.Count == 0) { return; }

        EasyCardCollection hitCollection = hits.hitCollections[0];
        if(hitCollection == _deck)
        {
            StartCoroutine(DealWastePile());
        }
    }

    // Disable Foundations when dragging multiple cards
    protected override void OnCardDrag(List<EasyCard> cards, EasyCardCollection collection)
    {
        if(cards == null || cards.Count <= 1) 
        { 
            EnableFoundationsDrop(true);
            return; 
        }
        EnableFoundationsDrop(false);
    }

    private void EnableFoundationsDrop(bool enabled)
    {
        foreach(EasyCardCollection foundation in _foundationPiles)
        {
            foundation.canAddCards = enabled;
        }
    }

    private void AddCardToTableau(EasyCard card, int tableausIndex)
    {
        EasyCardCollection faceUpTableau = _faceUpTableaus[tableausIndex];
        EasyCardCollection faceDownTableau = _faceDownTableaus[tableausIndex];

        Vector3 faceUpTableauPosition = faceDownTableau.transform.position;
        if (faceDownTableau.cards.Count > 0)
        {
            faceUpTableauPosition = faceDownTableau.GetTopCard().transform.position + new Vector3(0, -0.15f, -.01f);
        }
        faceUpTableau.transform.position = faceUpTableauPosition;

        faceUpTableau.AddCard(card, force: true);
    }

}

}
