using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    static InfoManager instance;
    public static InfoManager Instance { get { return instance; } }
    public Vector2 NewPlayerPosition { get; set; }
    public float PlayerHealth { get; set; }
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
        PlayerHealth = FindObjectOfType<Player>().currHealth;
    }
}
