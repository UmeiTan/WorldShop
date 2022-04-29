using UnityEngine;

[CreateAssetMenu]
public class DayConfig : ScriptableObject
{
    //шанс прихода нипов
    [SerializeField] private bool _spawnChanceLandlord = false;
    [SerializeField][Range(0, 100)] private int _spawnChanceTrader = 10;
    [SerializeField][Range(0, 100)] private int _spawnChanceSecretTrader = 5;

    //погодные явления (по дефолту х1)
    [SerializeField][Range (0, 100)] private int _spawnChanceCloudy = 30;//x0.7
    [SerializeField][Range (0, 100)] private int _spawnChanceStorm = 20;//x0.5 затем радуга и х2
    [SerializeField][Range (0, 100)] private int _spawnChanceMagneticStorm = 10;//x3

    //другие события
    [SerializeField][Range(0, 100)] private int _chancePriceIncrease = 15;
    [SerializeField][Range(0, 100)] private int _chancePriceFall = 15;

    public bool SpawnChanceLandlord => _spawnChanceLandlord;
    public int SpawnChanceTrader => _spawnChanceTrader;
    public int SpawnChanceSecretTrader => _spawnChanceSecretTrader;

    public int SpawnChanceCloudy => _spawnChanceCloudy;
    public int SpawnChanceStorm => _spawnChanceStorm;
    public int SpawnChanceMagneticStorm => _spawnChanceMagneticStorm;

    public int ChancePriceIncrease => _chancePriceIncrease;
    public int ChancePriceFall => _chancePriceFall;
}