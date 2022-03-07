using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //the number of beats in each loop
    public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;

    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;


    //userinput timing
    [Header("Beat Timing")]
    //window in seconds for a perfect hit
    public float perfectHitWindow;

    //window in seconds for a  hit
    public float hitWindow;



    //Conductor instance
    public static Conductor instance;


    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //calculate the loop position
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
        {
            completedLoops++;
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;
        Debug.Log("Loop");

        }

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;

        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / secPerBeat;

        //TEST

        //Debug.Log(Mathf.Floor(songPositionInBeats));
    }

    public int CheckHitTiming() {
        //Checks if if the song is at a beat.
        //Returns 0 for a miss,  1 for a general hit, 2 for a perfect hit

        int returnVal = 0;
        float currentTime = (float)AudioSettings.dspTime;
        int beat = (int)Mathf.Floor(songPositionInBeats);

        float beat1TimeDif = Mathf.Abs(GetTimeForBeat(beat) - currentTime);
        float beat2TimeDif = Mathf.Abs(GetTimeForBeat(beat + 1) - currentTime);

        //Check if perfect hit

        if (beat1TimeDif <= perfectHitWindow || beat2TimeDif <= perfectHitWindow) {
            returnVal = 2;
        } else if (beat1TimeDif <= hitWindow || beat2TimeDif <=hitWindow) {

            returnVal = 1;
        }





        return  returnVal;
    }

    private float GetTimeForBeat(int beat) {

         return this.dspSongTime + (this.secPerBeat * beat);
    }
}
