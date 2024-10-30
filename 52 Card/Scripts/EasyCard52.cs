using UnityEngine;

namespace EasyCard.Deck52
{

[RequireComponent(typeof(EasyCard))]
public class EasyCard52 : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _faceSpriteRenderer;
    [SerializeField] private MeshRenderer _faceMeshRenderer;
    [SerializeField] private EasyCard52Definition _cardDefinition;
    
    public void Initialize(EasyCard52Definition cardDefinition)
    {
        if(cardDefinition == null)
        {
            return;
        }
        _faceSpriteRenderer.sprite = cardDefinition.cardFace;
        this._cardDefinition = cardDefinition;
    }

    public void OnValidate()
    {
        Initialize(_cardDefinition);
    }

}

}
