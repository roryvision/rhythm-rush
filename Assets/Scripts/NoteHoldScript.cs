using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHoldScript : MonoBehaviour
{
    private GameManager game;
    public GameObject lane;

    public float moveSpeed = 5f;
    public bool canBePressed = false;
    public bool canBeReleased = false;
    public bool isHolding = false;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        foreach (GameObject l in game.lanes)
        {
            if (l.CompareTag(gameObject.tag))
            {
                lane = l;
            }
        }

        foreach (Transform child in transform)
        {
            SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
            childSpriteRenderer.sortingLayerName = gameObject.tag;
        }
    }

    void Update()
    {
        switch (gameObject.tag)
        {
            case "LaneOne":
                transform.position += (Vector3.right * moveSpeed + Vector3.down * moveSpeed) * Time.deltaTime;
                break;
            case "LaneTwo":
                transform.position += (Vector3.left * moveSpeed + Vector3.down * moveSpeed) * Time.deltaTime;
                break;
            case "LaneThree":
                transform.position += (Vector3.left * moveSpeed + Vector3.up * moveSpeed) * Time.deltaTime;
                break;
            case "LaneFour":
                transform.position += (Vector3.right * moveSpeed + Vector3.up * moveSpeed) * Time.deltaTime;
                break;
            default:
                break;
        }

        if (canBePressed && Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

                if (lane.GetComponent<Collider2D>().bounds.Contains(touchPosition))
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            isHolding = true;
                            break;
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            isHolding = true;
                            break;
                        case TouchPhase.Ended:
                        case TouchPhase.Canceled:
                            if (canBeReleased)
                            {
                                Hit();
                            } else {
                                Missed();
                            }
                            break;
                    }
                }
            }
        }
    }

    public void Hit()
    {
        game.AddScore(200);
        game.AddCombo(1);
        Destroy(gameObject);
    }

    public void Missed()
    {
        game.combo = 0;
        game.comboContainer.SetActive(false);
        game.SubtractHealth();
        Destroy(gameObject);
    }
}
