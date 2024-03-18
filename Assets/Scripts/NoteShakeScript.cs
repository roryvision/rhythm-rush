using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteShakeScript : MonoBehaviour
{
    private GameManager game;
    public float scaleSpeed = 0.5f;

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    float lowPassKernelWidthInSeconds = 1.0f;
    float shakeDetectionThreshold = 2.0f;

    float lowPassFilterFactor;
    Vector3 lowPassValue;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
    }

    void Update()
    {
        transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;

        if (transform.localScale.x <= 0f)
        {
            transform.localScale = Vector3.zero;
            Missed();
        }

        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
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
