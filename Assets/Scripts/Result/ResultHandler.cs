using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultHandler : MonoBehaviour
{
    public delegate List<int> —hoice—ompleted();

    public static event —hoice—ompleted On—hoice—ompleted;
    public static event Action<CardData> OnCardSuccessfullyUsed;
    public static event Action<List<int>, int> OnResultUpdated;

    [SerializeField] private ResultView _resultView;

    private List<int> _plusElements;
    private List<int> _minusElements;

    private void OnEnable()
    {
        Card.OnCardUsed += UsedCard;
        Card.OnCardUseEnded += UpdateView;
        AwardController.OnClientLeft += ClearView;
    }
    private void OnDisable()
    {
        Card.OnCardUsed -= UsedCard;
        Card.OnCardUseEnded -= UpdateView;
        AwardController.OnClientLeft -= ClearView;
    }
    private void Awake()
    {
        _plusElements = new List<int>();
        _minusElements = new List<int>();
    }

    public bool UsedCard(CardData cardData)
    {
        List<int> backupPlus = new List<int>(_plusElements);
        List<int> backupMinus = new List<int>(_minusElements);

        if (cardData.Sign)
        {
            foreach(int element in cardData.Elements)
            {
                if (_minusElements.Contains(element))
                {
                    _minusElements.Remove(element);
                }
                else
                {
                    _plusElements.Add(element);
                }
            }
        }
        else
        {
            foreach (int element in cardData.Elements)
            {
                if (_plusElements.Contains(element))
                {
                    _plusElements.Remove(element);
                }
                else
                {
                    _minusElements.Add(element);
                }
            }
        }

        if (_minusElements.Count > 9 || _plusElements.Count > 9)
        {
            _plusElements.Clear();
            _plusElements.AddRange(backupPlus);
            _minusElements.Clear();
            _minusElements.AddRange(backupMinus);
            return false;
        }
        else
        {
            OnCardSuccessfullyUsed?.Invoke(cardData);
            return true;
        }
    }

    private void UpdateView()
    {
        _resultView.UpdateResultView(_plusElements, _minusElements);
        OnResultUpdated?.Invoke(_plusElements, _minusElements.Count);
    }
    private void ClearView()
    {
        _plusElements?.Clear();
        _minusElements?.Clear();
        UpdateView();
    }
}