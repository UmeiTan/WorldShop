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
    [SerializeField] private GameObject _cardSelectionView;
    [SerializeField] private List<Card> _selectableCards;

    public void UpdateText(int cardDeck, int discardDeck)
    {
        _numberCardDeckText.text = cardDeck.ToString();
        _numberDiscardDeckText.text = discardDeck.ToString();
    }

    public void OpenDeckView(List<CardData> cardDeck) => OpenView("Колода", cardDeck);
    public void OpenDiscardDeckView(List<CardData> discardDeck) => OpenView("Колода сброса", discardDeck);
    private void OpenView(string text, List<CardData> cardDatas)//надо переделать
    {
        _cardDeckView.SetActive(true);
        _cardDeckViewText.text = text;
        int cards = cardDatas.Count;
        int pool = _cardsPool.Count;
        while (cards > pool)
        {
            _cardsPool.Add(GameObject.Instantiate(_cardsPool[0].gameObject, _cardsPool[0].transform.parent).GetComponent<Card>());
            pool++;
        }
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
    public void OpenCardSelectionView(List<CardData> cardDatas)
    {
        for (int i = 0; i < cardDatas.Count; i++)
        {
            _selectableCards[i].SetInactiveCard(cardDatas[i]);
        }
        _cardSelectionView.SetActive(true);
    }
    public void CloseCardSelectionView() => _cardSelectionView.SetActive(false);
}