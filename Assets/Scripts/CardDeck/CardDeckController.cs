using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeckController : MonoBehaviour
{
    [SerializeField][Range (0,100)] private int _negativeCardChance;
    [SerializeField] private CardDeckView _cardDeckView;

    private List<CardData> _cardDeck;
    private List<CardData> _discardDeck;
    private List<CardData> _activeDeck;
    private LevelConfig _levelConfig;
    private int _needLoadCard = 0;

    private void OnEnable()
    {
        ResultHandler.OnCardSuccessfullyUsed += UpdateDecks;
        Card.OnCardRemoved += NextCard;
        LevelController.OnLevelChanged += GetLevelCongig;
        SaveLoadGame.OnDataSaved += SaveDeck;
        SaveLoadGame.OnDataLoading += LoadDeck;
        SaveLoadGame.OnDefaultDataLoading += GetDefaultDeck;
    }

    private void OnDisable()
    {
        ResultHandler.OnCardSuccessfullyUsed -= UpdateDecks;
        Card.OnCardRemoved -= NextCard;
        LevelController.OnLevelChanged -= GetLevelCongig;
        SaveLoadGame.OnDataSaved -= SaveDeck;
        SaveLoadGame.OnDataLoading -= LoadDeck;
        SaveLoadGame.OnDefaultDataLoading -= GetDefaultDeck;
    }

    private void GetDefaultDeck()
    {
        _cardDeck = new List<CardData>();
        _discardDeck = new List<CardData>();
        _activeDeck = new List<CardData>();
        for (int i = 0; i < 4; i++)
        {
            _cardDeck.Add(new CardData(true, new List<int>() { i }));
            _cardDeck.Add(new CardData(false, new List<int>() { i }));
            NewCard(_cardDeck);
        }
        _cardDeckView.UpdateText(_cardDeck.Count, _discardDeck.Count);
    }
    private void GetLevelCongig(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }
    private void NewCard(List<CardData> list)
    {
        bool sign;
        List<int> elements = new();
        int typeCard = Random.Range(0, _levelConfig.MaxCardGrade) + 1;//1-3

        sign = Random.Range(0, 100) > _negativeCardChance;
        for (int i = 0; i < typeCard; i++)
        {
            elements.Add(Random.Range(0, _levelConfig.NumberElements));
        }

        list.Add (new CardData(sign, elements));
    }
    private void UpdateDecks(CardData cardData)
    {
        if (_activeDeck.Contains (cardData))
        {
            _discardDeck.Add (cardData);
            _activeDeck.Remove (cardData);

            _cardDeckView.UpdateText(_cardDeck.Count, _discardDeck.Count);
        }
        else
        {
            Debug.LogWarning("Карта в активной колоде не найдена");

        }
    }
    private CardData NextCard(Card card)
    {
        if (_activeDeck.Count == 4)
        {
            return _activeDeck[_needLoadCard++];
        }
        if (_cardDeck.Count == 0)
        {
            foreach(CardData cardData in _discardDeck)
            {
                _cardDeck.Add(cardData);
            }
            _discardDeck.Clear();
        }

        int i = Random.Range(0, _cardDeck.Count);
        _activeDeck.Add(_cardDeck[i]);
        _cardDeck.Remove(_cardDeck[i]);

        _cardDeckView.UpdateText(_cardDeck.Count, _discardDeck.Count);

        return _activeDeck[_activeDeck.Count - 1];
    }
    private XElement SaveDeck()
    {
        XElement rootNode = new("Deck");
        XElement node;
        XElement element;
        int i;

        node = new("CardDeck");
        foreach (var card in _cardDeck)
        {
            element = new("card");
            element.SetAttributeValue("sign", card.Sign.ToString());
            i = 0;
            foreach (var el in card.Elements)
            {
                element.SetAttributeValue("element" + i, card.Elements[i++].ToString());
            }
            node.Add(element);
        }
        rootNode.Add(node);

        node = new("DiscardDeck");
        foreach (var card in _discardDeck)
        {
            element = new("card");
            element.SetAttributeValue("sign", card.Sign.ToString());
            i = 0;
            foreach (var el in card.Elements)
            {
                element.SetAttributeValue("element" + i, card.Elements[i++].ToString());
            }
            node.Add(element);
        }
        rootNode.Add(node);

        node = new("ActiveDeck");
        foreach (var card in _activeDeck)
        {
            element = new("card");
            element.SetAttributeValue("sign", card.Sign.ToString());
            i = 0;
            foreach (var el in card.Elements)
            {
                element.SetAttributeValue("element" + i, card.Elements[i++].ToString());
            }
            node.Add(element);
        }
        rootNode.Add(node);

        return rootNode;
    }
    private void LoadDeck(XElement root)
    {
        _cardDeck = new List<CardData>();
        _discardDeck = new List<CardData>();
        _activeDeck = new List<CardData>();
        XElement rootNode = root.Element("Deck");
        XElement node;
        int i;

        bool sign;
        List<int> el;

        node = rootNode.Element("CardDeck");
        foreach (var card in node.Elements("card"))
        {
            i = 0;
            sign = bool.Parse(card.Attribute("sign").Value);
            el = new List<int>();
            foreach (var element in card.Attributes("element" + i))
            {
                el.Add(int.Parse(element.Value));
            }
            _cardDeck.Add(new CardData(sign, el));
        }

        node = rootNode.Element("DiscardDeck");
        foreach (var card in node.Elements("card"))
        {
            i = 0;
            sign = bool.Parse(card.Attribute("sign").Value);
            el = new List<int>();
            foreach (var element in card.Attributes("element" + i))
            {
                el.Add(int.Parse(element.Value));
            }
            _discardDeck.Add(new CardData(sign, el));
        }

        node = rootNode.Element("ActiveDeck");
        foreach (var card in node.Elements("card"))
        {
            i = 0;
            sign = bool.Parse(card.Attribute("sign").Value);
            el = new List<int>();
            foreach (var element in card.Attributes("element" + i))
            {
                el.Add(int.Parse(element.Value));
            }
            _activeDeck.Add(new CardData(sign, el));
        }

        _cardDeckView.UpdateText(_cardDeck.Count, _discardDeck.Count);
    }

    //вызывается на сцене
    public void CardDeckView(bool cardDeck)
    {
        if (cardDeck)
        {
            _cardDeckView.OpenDeckView(_cardDeck);
        }
        else
        {
            _cardDeckView.OpenDiscardDeckView(_discardDeck);
        }
    }
}