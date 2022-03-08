using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeCreationTest : MonoBehaviour
{
    public GameObject child;
    public float moveamount = 10;
    // Start is called before the first frame update
    void Start()
    {
        GameObject child2 = GameObject.Instantiate(child);
        child2.transform.parent = gameObject.transform;
        child2.transform.Translate(Vector3.right * moveamount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
