using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameras : MonoBehaviour
{
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchCamera(int cameraIndex) 
    {
        if(cameraIndex == 0) 
        {
            animator.Play("PartyShot");
        } 
        else if(cameraIndex == 1) 
        {
            animator.Play("Chara1");
        }
        else if(cameraIndex == 2)
        {
            animator.Play("Chara2");
        }
        else if(cameraIndex == 3)
        {
            animator.Play("Chara3");
        }
        else if(cameraIndex == 4)
        {
            animator.Play("Chara4");
        }
    }
}
