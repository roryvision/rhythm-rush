using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectManager : MonoBehaviour
{
    private UserManager user;
    public TextMeshProUGUI highScoreText;
    public GameObject[] starsGraphic;

    void Start()
    {
        user = GameObject.FindGameObjectWithTag("Player").GetComponent<UserManager>();

        highScoreText.text = user.highScore.ToString();

        for (int i = 0; i < starsGraphic.Length; i++)
        {
            if (i < user.stars)
            {
                starsGraphic[i].SetActive(true);
            }
            else
            {
                starsGraphic[i].SetActive(false);
            }
        }
    }

    public void LoadSceneGame()
    {
        SceneManager.LoadScene("Loading");
    }

    public void LoadSceneHome()
    {
        SceneManager.LoadScene("Home");
    }
}
