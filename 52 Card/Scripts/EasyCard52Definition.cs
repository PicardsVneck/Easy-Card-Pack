using UnityEngine;

namespace EasyCard.Deck52
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

[CreateAssetMenu(fileName = "52CardDefinition", menuName = "Easy Card Pack/52 Card Definition", order = 1)]
public class EasyCard52Definition : ScriptableObject
{
    public Sprite cardFace;
    public Rank rank;
    public Suit suit;
    
}

}
