using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHoldEndScript : MonoBehaviour
{
    private NoteHoldScript holdParent;
    public float deadZone = 0f;

    void Start()
    {
        holdParent = gameObject.GetComponentInParent<NoteHoldScript>();
    }

    void Update()
    {
        if (deadZone - 0.1f <= transform.position.x && transform.position.x <= deadZone + 0.1f)
        {
            if (!holdParent.isHolding)
            {
                holdParent.Missed();
            } else
            {
                holdParent.Hit();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(holdParent.lane.tag))
        {
            holdParent.canBeReleased = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(holdParent.lane.tag))
        {
            if (!holdParent.isHolding)
            {
                holdParent.Missed();
            }
        }
    }
}
