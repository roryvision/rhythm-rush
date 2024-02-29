using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> lanes = new List<GameObject>();
    public GameObject laneOne;
    public GameObject laneTwo;
    public GameObject laneThree;
    public GameObject laneFour;

    public int score = 0;
    public int combo = 0;
    public int highestCombo = 0;
    public int health = 4;

    public TextMeshProUGUI scoreText;
    public GameObject comboContainer;
    public TextMeshProUGUI comboText;
    public GameObject[] healthBars;

    private UserManager user;

    void Start()
    {
        user = GameObject.FindGameObjectWithTag("Player").GetComponent<UserManager>();

        lanes.Add(laneOne);
        lanes.Add(laneTwo);
        lanes.Add(laneThree);
        lanes.Add(laneFour);

        Camera mainCamera = Camera.main;
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            mainCamera.orthographicSize = 15f;
        }
    }

    void Update()
    {
        if (health == 0)
        {
            LoadSceneSummary();
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd + (scoreToAdd * combo / 100);
        scoreText.text = score.ToString();
    }

    public void AddCombo(int comboToAdd) {
        combo += comboToAdd;
        if (combo >= 2)
        {
            comboText.text = "x" + combo.ToString();
            comboContainer.SetActive(true);
        }
        else
        {
            comboContainer.SetActive(false);
        }
        highestCombo = Mathf.Max(combo, highestCombo);
    }

    public void SubtractHealth()
    {
        if (health > 0)
        {
            health--;
            for (int i = 0; i < healthBars.Length; i++)
            {
                if (i < health)
                {
                    healthBars[i].SetActive(true);
                }
                else
                {
                    healthBars[i].SetActive(false);
                }
            }
        }
    }

    public void LoadSceneSummary()
    {
        user.currScore = score;
        user.updateHighScore();
        user.currCombo = highestCombo;
        user.currHealth = health;
        user.updateStars();

        SceneManager.LoadScene("Summary");
    }
}
