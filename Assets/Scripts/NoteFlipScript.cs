using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFlipScript : MonoBehaviour
{
    private GameManager game;
    public float scaleSpeed = 0.5f;
    private Vector3 initialAccel;
    private Vector3 currentAccel;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        initialAccel = Input.acceleration;
        currentAccel = Vector3.zero;
    }

    void Update()
    {
        transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;

        if (transform.localScale.x <= 0f)
        {
            transform.localScale = Vector3.zero;
            Missed();
        }

        currentAccel = Vector3.Lerp(currentAccel, Input.acceleration - initialAccel, Time.deltaTime);
        
        if (currentAccel.z > 0.1f)
        {
            Hit();
        }
    }

    void Hit()
    {
        game.AddScore(500);
        game.AddCombo(1);
        Destroy(gameObject);
    }

    void Missed()
    {
        game.combo = 0;
        game.comboContainer.SetActive(false);
        game.SubtractHealth();
        Destroy(gameObject);
    }
}
