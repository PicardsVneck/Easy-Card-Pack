using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyCardPack
{

[ExecuteAlways]
[AddComponentMenu("Easy Card Pack/Easy Card Collection")]
public class EasyCardCollection : MonoBehaviour
{
    [Header("Display")]
    public float curveRadius = 3;
    [Range(0, 360)] public float cardSpreadInDegrees = 20f;
    [Range(0, 360)] public float SpreadLimitInDegrees = 120f;
    public Vector3 cardPositionOffest = new Vector3(0, 0, -.01f);
    public bool faceDown = false;
    public bool onlyShowTopCard = false;

    [Header("Functionality")]
    public int maxCards = 54;
    public EasyCardCollectionBehavior collectionBehavior;
    public bool canAddCards = true;
    public bool canRemoveCards = true;
    public bool onlyAddTopCard = false;
    public bool onlyRemoveTopCard = false;
    public bool multiCardSelect = false;

    [Header("Contents")]
    public List<EasyCard> cards;
    
    #region Callbacks
    public void OnValidate()
    {
        UpdateCardPositions(instantanious : true);
    }
    
    #endregion

    #region Public Methods
    public EasyCard GetCard(int index)
    {
        return cards[index];
    }

    public EasyCard GetTopCard()
    {
        return cards.LastOrDefault();
    }

    public int GetCardIndex(EasyCard card)
    {
        return cards.IndexOf(card);
    }

    public bool CanAddCard(EasyCard card, int index)
    {
        bool canAdd = true;
        if (collectionBehavior != null)
        {
            canAdd = collectionBehavior.CanAddCard(card, index, this);
        }
        
        bool isTopCard = index == cards.Count;

        return canAdd && 
        canAddCards  && 
        !cards.Contains(card) &&
        cards.Count < maxCards &&
        (onlyAddTopCard ? isTopCard : true);
    }

    public bool CanRemoveCard(EasyCard card)
    {
        bool canRemove = true;
        if (collectionBehavior != null)
        {
            canRemove = collectionBehavior.CanRemoveCard(card, this);
        }

        bool isTopCard = cards.IndexOf(card) == cards.Count - 1;

        return canRemove && 
        canRemoveCards &&
        cards.Contains(card) &&
        (onlyRemoveTopCard ? isTopCard : true);

    }

    public bool AddCard(EasyCard card, int index, bool force = false, bool instantanious = false)
    {

        if (onlyAddTopCard && !force)
        {
            index = cards.Count;
        }

        if (!CanAddCard(card, index) && !force)
        {
            return false;
        }

        cards.Insert(index, card);
        card.transform.SetParent(transform);
        card.Collection = this;

        UpdateCardPositions(instantanious);
        EasyCardEvents.OnCardAdded(card, this);
        return true;
    }

    public bool AddCard(EasyCard card, bool force = false, bool instantanious = false)
    {
        return AddCard(card, cards.Count, force, instantanious);
    }

    public EasyCard RemoveCard(EasyCard card, bool force = false, bool instantanious = false)
    {
        if (!CanRemoveCard(card) && !force)
        {
            return null;
        }
        cards.Remove(card);
        card.Collection = null;

        UpdateCardPositions(instantanious);
        EasyCardEvents.OnCardRemoved(card, this);
        return card;
    }

    public EasyCard RemoveCardAt(int index, bool force = false)
    {
        return RemoveCard(GetCard(index), force);
    }

    public int GetClosestCardIndexByPosition(Vector3 position)
    {
        float smallestDistance = float.MaxValue;
        int smallestDistanceIndex = 0;

        for (int i = 0; i < cards.Count + 1; i++)
        {
            Vector3 localPosition = getCardLocalPosition(i, cards.Count + 1);
            Vector3 futureGlobalPosition = localPosition + transform.position;

            float distance = Vector3.Distance(position, futureGlobalPosition);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                smallestDistanceIndex = i;
            }
        }

        return smallestDistanceIndex;
    }

    public void UpdateCardPositions(bool instantanious = false)
    {
        CleanCards();

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null)
            {
                continue;
            }
            
            Vector3 localPosition = getCardLocalPosition(i, cards.Count);
            Vector3 position = transform.TransformPoint(localPosition);

            Quaternion localRotation = getCardLocalRotation(i, cards.Count);
            Quaternion rotation = transform.rotation * localRotation;

            if (instantanious)
            {
                cards[i].SetTransform(position, rotation);
            }
            else
            {
                cards[i].MoveTo(position, rotation);
            }

            if (onlyShowTopCard && i < cards.Count - 1)
            {
                cards[i].gameObject.SetActive(false);
            }
            else
            {
                cards[i].gameObject.SetActive(true);
            }
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            EasyCard temp = cards[i];
            int randomIndex = Random.Range(0, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
        UpdateCardPositions();
    }

    #endregion

    #region Private Methods

    private Vector3 getCardLocalPosition(int i, int numberOfCards)
    {
        float cardRotation = CardAngleInRadians(i, numberOfCards);
        Vector3 position = new Vector3(Mathf.Sin(cardRotation) * curveRadius, Mathf.Cos(cardRotation) * curveRadius - curveRadius, 0);
        position += cardPositionOffest * i;
        return position;
    }

    private Quaternion getCardLocalRotation(int i, int numberOfCards)
    {
        float cardRotation = CardAngleInRadians(i, numberOfCards);
        Quaternion rotation = Quaternion.Euler(0, 0, -1 * cardRotation * Mathf.Rad2Deg);

        if (faceDown)
        {
            rotation *= Quaternion.Euler(180, 0, 0);
        }
        return rotation;
    }

    private float CardAngleInRadians(float i, int numberOfCards)
    {
        if (SpreadLimitInDegrees < Mathf.Abs(numberOfCards * cardSpreadInDegrees))
        {
            cardSpreadInDegrees = SpreadLimitInDegrees / numberOfCards;
        }
        float cardRotation = (i * cardSpreadInDegrees) - (numberOfCards / 2f * cardSpreadInDegrees) + (.5f * cardSpreadInDegrees);
        return cardRotation * Mathf.Deg2Rad;
    }

    private void CleanCards()
    {
        cards.RemoveAll(item => item == null);
        for (int i = 0; i < cards.Count; i++)
        {
            EasyCard card = cards[i];
            if (card == null)
            {
                continue;
            }

            if (card.Collection == null)
            {
                card.Collection = this;
            }

            if (card.Collection != this)
            {
                card.Collection.RemoveCard(card, true);
                card.Collection = this;
            }
        }
    }

    #endregion
    
}

}
