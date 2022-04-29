using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Value : MonoBehaviour
{
    [SerializeField] private Text _textValue;
    protected string _nameValue;
    private int _value = 0;

    protected void Enable()
    {
        SaveLoadGame.OnDataSaved += Save;
        SaveLoadGame.OnDataLoading += Load;
        SaveLoadGame.OnDefaultDataLoading += LoadDefaultState;
    }
    protected void Disable()
    {
        SaveLoadGame.OnDataSaved -= Save;
        SaveLoadGame.OnDataLoading -= Load;
        SaveLoadGame.OnDefaultDataLoading += LoadDefaultState;
    }

    private XElement Save()
    {
        XElement rootNode = new(_nameValue, _value);
        return rootNode;
    }
    protected virtual void Load(XElement root)
    {
        XElement rootNode = root.Element(_nameValue);
        _value = int.Parse(rootNode.Value);
        UpdateView();
    }
    protected virtual void LoadDefaultState(){}
    private void UpdateView()
    {
        _textValue.text = _value.ToString();
    }

    protected int ChangeValue(int value)
    {
        if (_value + value >= 0)
        {
            _value += value;
        }
        else
        {
            _value = 0;
        }
        UpdateView();
        return _value;
    }

}