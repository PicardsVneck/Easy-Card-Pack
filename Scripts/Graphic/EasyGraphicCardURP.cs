using UnityEngine;

namespace EasyCardPack.Graphic
{

[RequireComponent(typeof(EasyCard))]
[AddComponentMenu("Easy Card Pack/Graphic Card")]
public class EasyPlayingCardURP : MonoBehaviour
{   
    [Header("Card Definition")]
    [SerializeField] private EasyGraphicCardDefinition _cardDefinition;

    [Header("Card Face")]
    [SerializeField] private MeshRenderer _faceRenderer;
    [SerializeField] private Material _material;
    
    [Header("Card Back")]
    [SerializeField] private MeshRenderer _backRenderer;
    [SerializeField] private Sprite _cardBack;

    [Header("Outline")]
    [SerializeField] private MeshRenderer _outlineRenderer;
    [SerializeField] private MeshRenderer _backOutlineRenderer;
    [SerializeField] private Material _outlineMaterial;
    [SerializeField] private float _outlineWidth = 0.01f;

    /*public Suit suit
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
    }*/

    void Start()
    {
        Initialize(_cardDefinition);
    }

    void OnValidate()
    {
        Initialize(_cardDefinition);
    }
    
    public void Initialize(EasyGraphicCardDefinition cardDefinition)
    {
        this._cardDefinition = cardDefinition;

        UpdateCardFace();
        UpdateCardBack();
        UpdateOutline();
        
    }

    public Material GetCardMaterial(Sprite sprite)
    {
        Material newMaterial = Instantiate(_material);
        
        Vector2 spriteSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 offset = new Vector2(sprite.rect.x / spriteSize.x, sprite.rect.y / spriteSize.y);
        Vector2 tiling = new Vector2(sprite.rect.width / spriteSize.x, sprite.rect.height / spriteSize.y);

        newMaterial.SetTexture("_MainTex", sprite.texture);
        newMaterial.SetTextureOffset("_MainTex", offset);
        newMaterial.SetTextureScale("_MainTex", tiling);
        //newMaterial.SetVector("_Tiling", tiling);
        //newMaterial.SetVector("_Offset", offset);

        return newMaterial;
    }

    private void UpdateOutline()
    {
        if(_outlineWidth <= 0)
        {
            _outlineRenderer?.gameObject.SetActive(false);
            _backOutlineRenderer?.gameObject.SetActive(false);
            return;
        }
        else
        {
            _outlineRenderer?.gameObject.SetActive(true);
            _backOutlineRenderer?.gameObject.SetActive(true);
        }

        Vector3 faceScale = _faceRenderer.transform.localScale;
        float outlineWidthX = _outlineWidth / _faceRenderer.transform.lossyScale.x;
        float outlineWidthY = _outlineWidth / _faceRenderer.transform.lossyScale.y;

        if(_outlineRenderer != null)
        {
            _outlineRenderer.transform.localScale = new Vector3(faceScale.x + outlineWidthX, faceScale.y + outlineWidthY, 1);
            _outlineRenderer.sharedMaterial = _outlineMaterial;
        }

        if(_backOutlineRenderer != null)
        {
            _backOutlineRenderer.transform.localScale = new Vector3(faceScale.x + outlineWidthX, faceScale.y + outlineWidthY, 1);
            _backOutlineRenderer.sharedMaterial = _outlineMaterial;
        }
    }

    private void UpdateCardBack()
    {
        if(_backRenderer != null && _cardBack != null)
        {
            _backRenderer.material = GetCardMaterial(_cardBack);

        }
    }

    private void UpdateCardFace()
    {
        if(_faceRenderer != null && _cardDefinition != null)
        {
            _faceRenderer.material = GetCardMaterial(_cardDefinition.cardFace);
        }
    }

    

    /*Texture2D GetSlicedSpriteTexture(Sprite sprite)
    {
        Rect rect = sprite.rect;
        Texture2D slicedTex = new Texture2D((int)rect.width, (int)rect.height);
        slicedTex.filterMode = sprite.texture.filterMode;

        slicedTex.SetPixels(0, 0, (int)rect.width, (int)rect.height, sprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
        slicedTex.Apply();

        return slicedTex;
    }*/

}

}
