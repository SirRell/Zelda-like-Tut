using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerStartPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InfoManager.Instance.PlayerPosition = playerStartPosition;
            InfoManager.Instance.Unsubscribe();
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
