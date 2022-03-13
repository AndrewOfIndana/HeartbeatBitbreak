using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncBeat : MonoBehaviour
{
    public static SyncBeat Instance { get; private set; }

    

    public enum State {WAITING,PLAYING }

    

    private State CurrentState = State.WAITING;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    

    public void StartBeat() {
        this.CurrentState = State.PLAYING;
    }

    public State GetCurrentState() {
        return this.CurrentState;
    }

}
