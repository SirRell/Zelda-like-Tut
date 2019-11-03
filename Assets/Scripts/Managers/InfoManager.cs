using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    static InfoManager instance;
    public static InfoManager Instance { get { return instance; } }
    public Vector2 NewPlayerPosition { get; set; }
    public float PlayerHealth { get; set; }
    public int Keys { get; set; }
    public List<Items> items;
    public Dictionary<string, bool> chests;

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
        Keys = player.GetComponent<Inventory>().commonKeys;


        //I may need this method when I start actually saving games
        //Chest[] chestsArray = FindObjectsOfType<Chest>();
        //foreach (Chest currentChest in chestsArray)
        //{
        //    chests[currentChest.name] = currentChest.isOpen;
        //}
    }
}
