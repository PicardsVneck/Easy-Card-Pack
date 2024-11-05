using UnityEngine;

namespace EasyCardPack.Playing
{

public enum Rank
{
    None = 0,
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Joker = 14
}

public enum Suit
{
    None = 0,
    Clubs = 1,
    Diamonds = 2,
    Hearts = 3,
    Spades = 4
}

[CreateAssetMenu(fileName = "PlayingCardDefinition", menuName = "Easy Card Pack/Playing Card Definition", order = 1)]
public class EasyPlayingCardDefinition : EasyCardDefinition
{
    [SerializeField] private Sprite _cardFace;
    [SerializeField] private Rank _rank;
    [SerializeField] private Suit _suit;
    [SerializeField] public EasyCard _prefab;

    //accessors
    public Sprite cardFace { get { return _cardFace; } }
    public Rank rank { get { return _rank; } }
    public Suit suit { get { return _suit; } }
    public EasyCard prefab { get { return _prefab; } }

    public override EasyCard CreateCard()
    {
        EasyCard card = Instantiate(_prefab);
        card.gameObject.name = $"{_rank} of {_suit}";

        EasyPlayingCardURP cardURP = card.GetComponent<EasyPlayingCardURP>();
        cardURP?.Initialize(this);

        return card;
    }
}

}
