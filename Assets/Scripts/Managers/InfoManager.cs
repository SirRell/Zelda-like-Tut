using UnityEngine;
using UnityEngine.SceneManagement;
public class InfoManager : MonoBehaviour
{
    static InfoManager instance;
    public static InfoManager Instance { get { return instance; } }
    public Vector2 PlayerPosition { get; set; }
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
            //SceneManager.sceneLoaded += LevelWasLoaded;
        }
    }

    public void Unsubscribe()
    {
        PlayerHealth = FindObjectOfType<Player>().currHealth;
        //FindObjectOfType<Player>().ReceivedDamage -= PlayerReceivedDamage;
    }

    //void LevelWasLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (instance == this)
    //    {
    //        Subscribe();
    //    }
    //}

    //void Subscribe()
    //{
    //    FindObjectOfType<Player>().ReceivedDamage += PlayerReceivedDamage;
    //}

    //private void PlayerReceivedDamage()
    //{

    //}

}
