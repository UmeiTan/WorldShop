using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwardController : MonoBehaviour
{
    public static event Action<int> OnAwardMoneyIssued;
    public static event Action<int> OnAwardReputationIssued;
    public static event Action<int> OnAwardDiamondIssued;
    public static event Action OnClientLeft;

    [SerializeField] private GameObject _positive;
    [SerializeField] private GameObject _negative;
    [SerializeField] private Text _moneyPreview;
    [SerializeField] private Text _reputationPreview;

    private int _maxAwardMoney;
    private List<int> _request;
    private (int money, int reputation) temp = (0, 0);

    private void OnEnable()
    {
        VisitorController.OnNewClientCreated += GetClient;
        VisitorController.OnClientLoaded += GetClient;
        RequestController.OnNewRequestCreated += GetRequest;
        ResultHandler.OnResultUpdated += AwardPreview;
    }
    private void OnDisable()
    {
        VisitorController.OnNewClientCreated -= GetClient;
        VisitorController.OnClientLoaded -= GetClient;
        RequestController.OnNewRequestCreated -= GetRequest;
        ResultHandler.OnResultUpdated -= AwardPreview;
    }
    private void GetClient(Client client)
    {
        _maxAwardMoney = client.BaseAwardMoney[0];//TODO после добавления прокачки персонажа нужно получать его уровень
    }
    private void GetRequest(List<int> elements)
    {
        _request = new List<int>(elements);
    }

    private void AwardPreview(List<int> plus, int minus)
    {
        if (minus == 0 && plus.Count != 0)
        {
            temp = AwardCalculation(plus);
            if (temp.money != 0)
            {
                _negative.SetActive(false);
                _positive.SetActive(true);
                _moneyPreview.text = temp.money.ToString();
                _reputationPreview.text = temp.reputation.ToString();
                return;
            }
        }
        _positive.SetActive(false);
        _negative.SetActive(true);
    }
    public void IssueAward()
    {
        if (temp.reputation == 0)
        {
            OnAwardReputationIssued?.Invoke(-5);
        }
        else
        {
            OnAwardMoneyIssued?.Invoke(temp.money);
            OnAwardReputationIssued?.Invoke(temp.reputation);
        }
        temp = (0, 0);
        OnClientLeft?.Invoke();
    }
    private (int money, int reputation) AwardCalculation(List<int> plus)
    {
        int money = 0, reputation = 0, matchingElements = 0, extraAward = 0;
        bool extra = true;
        List<int> temp = new List<int>(_request);

        if (plus.Count == _request.Count)
        {
            for (int i = 0; i < plus.Count; i++)
            {
                for (int j = 0; j < temp.Count; j++)
                {
                    if (plus[i] == temp[j])
                    {
                        if (extra) extraAward++;
                        matchingElements++;
                        temp.RemoveAt(j);
                        break;
                    }
                    extra = false;
                }
            }
            if (plus.Count != matchingElements) return (0, 0);
            if (!extra && plus[0] == _request[0] && plus[plus.Count - 1] == _request[_request.Count - 1]) 
                extraAward++;

            money = _maxAwardMoney + Mathf.RoundToInt(_maxAwardMoney * 0.25f * extraAward);
            reputation = 3 + _request.Count - Mathf.RoundToInt(_request.Count * 0.45f);
        }
        else
        {
            for (int i = 0; i < plus.Count; i++)
            {
                for (int j = 0; j < temp.Count; j++)
                {
                    if (plus[i] == temp[j])
                    {
                        matchingElements++;
                        temp.RemoveAt(j);
                        break;
                    }
                }
            }
            if (plus.Count != matchingElements) return (0, 0);
            if (matchingElements < Mathf.RoundToInt(_request.Count*0.45f)) return (0, 0);

            var elementCost = _maxAwardMoney / 2 / (_request.Count - Mathf.RoundToInt(_request.Count * 0.45f));
            money = Mathf.RoundToInt(_maxAwardMoney / 2 + elementCost*(matchingElements - Mathf.RoundToInt(_request.Count * 0.45f)));
            reputation = 3 + matchingElements - Mathf.RoundToInt(_request.Count * 0.45f);
        }

        return (money, reputation);
    }

}