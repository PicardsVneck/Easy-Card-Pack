using System;
using UnityEngine;

namespace EasyCardPack.Graphic
{

[CreateAssetMenu(fileName = "GraphicCardDefinition", menuName = "Easy Card Pack/Graphic Card Definition", order = 1)]
public class EasyGraphicCardDefinition : EasyCardDefinition
{
    [SerializeField] private String _title;
    [SerializeField] private String _description;
    [SerializeField] private Sprite _cardFace;
    [SerializeField] public EasyCard _prefab;

    //accessors
    public string title { get { return _title; } }
    public string description { get { return _description; } }
    public Sprite cardFace { get { return _cardFace; } }
    public EasyCard prefab { get { return _prefab; } }

    public override EasyCard CreateCard()
    {
        EasyCard card = Instantiate(_prefab);
        card.gameObject.name = $"{_title} Card";

        EasyPlayingCardURP cardURP = card.GetComponent<EasyPlayingCardURP>();
        //cardURP?.Initialize(this);

        return card;
    }
}

}
