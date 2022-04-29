using System.Collections.Generic;

public class CardData
{
    public readonly bool Sign;
    public readonly List<int> Elements;

    public CardData(bool sign, List<int> elements)
    {
        Sign = sign;
        Elements = elements;
    }
}