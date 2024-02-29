using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public int currScore = 0;
    public int highScore = 0;
    public int currCombo = 0;
    public int currHealth = 4;
    public int currStars = 0;
    public int stars = 0;

    public static UserManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void updateHighScore()
    {
        highScore = Mathf.Max(currScore, highScore);
    }

    public void updateStars()
    {
        currStars = 0;

        switch (currHealth)
        {
            case 1:
                currStars = 1;
                break;
            case 2:
            case 3:
                currStars = 2;
                break;
            case 4:
                currStars = 3;
                break;
            default:
                currStars = 0;
                break;
        }

        stars = Mathf.Max(currStars, stars);
    }
}
