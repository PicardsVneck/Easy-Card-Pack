using UnityEngine;

namespace EasyCardPack.Graphic
{

[CreateAssetMenu(fileName = "PlayingCardDefinition", menuName = "Easy Card Pack/Playing Card Definition", order = 1)]
public class EasyGraphicCardDefinition : EasyCardDefinition
{
    [SerializeField] private Sprite _cardFace;
    [SerializeField] public EasyCard _prefab;

    //accessors
    public Sprite cardFace { get { return _cardFace; } }
    public EasyCard prefab { get { return _prefab; } }

    public override EasyCard CreateCard()
    {
        EasyCard card = Instantiate(_prefab);
        //card.gameObject.name = $"{_rank} of {_suit}";

        EasyPlayingCardURP cardURP = card.GetComponent<EasyPlayingCardURP>();
        //cardURP?.Initialize(this);

        return card;
    }
}

}
