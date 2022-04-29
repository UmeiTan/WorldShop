using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    [SerializeField][Range(10, 100)] private int _baseReputation = 50;
    [SerializeField][Range(1, 10)] private float _awardMultiplierReputation = 1f; //можно увеличить и больше 10 мб
    [SerializeField][Range(1, 10)] private float _awardMultiplierMoney = 1f;
    [SerializeField][Range (1, 10)] private int _baseReputationPenalty = 1;

    [SerializeField][Range(8, 100)] private int _maxCards = 10;
    [SerializeField][Range(1, 3)] private int _maxCardGrade = 1;
    [SerializeField][Range(4, 16)] private int _numberElements = 4;
    [SerializeField][Range(1, 10)] private int _minClientsSpawn = 1;
    [SerializeField][Range(1, 20)] private int _numberClients = 4;


    public int BaseReputation => _baseReputation;
    public float AwardMultiplierReputation => _awardMultiplierReputation;
    public float AwardMultiplierMoney => _awardMultiplierMoney;
    public int BaseReputationPenalty => _baseReputationPenalty;

    public int MaxCards => _maxCards;
    public int MaxCardGrade => _maxCardGrade;
    public int NumberElements => _numberElements;
    public int MinClientsSpawn => _minClientsSpawn;
    public int NumberClients => _numberClients;

}