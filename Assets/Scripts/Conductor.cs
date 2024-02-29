using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    private GameManager game;
    public AudioSource musicSource;

    public float songBpm;
    public float secPerBeat;
    public float songPosition;
    public float songPosInBeats;
    public float dspSongTime;
    public float travelTimeInBeats;
    public float startingPosInSeconds;
    public float startingPosInBeats;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;
        dspSongTime = (float) AudioSettings.dspTime;

        startingPosInSeconds = startingPosInBeats * secPerBeat;
        musicSource.time = startingPosInSeconds;

        musicSource.Play();
    }

    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - dspSongTime);
        songPosInBeats = songPosition / secPerBeat + travelTimeInBeats + startingPosInBeats;

        if (songPosition >= musicSource.clip.length)
        {
            game.LoadSceneSummary();
        }
    }
}
