using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSwipeScript : MonoBehaviour
{
    private GameManager game;
    public GameObject lane;

    public float moveSpeed = 5f;
    public float deadZone = 0f;
    private bool canSwipe = false;
    private bool wasSuccess = false;
    private Vector3 swipeStart;

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

    // Update is called once per frame
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

        if (deadZone - 0.1f <= transform.position.x && transform.position.x <= deadZone + 0.1f)
        {
            Missed();
        }

        if (canSwipe && Input.touchCount > 0)
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
                            swipeStart = touchPosition;
                            break;
                        case TouchPhase.Moved:
                            Vector3 swipeDelta = touchPosition - swipeStart;
                            if (swipeDelta.magnitude > 0.3)
                            {
                                float angle = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;
                                string direction = GetSwipeDirection(angle);
                                if (lane.CompareTag(direction))
                                {
                                    wasSuccess = true;
                                    Hit();
                                }
                            }
                            break;
                        case TouchPhase.Ended:
                        case TouchPhase.Canceled:
                            if (!wasSuccess)
                            {
                                Missed();
                            }
                            break;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            canSwipe = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            canSwipe = false;
        }
    }

    void Hit()
    {
        float distance = Vector2.Distance(transform.position, lane.transform.position);
        float scoreToAdd = 300f * Mathf.Clamp01((1f - distance));

        game.AddScore((int)scoreToAdd);
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

    string GetSwipeDirection(float angle)
    {
        if (0 <= angle && angle < 90)
        {
            return "LaneOne";
        }
        else if (-90 < angle && angle <= 0)
        {
            return "LaneTwo";
        }
        else if (-180 < angle && angle <= -90)
        {
            return "LaneThree";
        }
        else
        {
            return "LaneFour";
        }
    }
}
