using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerStartPosition;

    public GameObject fadeOutImage;
    public float fadeWait = .33f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InfoManager.Instance.NewPlayerPosition = playerStartPosition;

            InfoManager.Instance.UpdateStats();
            StartCoroutine(FadeAndLoad(other));
        }
    }

    IEnumerator FadeAndLoad(Collider2D player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Transition").GetComponentInParent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation loadNewScene = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!loadNewScene.isDone)
            yield return null;

    }
}
