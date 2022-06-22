var d = new Deck();

var h1 = d.Draw(5);
var h2 = d.Draw(5);
// var h1=new Hand(new []{
//     new Card(Suit.Hearts,Rank.Four),
//     new Card(Suit.Diamonds,Rank.Two),
//     new Card(Suit.Clubs,Rank.Four),
//     new Card(Suit.Diamonds,Rank.Four),
//     new Card(Suit.Clubs,Rank.Two),
//     });
// Console.WriteLine(h1.DetermineHandRank());
//var h2=new Hand(new []{new Card(Suit.Clubs,Rank.Eight)});

Console.WriteLine("Player 1: " + h1.ToString()+" --- "+h1.DetermineHandRank());
Console.WriteLine("Player 2: " + h2.ToString()+" --- "+h2.DetermineHandRank());
Console.WriteLine("Winner Player:" + DetermineWinner(h1, h2));

int DetermineWinner(Hand hand1, Hand hand2)
{
    int winner = 0;

    if (hand1.Score > hand2.Score)
        winner = 1;
    else if (hand2.Score > hand1.Score)
        winner = 2;
    // else
    // 	winner = TieBreaker(h1, h2);

    return winner;
}

public class Hand
{
    public Hand(Card[] cards)
    {
        this.Cards = cards.OrderBy(c=>c.Rank).ToArray();        
        Score = DetermineHandRank();
    }

    public Card[] Cards { get; }
    public HandRank Score { get; }
    public bool IsSameSuit() => Cards.All(c => c.Suit == Cards.First().Suit);

    public bool IsConsecutive()
	{
		var normalized = Cards.Select(c => c.Rank)
								.Where(r => r != Rank.Ace)
                                .Select(r => (int)r).ToList();

		if (normalized.Count < 4) return false;

		if (normalized.Count == 4)
		{
			if (normalized[0] == 2)
			{
				normalized.Insert(0, 1);
			}
			else if (normalized[normalized.Count - 1] == 13)
			{
				normalized.Add(14);
			}
			else
				return false;
		}

		for (int i = 0; i < normalized.Count; i++)
		{
			if (normalized[0] + i != normalized[i])
				return false;
		}

		return true;
	}

    public override string ToString()
    {
        return Cards.Select(c => c.Rank.ToString() + ":" + c.Suit.ToString()).Aggregate((a, b) => a + ", " + b);
    }

    public HandRank DetermineHandRank()
    {
        if (IsSameSuit() && IsConsecutive())
		{
			if (Cards[4].Rank == Rank.Ace)
				return HandRank.RoyalFlush;
			else
				return HandRank.StraightFlush;
		}

        var g = Cards.GroupBy(c => c.Rank);

        if (g.Count() == 2)
		{
			if (g.Any(c => c.Count() == 4))
				return HandRank.FourOfAKind;

			if (g.Any(c => c.Count() == 3) && g.Any(c => c.Count() == 2))
				return HandRank.FullHouse;
		}

        if (IsSameSuit()) return HandRank.Flush;

		if (IsConsecutive()) return HandRank.Straight;

        if (g.Any(c => c.Count() == 3))
			return HandRank.ThreeOfAKind;

		if (g.Count() == 3)
			return HandRank.TwoPairs;

		if (g.Any(c => c.Count() == 2))
			return HandRank.OnePair;

		return HandRank.High;
    }
}
