using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    private GameManager game;
    // private Conductor conductor;
    public GameObject lane;
    private SpriteRenderer childSpriteRenderer;

    public float moveSpeed = 5f;
    public float deadZone = 0f;
    // public float startBeat;
    private bool canTap = false;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        // conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
        // startBeat = conductor.songPosInBeats;

        foreach (GameObject l in game.lanes)
        {
            if (l.CompareTag(gameObject.tag))
            {
                lane = l;
            }
        }

        childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        childSpriteRenderer.sortingLayerName = gameObject.tag;
    }

    void Update()
    {
        // float distanceToMove = (startBeat - conductor.songPosInBeats) * conductor.secPerBeat * moveSpeed;
        // Vector3 moveDirection = new Vector3(-1, 1, 0).normalized;
        // transform.position += moveDirection * distanceToMove;

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
        }

        if (deadZone - 0.1f <= transform.position.x && transform.position.x <= deadZone + 0.1f)
        {
            Missed();
        }

        if (canTap && Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

                if (lane.GetComponent<Collider2D>().bounds.Contains(touchPosition))
                {
                    Hit();
                    canTap = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            canTap = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            canTap = false;
        }
    }

    void Hit()
    {
        float distance = Vector2.Distance(transform.position, lane.transform.position);
        float scoreToAdd = 100f * Mathf.Clamp01((1f - distance));

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
}
