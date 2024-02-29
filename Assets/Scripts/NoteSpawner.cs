using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    private Conductor conductor;
    public GameObject noteTapPrefab;
    public GameObject noteHoldPrefab;
    public GameObject noteSwipePrefab;
    public string laneTag;
    private GameObject note;
    public float[] notesTap = { };
    public float[] notesHold = { };
    public float[] notesSwipe = { };
    private int nextIndexTap = 999;
    private int nextIndexHold = 999;
    private int nextIndexSwipe = 999;

    void Start()
    {
        conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();

        for (int i = 0; i < notesTap.Length; i++)
        {
            if (notesTap[i] >= conductor.startingPosInBeats)
            {
                nextIndexTap = i;
                break;
            }
        }
        for (int i = 0; i < notesHold.Length; i++)
        {
            if (notesHold[i] >= conductor.startingPosInBeats)
            {
                nextIndexHold = i;
                break;
            }
        }
        for (int i = 0; i < notesSwipe.Length; i++)
        {
            if (notesSwipe[i] >= conductor.startingPosInBeats)
            {
                nextIndexSwipe = i;
                break;
            }
        }
    }

    void Update()
    {
        if (nextIndexTap < notesTap.Length && notesTap[nextIndexTap] < conductor.songPosInBeats)
        {
            SpawnNote(noteTapPrefab);
            nextIndexTap++;
        }
        if (nextIndexHold < notesHold.Length && notesHold[nextIndexHold] < conductor.songPosInBeats)
        {
            SpawnNote(noteHoldPrefab);
            nextIndexHold++;
        }
        if (nextIndexSwipe < notesSwipe.Length && notesSwipe[nextIndexSwipe] < conductor.songPosInBeats)
        {
            SpawnNote(noteSwipePrefab);
            nextIndexSwipe++;
        }
    }

    void SpawnNote(GameObject prefab)
    {
        note = Instantiate(prefab, transform.position, transform.rotation);
        note.tag = laneTag;
    }
}
