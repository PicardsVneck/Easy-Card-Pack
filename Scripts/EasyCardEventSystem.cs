using System.Collections.Generic;
using UnityEngine;

namespace EasyCard
{

public class EasyCardEventHits
{
    public List<EasyCard> hitCards = new List<EasyCard>();
    public List<EasyCardCollection> hitCollections = new List<EasyCardCollection>();
}
public class EasyCardEventSystem : MonoBehaviour
{
    EasyCardEventHits hits = new EasyCardEventHits();

    bool hasClicked = false;

    void Update(){

        if (Input.GetMouseButtonDown(0))
        {
            hits = RayCastForCards();
            if (hits.hitCards.Count != 0 && hits.hitCollections.Count != 0)
            {
                EasyCardEvents.OnClicked(hits);
                hasClicked = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (hasClicked)
            {
                hits = RayCastForCards();
                EasyCardEvents.OnReleased(hits);
                hasClicked = false;
            }
        }
    }


    private EasyCardEventHits RayCastForCards()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, float.MaxValue);
        System.Array.Sort(raycastHits, (a, b) => (a.distance.CompareTo(b.distance)));

        hits.hitCards.Clear();
        hits.hitCollections.Clear();
        foreach (RaycastHit hit in raycastHits)
        {
            EasyCard hitCard = hit.collider.gameObject.GetComponent<EasyCard>();
            EasyCardCollection collection = hit.collider.gameObject.GetComponent<EasyCardCollection>();

            if (hitCard != null)
            {
                hits.hitCards.Add(hitCard);
                if (hitCard.Collection != null && !hits.hitCollections.Contains(hitCard.Collection))
                {
                    hits.hitCollections.Add(hitCard.Collection);
                }
            }

            if (collection != null && !hits.hitCollections.Contains(collection))
            {
                hits.hitCollections.Add(collection);
            }
        }
        return hits;
    }
}

}
