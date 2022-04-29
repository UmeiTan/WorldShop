public class DiamondValue : Value
{
    private static int _value = 0;
    private int _defaultValue = 10;

    private void Start()
    {
        _nameValue = "Diamond";
    }
    private void OnEnable()
    {
        Enable();
        AwardController.OnAwardDiamondIssued += UpdateValue;
        ChangeValue(_value);
    }
    private void OnDisable()
    {
        Disable();
        AwardController.OnAwardDiamondIssued -= UpdateValue;
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
