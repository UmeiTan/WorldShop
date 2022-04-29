public class MoneyValue : Value
{
    private static int _value = 0;
    private int _defaultValue = 100;

    private void Start()
    {
        _nameValue = "Money";
    }

    private void OnEnable()
    {
        Enable();
        AwardController.OnAwardMoneyIssued += UpdateValue;
        ChangeValue(_value);
    }
    private void OnDisable()
    {
        Disable();
        AwardController.OnAwardMoneyIssued -= UpdateValue;
    }

    private void UpdateValue(int value)
    {
        _value = ChangeValue(value);
    }
    protected override void LoadDefaultState()
    {
        _value = _defaultValue;
        ChangeValue(_defaultValue);
    }
}
