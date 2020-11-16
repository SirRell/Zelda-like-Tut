using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    static InfoManager instance;
    public static InfoManager Instance { get { return instance; } }
    public Vector2 NewPlayerPosition { get; set; }
    public float PlayerMaxHealth { get; set; }
    public float PlayerHealth { get; set; }
    public float PlayerMagic { get; set; }
    public int AmmoLeft { get; set; }
    public int CommonKeys { get; set; }
    public int UnCommonKeys { get; set; }
    public int BossKeys { get; set; }
    public int Money { get; set; }
    public List<Item> items;
    public Dictionary<string, bool> chests = new Dictionary<string, bool>();
    public Dictionary<string, bool> buttons = new Dictionary<string, bool>();
    public Dictionary<string, DoorType> doors = new Dictionary<string, DoorType>();

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
    }

    public void InitiateLevel(Scene scene, LoadSceneMode mode)
    {
        GameObject.FindWithTag("Transition").GetComponent<UnityEngine.UI.Image>().enabled = true;
    }

    public void UpdateStats()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Inventory playerInventory = player.GetComponent<Inventory>();
        PlayerMaxHealth = player.maxHealth;
        PlayerHealth = player.currHealth;
        items = playerInventory.MyItems;
        CommonKeys = playerInventory.commonKeys;
        UnCommonKeys = playerInventory.uncommonKeys;
        BossKeys = playerInventory.bossKeys;
        Money = playerInventory.money;
        AmmoLeft = playerInventory.currentAmmo;


        //I may need this method when I start actually saving games
        //Chest[] chestsArray = FindObjectsOfType<Chest>();
        //foreach (Chest currentChest in chestsArray)
        //{
        //    chests[currentChest.name] = currentChest.isOpen;
        //}
    }
}
