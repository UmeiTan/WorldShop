using System.Diagnostics;
using System.IO;
using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadGame : MonoBehaviour
{
    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _game;

    public delegate XElement DataSaved();

    public static event DataSaved OnDataSaved;
    public static event Action<XElement> OnDataLoading;
    public static event Action OnDefaultDataLoading;

    private static string _savePath;
    private static string _saveBackupPath;

    private void Awake()
    {
        _savePath = Application.persistentDataPath + "/save.xml";
        _saveBackupPath = Application.persistentDataPath + "/backup.xml";
    }
    private void Start()
    {
        Load();
    }
    public static void Save()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        XDocument xmlDoc = new();
        XElement rootNode = new("ROOT");

        xmlDoc.Add(rootNode);

        Delegate[] savedData = OnDataSaved?.GetInvocationList();

        for (int i = 0; i < savedData.Length; ++i)
        {
            rootNode.Add(savedData[i].DynamicInvoke());
        }
        xmlDoc.Save(_savePath);
        stopwatch.Stop();
        TimeSpan ts = stopwatch.Elapsed;
        UnityEngine.Debug.Log(string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
    }
    private static void SaveBackup()
    {
        if (File.Exists(_savePath))
        {
            File.Copy(_savePath, _saveBackupPath, true);
        }
        else
        {
            UnityEngine.Debug.LogError("Файл сохранения не найден");
        }
    }
    public void Load()
    {
        XElement root = null;

        if (File.Exists(_savePath))
        {
            root = XDocument.Parse(File.ReadAllText(_savePath)).Element("ROOT");

            OnDataLoading?.Invoke(root);
        }
        else if (File.Exists(_saveBackupPath))
        {
            root = XDocument.Parse(File.ReadAllText(_saveBackupPath)).Element("ROOT");

            OnDataLoading?.Invoke(root);
        }
        else
        {
            OnDefaultDataLoading?.Invoke();
            Save();
            SaveBackup();
        }
        //_game.SetActive(true);
        //_start.SetActive(false);
    }
    public static void GameOver()
    {
        //if (File.Exists(_saveBackupPath) && File.Exists(_savePath))
        //{
        //    XElement save = XDocument.Parse(File.ReadAllText(_savePath)).Element("ROOT");
        //    XElement saveBackup = XDocument.Parse(File.ReadAllText(_saveBackupPath)).Element("ROOT");

        //    save.Element("Deck").RemoveNodes();
        //    save.Element("Deck").Add(saveBackup.Element("Deck").Elements());

        //    save.Element("Reputation").Value = saveBackup.Element("Reputation").Value;

        //    save.Element("Visitors").RemoveNodes();
        //    save.Element("Visitors").Add(saveBackup.Element("Visitors").Elements());

        //    // = saveBackup.Element("Deck");
        //    save.Save(_savePath);
        //    SceneManager.LoadScene(0);
        //}
        //else 
        if (File.Exists(_savePath))
        {
            File.Delete(_savePath);
            if (File.Exists(_saveBackupPath))
            {
                File.Delete(_saveBackupPath);
            }
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

}