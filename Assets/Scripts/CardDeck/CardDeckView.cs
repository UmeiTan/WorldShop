using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CardDeckView
{
    [SerializeField] private Text _numberCardDeckText;
    [SerializeField] private Text _numberDiscardDeckText;
    [SerializeField] private GameObject _cardDeckView;
    [SerializeField] private Text _cardDeckViewText;
    [SerializeField] private List<Card> _cardsPool; //должен содержать возможный максимум карт...

    public void UpdateText(int cardDeck, int discardDeck)
    {
        _numberCardDeckText.text = cardDeck.ToString();
        _numberDiscardDeckText.text = discardDeck.ToString();
    }

    public void OpenDeckView(List<CardData> cardDeck)
    {
        OpenView("Колода", cardDeck);
    }
    public void OpenDiscardDeckView(List<CardData> discardDeck)
    {
        OpenView("Колода сброса", discardDeck);
    }
    private void OpenView(string text, List<CardData> cardDatas)//надо переделать
    {
        _cardDeckView.SetActive(true);
        _cardDeckViewText.text = text;
        int cards = cardDatas.Count;
        int pool = _cardsPool.Count;

        for (int i = 0; i < cards; i++)
        {
            _cardsPool[i].SetInactiveCard(cardDatas[i]);
            _cardsPool[i].gameObject.SetActive(true);
        }
        for (int i = cards; i < pool; i++)
        {
            _cardsPool[i].gameObject.SetActive(false);
        }
    }
}