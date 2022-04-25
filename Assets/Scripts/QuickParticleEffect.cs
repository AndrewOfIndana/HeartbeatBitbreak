using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickParticleEffect : MonoBehaviour
{
    public float expireTime;

    void Start()
    {
        Invoke("Expire", expireTime);
    }
    void Expire()
    {
        Destroy(this.gameObject);
    }
}
