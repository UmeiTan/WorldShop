using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CardView
{
    [SerializeField] private IconBase _icons;
    [SerializeField] private Image _sign;
    [SerializeField] private GameObject _type1;
    [SerializeField] private Image _type1Element;
    [SerializeField] private GameObject _type2;
    [SerializeField] private List<Image> _type2Elements;
    [SerializeField] private GameObject _type3;
    [SerializeField] private List<Image> _type3Elements;

    public void UpdateCardView(CardData cardData)
    {
        _sign.sprite = cardData.Sign ? _icons.Sign[1] : _icons.Sign[0];
        switch (cardData.Elements.Count)
        {
            case 0:
                {
                    Debug.LogError("Карта не содержит элементов");
                    break;
                }
            case 1:
                {
                    _type1.SetActive(true);
                    _type2.SetActive(false);
                    _type3.SetActive(false);

                    _type1Element.sprite = _icons.Sprites[cardData.Elements[0]];

                    break;
                }
            case 2:
                {
                    _type1.SetActive(false);
                    _type2.SetActive(true);
                    _type3.SetActive(false);

                    for (int i = 0; i < 2; i++)
                    {
                        _type2Elements[i].sprite = _icons.Sprites[cardData.Elements[i]];
                    }

                    break;
                }
            case 3:
                {
                    _type1.SetActive(false);
                    _type2.SetActive(false);
                    _type3.SetActive(true);

                    for (int i = 0; i < 3; i++)
                    {
                        _type3Elements[i].sprite = _icons.Sprites[cardData.Elements[i]];
                    }

                    break;
                }
        }
    }
}