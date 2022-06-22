

public class Deck
{
    public Deck()
    {
        Cards = new List<Card>();

        foreach (Suit suit in (Suit[])Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in (Rank[])Enum.GetValues(typeof(Rank)))
            {
                Cards.Add(new Card(suit, rank));
            }
        }
        
        Suffle();
    }

    public List<Card> Cards { get; set; }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }


    public void Suffle()
    {
        Random rnd = new Random();
        for (int i = 0; i < Cards.Count; i++)
        {
            int j = rnd.Next(i, Cards.Count);
            Card temp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = temp;
        }
    }

    public Card Draw()
    {
        Card card = Cards[0];
        Cards.RemoveAt(0);
        return card;
    }

    public Hand Draw(int n)
    {
        Card[] hand = new Card[n];
        for (int i = 0; i < n; i++)
        {
            hand[i] = Draw();
        }
        return new Hand(hand);
    }

}
