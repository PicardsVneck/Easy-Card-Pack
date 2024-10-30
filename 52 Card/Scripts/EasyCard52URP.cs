using UnityEngine;

namespace EasyCard.Deck52
{

[RequireComponent(typeof(EasyCard))]
public class EasyCard52URP : MonoBehaviour
{   
    [SerializeField] private EasyCard52Definition _cardDefinition;
    [SerializeField] private Material _material;
    [SerializeField] private MeshRenderer _faceMeshRenderer;
    [SerializeField] private MeshRenderer _backMeshRenderer;
    [SerializeField] private Sprite _cardBack;

    public Suit suit
    {
        get
        {
            return _cardDefinition.suit;
        }
    }

    public Rank rank
    {
        get
        {
            return _cardDefinition.rank;
        }
    }

    void Start()
    {
        Initialize(_cardDefinition);
    }
    
    public void Initialize(EasyCard52Definition cardDefinition)
    {
        this._cardDefinition = cardDefinition;

        if (cardDefinition == null)
        {
            return;
        }

        _faceMeshRenderer.material = GetCardMaterial(cardDefinition.cardFace);
        _backMeshRenderer.material = GetCardMaterial(_cardBack);
    }

    public Material GetCardMaterial(Sprite sprite)
    {
        Material newMaterial = Instantiate(_material);
        newMaterial.mainTexture = GetSlicedSpriteTexture(sprite);
        return newMaterial;
    }

    void OnValidate()
    {
        Initialize(_cardDefinition);
    }

    Texture2D GetSlicedSpriteTexture(Sprite sprite)
    {
        Rect rect = sprite.rect;
        Texture2D slicedTex = new Texture2D((int)rect.width, (int)rect.height);
        slicedTex.filterMode = sprite.texture.filterMode;

        slicedTex.SetPixels(0, 0, (int)rect.width, (int)rect.height, sprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
        slicedTex.Apply();

        return slicedTex;
    }

}

}
