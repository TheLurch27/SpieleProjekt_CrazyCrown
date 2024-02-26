using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<string> collectedItems = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveInventory()
    {
        PlayerPrefs.SetString("Inventory", string.Join(",", collectedItems.ToArray()));
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey("Inventory"))
        {
            string[] items = PlayerPrefs.GetString("Inventory").Split(',');
            collectedItems.AddRange(items);
        }
    }

    public void ClearInventory()
    {
        collectedItems.Clear();
    }

    public bool HasItem(string itemName)
    {
        return collectedItems.Contains(itemName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadInventory();
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }
}