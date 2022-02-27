using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTimingTest : MonoBehaviour
{

    public Conductor conductor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int hitVal = 0;

        if (Input.GetKeyDown("space")) {

            hitVal = conductor.CheckHitTiming();

            Debug.Log(hitVal);
        
        }
    }
}
