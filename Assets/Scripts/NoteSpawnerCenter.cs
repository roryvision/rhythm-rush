using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnerCenter : MonoBehaviour
{
    private Conductor conductor;
    public GameObject noteFlipPrefab;
    public GameObject noteShakePrefab;
    public float[] notesFlip = { };
    public float[] notesShake = { };
    private int nextIndexFlip = 999;
    private int nextIndexShake = 999;

    void Start()
    {
        conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();

        for (int i = 0; i < notesFlip.Length; i++)
        {
            if (notesFlip[i] >= conductor.startingPosInBeats)
            {
                nextIndexFlip = i;
                break;
            }
        }
        for (int i = 0; i < notesShake.Length; i++)
        {
            if (notesShake[i] >= conductor.startingPosInBeats)
            {
                nextIndexShake = i;
                break;
            }
        }
    }

    void Update()
    {
        if (nextIndexFlip < notesFlip.Length && notesFlip[nextIndexFlip] < conductor.songPosInBeats)
        {
            SpawnNote(noteFlipPrefab);
            nextIndexFlip++;
        }
        if (nextIndexShake < notesShake.Length && notesShake[nextIndexShake] < conductor.songPosInBeats)
        {
            SpawnNote(noteShakePrefab);
            nextIndexShake++;
        }
    }

    void SpawnNote(GameObject prefab)
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
