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

    [Header("Sprite")]
    public GameObject BeatTemplate;

    [Header("Beat Container")]
    public GameObject BeatHolder;

    private List<GameObject> BeatArray;

    //Misc Variables

    float distance;
    float distanceBetweenBeats;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBeatSprites();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
