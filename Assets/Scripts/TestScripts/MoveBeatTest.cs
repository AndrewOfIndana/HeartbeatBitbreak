using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeatTest : MonoBehaviour
{
    [Header("Duration")]
    public float moveTime;
    
    [Header("Points")]
    public Transform StartPoint;
    public Transform Endpoint;

    [Header("Object")]
    public GameObject moveObject;

    private float distance;
    private float SpeedPerSec;
    void Start()
    {
        distance = Mathf.Abs(StartPoint.position.x - Endpoint.position.x);
        SpeedPerSec = distance / moveTime;

        moveObject.transform.SetPositionAndRotation(StartPoint.position, moveObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        moveObject.transform.Translate(Vector3.right * SpeedPerSec * Time.deltaTime);
    }
}
