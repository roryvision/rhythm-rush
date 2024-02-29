using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SummaryManager : MonoBehaviour
{
    private UserManager user;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI replayText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highestComboText;
    public GameObject[] starsGraphic;

    void Start()
    {
        user = GameObject.FindGameObjectWithTag("Player").GetComponent<UserManager>();

        if (user.currHealth > 0)
        {
            resultText.text = "SUCCESS";
            resultText.color = new Color(0.9411765f, 0.4235294f, 0f, 1f);

            replayText.text = "REPLAY";
        }

        finalScoreText.text = user.currScore.ToString();
        highestComboText.text = "x" + user.currCombo.ToString();

        for (int i = 0; i < starsGraphic.Length; i++)
        {
            if (i < user.currStars)
            {
                starsGraphic[i].SetActive(true);
            }
            else
            {
                starsGraphic[i].SetActive(false);
            }
        }
    }

    public void LoadSceneReplay()
    {
        SceneManager.LoadScene("Loading");
    }

    public void LoadSceneLobby()
    {
        SceneManager.LoadScene("SongSelect");
    }
}
