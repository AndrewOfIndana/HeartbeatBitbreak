using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncBeat : MonoBehaviour
{
    public static SyncBeat Instance { get; private set; }

    

    public enum State {WAITING, STARTUP, PLAYING, RESET }
    private bool StartUpTriggered = false;
    private bool ResetTriggered = false;

    

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
        this.CurrentState = State.STARTUP;
    }

    public void InactiveBeat()
    {
        this.CurrentState = State.WAITING;
    }

    public State GetCurrentState() {
        return this.CurrentState;
    }

    private void Update()
    {
        if (StartUpTriggered)
        {
            this.CurrentState = State.PLAYING;
            StartUpTriggered = false;
        }
        else if (ResetTriggered) 
        {
            this.CurrentState = State.WAITING;
            ResetTriggered = false; 
        }
    }

    private void LateUpdate()
    {
        if (CurrentState == State.STARTUP)
        {
            StartUpTriggered = true;
        }
        else if (CurrentState == State.RESET)
        {
            ResetTriggered = true;
        }
    }
}
