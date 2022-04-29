using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class LevelController : MonoBehaviour
{
    public static event Action<LevelConfig> OnLevelChanged;

    [SerializeField] private List<LevelConfig> _levelConfigs;
    
    private int _currentLevel = 0;

    private void OnEnable()
    {
        SaveLoadGame.OnDataSaved += SaveLevelState;
        SaveLoadGame.OnDataLoading += LoadLevelState;
        SaveLoadGame.OnDefaultDataLoading += SetDefaultState;
    }
    private void OnDisable()
    {
        SaveLoadGame.OnDataSaved -= SaveLevelState;
        SaveLoadGame.OnDataLoading -= LoadLevelState;
        SaveLoadGame.OnDefaultDataLoading -= SetDefaultState;
    }

    public void LoadLevel()
    {
        OnLevelChanged.Invoke(_levelConfigs[_currentLevel]);
    }

    private XElement SaveLevelState()
    {
        XElement rootNode = new("LevelState");
        XElement node;
        //XElement element;

        node = new("CurrentLevel");
        node.SetValue(_currentLevel.ToString());
        rootNode.Add(node);

        return rootNode;
    }
    private void LoadLevelState(XElement root)
    {
        XElement rootNode = root.Element("LevelState");
        XElement node;

        node = rootNode.Element("CurrentLevel");
        _currentLevel = int.Parse(node.Value);
        OnLevelChanged?.Invoke(_levelConfigs[_currentLevel]);
    }
    private void SetDefaultState()
    {
        _currentLevel = 0;
        OnLevelChanged?.Invoke(_levelConfigs[_currentLevel]);
    }


}
