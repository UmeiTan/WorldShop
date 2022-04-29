using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ReputationValue : Value
{
    [SerializeField] private Slider _slider;
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
        UpdateValue(0);
    }
    private void OnDisable()
    {
        Disable();
        AwardController.OnAwardReputationIssued -= UpdateValue;
    }
    private void UpdateValue(int value)
    {
        _value = ChangeValue(value);
        UpdateSlider();
    }
    private void UpdateSlider()
    {
        _slider.value = _value;
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