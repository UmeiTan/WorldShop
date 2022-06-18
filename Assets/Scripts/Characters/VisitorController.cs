using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class VisitorController : MonoBehaviour
{
    public static event Action OnClientsEnded;
    public static event Action<Client> OnNewClientCreated;
    public static event Action<Client> OnClientLoaded;

    [SerializeField] private CharacterBase _characterBase;
    [SerializeField] private Text _numberClientsText;
    [SerializeField] private ClientView _clientView;
    [SerializeField] private Button _acceptClient;

    private NpcView _npcView;
    private LevelConfig _levelConfig;
    private DayConfig _dayConfig;

    private int _numberClients;
    private int _currentClientId;
    private int _npcId;
    private bool _clientAccepted;
    private bool _load = true;

    private void OnEnable()
    {
        SaveLoadGame.OnDataSaved += Save;
        SaveLoadGame.OnDataLoading += Load;
        SaveLoadGame.OnDefaultDataLoading += SetDefaultState;
        LevelController.OnLevelChanged += GetLevelCongig;
        CalendarController.OnDayChanged += GetDayCongig;
        AwardController.OnClientLeft += NextClient;
    }
    private void OnDisable()
    {
        SaveLoadGame.OnDataSaved -= Save;
        SaveLoadGame.OnDataLoading -= Load;
        SaveLoadGame.OnDefaultDataLoading -= SetDefaultState;
        LevelController.OnLevelChanged -= GetLevelCongig;
        CalendarController.OnDayChanged -= GetDayCongig;
        AwardController.OnClientLeft -= NextClient;
    }

    public XElement Save()
    {
        XElement rootNode = new("Visitors");
        XElement node;

        node = new("NumberClients", _numberClients.ToString());
        rootNode.Add(node);

        node = new("CurrentClientId", _currentClientId.ToString());
        rootNode.Add(node);

        node = new("ClientAccepted", _clientAccepted.ToString());
        rootNode.Add(node);

        node = new("NpcId", _npcId.ToString());
        rootNode.Add(node);

        return rootNode;
    }
    private void Load(XElement root)
    {
        XElement rootNode = root.Element("Visitors");
        XElement node;

        node = rootNode.Element("NumberClients");
        _numberClients = int.Parse(node.Value);

        node = rootNode.Element("CurrentClientId");
        _currentClientId = int.Parse(node.Value);

        node = rootNode.Element("ClientAccepted");
        _clientAccepted = bool.Parse(node.Value);
        if (_clientAccepted)
        {
            _acceptClient.onClick.Invoke();
        }
        else
        {
            _load = false;
        }

        node = rootNode.Element("NpcId");
        _npcId = int.Parse(node.Value);

        _clientView.View(_characterBase.Clients[_currentClientId]);
        _numberClientsText.text = _numberClients.ToString();
        OnClientLoaded?.Invoke(_characterBase.Clients[_currentClientId]);
    }
    private void SetDefaultState()
    {
        _numberClients = 3;
        _currentClientId = 0;
        _npcId = -1;
        _clientView.View(_characterBase.Clients[_currentClientId]);
        _numberClientsText.text = _numberClients.ToString();
        OnNewClientCreated?.Invoke(_characterBase.Clients[_currentClientId]);
    }

    private void GetLevelCongig(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }
    private void GetDayCongig(DayConfig dayConfig)
    {
        _dayConfig = dayConfig;
    }

    public void NextClient()
    {
        _clientAccepted = false;
        if (_numberClients > 0)
        {
            _currentClientId = Random.Range(0, _levelConfig.NumberClients);
            _clientView.View(_characterBase.Clients[_currentClientId]);
            _numberClients--;
            _numberClientsText.text = _numberClients.ToString();
            OnNewClientCreated?.Invoke(_characterBase.Clients[_currentClientId]);
        }
        else
        {
            OnClientsEnded?.Invoke();
            _numberClients = 5;
            _currentClientId = 1;
            _clientView.View(_characterBase.Clients[_currentClientId]);
            _numberClientsText.text = _numberClients.ToString();
            OnNewClientCreated?.Invoke(_characterBase.Clients[_currentClientId]);
        }
        SaveLoadGame.Save();
    }

    public void AcceptClient()
    {
        _clientAccepted = true;
        if (!_load)
        {
            SaveLoadGame.Save();
        }
        else
        {
            _load = false;
        }
            
    }
    public void KickClient()
    {
        //+stat kick
        //NextClient();
        SaveLoadGame.Save();
    }
}