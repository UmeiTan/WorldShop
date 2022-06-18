using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ReputationValue : Value
{
    [SerializeField] private Image _slider;
    private static int _value = 0;
    private int _defaultValue = 50;

    private void Start()
    {
        _nameValue = "Reputation";
    }
    private void OnEnable()
    {
        Enable();
        AwardController.OnAwardReputationIssued += UpdateValue;
        SetValue(0);
    }
    private void OnDisable()
    {
        Disable();
        AwardController.OnAwardReputationIssued -= UpdateValue;
    }
    private void SetValue(int value)
    {
        _value = ChangeValue(value);
        UpdateSlider();
    }
    private void UpdateValue(int value)
    {
        _value = ChangeValue(value);
        if (_value <= 0)
        {
            SaveLoadGame.GameOver();
        }
        if (_value >= 100)
        {

        }
        UpdateSlider();
    }
    private void UpdateSlider()
    {
        _slider.fillAmount = _value/100f;
    }

    protected override void Load(XElement root)
    {
        base.Load(root);
        UpdateValue(0);
    }
    protected override void LoadDefaultState()
    {
        UpdateValue(_defaultValue);
    }
}