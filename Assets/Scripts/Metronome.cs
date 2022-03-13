using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [Header("Points")]
    public Transform StartPoint;
    public Transform EndPoint;

    [Header("Values")]
    public int NumberOfBeats;

    [Header("Beat Sprite")]
    public GameObject BeatTemplate;
    

    [Header("Beat Container")]
    public GameObject BeatHolder;

    [Header("Player Marker Sprite")]
    public GameObject MarkerTemplate;

    [Header("Progress Indicator")]
    public GameObject ProgressSprite;

    [Header("Timing")]
    public float TravelTime; //NEEDS TO BE SET BY ANOTHER SCRIPT
   

    private List<GameObject> BeatArray;
    private List<GameObject> MarkerArray;
    //Misc Variables

    float distance;
    float distanceBetweenBeats;

    float speedPerSec;

    private bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        MarkerTemplate.SetActive(false); //Keep marker template hidden visually
        StartBeat();

        
    }
    
    public void StartBeat() {
        
        GenerateBeatSprites();
        GenerateMarkers(new List<int> { 0, 4, 8, 12 }); //TEST LINE, NEEDS TO BE CORRECTLY IMPLEMENTED LATER
        ResetProgress();
    }
    //Generates UI Beat
    public void GenerateBeatSprites() {
        //Ensure array is empty
        BeatArray = new List<GameObject>();

        //Calculate distance
        distance = Mathf.Abs(this.StartPoint.position.x - this.EndPoint.position.x);
        distanceBetweenBeats = distance / NumberOfBeats;

        //Move BeatTemplate to StartingPoint
        BeatTemplate.transform.SetPositionAndRotation(this.StartPoint.position, BeatTemplate.transform.rotation);

        //Add BeatTemplate to list to keep track of it
        BeatArray.Add(BeatTemplate);

        GameObject temp;
        for (int i = 1; i < NumberOfBeats; i++) {
            temp = GameObject.Instantiate(BeatTemplate);
            temp.transform.SetParent(BeatHolder.transform);

            //Move new beat to appropriate position
            temp.transform.SetPositionAndRotation(this.StartPoint.position, BeatTemplate.transform.rotation);
            temp.transform.Translate(Vector3.right * (distanceBetweenBeats * i));

            //add beat to array

            BeatArray.Add(temp);


        }
    }

    public void GenerateMarkers(List<int> MarkerPositions) {
        //Each integer in MarkerPositions is the index of a beat that has a marker attached, starting from 0
        //The beat will be instantiated, attached to the beat as a child, move to the parents position, and offset
        GameObject temp;
        this.MarkerArray = new List<GameObject>();

        foreach (int element in MarkerPositions) {
            temp = GameObject.Instantiate(this.MarkerTemplate);
            temp.SetActive(temp);
            this.AttachMarker(temp, this.BeatArray[element]);
            this.MarkerArray.Add(temp);
        }

        
    }

    public void AttachMarker(GameObject Marker, GameObject ParentBeat) {
        //Attaches Marker to Gameobject, sets its position, and offsets it

        Marker.transform.SetParent(ParentBeat.transform);
        Marker.transform.SetPositionAndRotation(ParentBeat.transform.position, MarkerTemplate.transform.rotation);
        Marker.transform.Translate(Vector3.left * distanceBetweenBeats / 2);
    }

    public void ChangeBeatPosition(int MarkerPosition, int BeatPosition) {
        this.AttachMarker(this.MarkerArray[MarkerPosition], this.BeatArray[BeatPosition]);
    }

    // Update is called once per frame
    public void ResetProgress() {
        speedPerSec = distance / TravelTime;
        this.ProgressSprite.transform.SetPositionAndRotation(this.StartPoint.transform.position, this.ProgressSprite.transform.rotation);
    }

    private void UpdateProgressBar() {
        this.ProgressSprite.transform.Translate(Vector3.right * speedPerSec * Time.deltaTime);
    }

    void Update()
    {
        if (this.playing)
        {
            UpdateProgressBar(); 
        }
    }

    void LateUpdate()
    {
        if (SyncBeat.Instance.GetCurrentState() == SyncBeat.State.PLAYING) {
            this.playing = true;
        }
    }
}
