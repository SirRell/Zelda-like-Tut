using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    static InfoManager instance;
    public static InfoManager Instance { get { return instance; } }
    public Vector2 NewPlayerPosition { get; set; }
    public Vector2 NewCameraBoundsMin, NewCameraBoundsMax;
    public float PlayerHealth { get; set; }
    public int CommonKeys { get; set; }
    public int UnCommonKeys { get; set; }
    public int BossKeys { get; set; }
    public int Money { get; set; }
    public List<Items> items;
    public Dictionary<string, bool> chests;
    public Dictionary<string, bool> buttons;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += InitiateLevel;
        chests = new Dictionary<string, bool>();
        buttons = new Dictionary<string, bool>();
    }

    public void InitiateLevel(Scene scene, LoadSceneMode mode)
    {
        GameObject.FindWithTag("Transition").GetComponent<UnityEngine.UI.Image>().enabled = true;
    }

    public void UpdateStats()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerHealth = player.GetComponent<Player>().currHealth;
        items = player.GetComponent<Inventory>().MyItems;
        CommonKeys = player.GetComponent<Inventory>().commonKeys;
        UnCommonKeys = player.GetComponent<Inventory>().uncommonKeys;
        BossKeys = player.GetComponent<Inventory>().bossKeys;
        Money = player.GetComponent<Inventory>().money;


        //I may need this method when I start actually saving games
        //Chest[] chestsArray = FindObjectsOfType<Chest>();
        //foreach (Chest currentChest in chestsArray)
        //{
        //    chests[currentChest.name] = currentChest.isOpen;
        //}
    }
}
