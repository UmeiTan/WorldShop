using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Client
{
    [SerializeField] private Sprite _clientSprite;
    [SerializeField][Range (4, 8)] private int _numberElementsRequested = 4;
    [SerializeField] private List<int> _baseAwardMoney;//прокачка клиентов

    public Sprite ClientSprite => _clientSprite;
    public int NumberElementsRequested => _numberElementsRequested;
    public List<int> BaseAwardMoney => _baseAwardMoney;
}
