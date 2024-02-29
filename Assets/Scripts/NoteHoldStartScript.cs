using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHoldStartScript : MonoBehaviour
{
    private NoteHoldScript holdParent;
    void Start()
    {
        holdParent = gameObject.GetComponentInParent<NoteHoldScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(holdParent.lane.tag))
        {
            holdParent.canBePressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(holdParent.lane.tag))
        {
            if (!holdParent.isHolding)
            {
                holdParent.canBePressed = false;
                holdParent.Missed();
            }
        }
    }
}
