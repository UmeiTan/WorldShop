using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RequestController : MonoBehaviour
{
    public static event Action<List<int>> OnNewRequestCreated;

    [SerializeField] private RequestView _requestView;

    private LevelConfig _levelConfig;
    private List<int> _request;

    private void OnEnable()
    {
        LevelController.OnLevelChanged += GetLevelCongig;
        VisitorController.OnNewClientCreated += NewRequest;
        SaveLoadGame.OnDataSaved += SaveRequest;
        SaveLoadGame.OnDataLoading += LoadRequest;
    }
    private void OnDisable()
    {
        LevelController.OnLevelChanged -= GetLevelCongig;
        VisitorController.OnNewClientCreated -= NewRequest;
        SaveLoadGame.OnDataSaved -= SaveRequest;
        SaveLoadGame.OnDataLoading -= LoadRequest;
    }

    private void GetLevelCongig(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }
    public void NewRequest(Client client)
    {
        _request = new();
        for (int i = 0; i < client.NumberElementsRequested; i++)
        {
            _request.Add(Random.Range(0, _levelConfig.NumberElements));
        }
        _requestView.UpdateRequestView(_request);
        OnNewRequestCreated?.Invoke(_request);
    }
    public XElement SaveRequest()
    {
        XElement rootNode = new("Request");
        XElement node;

        foreach (var el in _request)
        {
            node = new("el", el.ToString());
            rootNode.Add(node);
        }

        return rootNode;
    }
    private void LoadRequest(XElement root)
    {
        XElement rootNode = root.Element("Request");
        _request = new();

        foreach (var el in rootNode.Elements("el"))
        {
            _request.Add(int.Parse(el.Value));
        }
        _requestView.UpdateRequestView(_request);
        OnNewRequestCreated?.Invoke(_request);
    }


}