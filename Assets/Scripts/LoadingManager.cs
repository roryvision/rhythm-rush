using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Slider progressBar;

    private float loadingTime = 3f;
    private float elapsedTime = 0f;

    void Start()
    {
        StartCoroutine(LoadingGame());
    }

    IEnumerator LoadingGame()
    {
        yield return new WaitForSeconds(1f);

        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / loadingTime;
            progressBar.value = progress;
            yield return null;
        }

        progressBar.value = 1f;
        SceneManager.LoadScene("Level_LeSserafim_NoCelestial");
    }
}
