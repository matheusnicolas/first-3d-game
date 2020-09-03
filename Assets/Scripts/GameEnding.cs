using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    bool isPlayerAtExit = false;
    bool isPlayerCaught = false;
    float timer;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAtExit) EndLevel(exitBackgroundImageCanvasGroup, false);
        else if (isPlayerCaught) EndLevel(caughtBackgroundImageCanvasGroup, true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) {
            isPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer/fadeDuration;
        if (timer > fadeDuration + displayImageDuration) {
            if (doRestart) {
                SceneManager.LoadScene(0);
            } else {
                Application.Quit();
            }
        }
    }
}
