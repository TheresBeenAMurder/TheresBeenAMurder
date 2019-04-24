using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class NPCAnimator : MonoBehaviour {

    public Animator animator;
    public AudioSource footsteps;

    //idle = 0
    //left = 1
    //right = 2
    //forward = 3
    //backward = 4
    //talk normal = 5
    //talk agitated = 6
    //dance = 7

    public enum CHARACTERSTATE { IDLE, WALKLEFT, WALKRIGHT, WALKFORWARD, WALKBACKWARD, TALKNORMAL, TALKAGITATED, DANCE, HAND };
    public CHARACTERSTATE currentState;

	// Use this for initialization
	void Start () {
        currentState = CHARACTERSTATE.TALKNORMAL;
        animator.SetInteger("state", 5);
    }
	
    public void changeState(CHARACTERSTATE newState)
    {
        currentState = newState;

        switch(newState)
        {
            case CHARACTERSTATE.IDLE:
                {
                    if(footsteps.isPlaying)
                    {
                        footsteps.Stop();
                    }
                    animator.SetInteger("state", 0);
                    break;
                }
            case CHARACTERSTATE.WALKLEFT:
                {
                    if (footsteps.isPlaying)
                    {
                        footsteps.Stop();
                    }
                    animator.SetInteger("state", 1);
                    break;
                }
            case CHARACTERSTATE.WALKRIGHT:
                {
                    if (footsteps.isPlaying)
                    {
                        footsteps.Stop();
                    }
                    animator.SetInteger("state", 2);
                    break;
                }
            case CHARACTERSTATE.WALKFORWARD:
                {
                    footsteps.Play();
                    animator.SetInteger("state", 3);
                    break;
                }
            case CHARACTERSTATE.WALKBACKWARD:
                {
                    if (footsteps.isPlaying)
                    {
                        footsteps.Stop();
                    }
                    animator.SetInteger("state", 4);
                    break;
                }
            case CHARACTERSTATE.TALKNORMAL:
                {
                    if (footsteps.isPlaying)
                    {
                        footsteps.Stop();
                    }
                    animator.SetInteger("state", 5);
                    break;
                }
            case CHARACTERSTATE.TALKAGITATED:
                {
                    if (footsteps.isPlaying)
                    {
                        footsteps.Stop();
                    }
                    animator.SetInteger("state", 6);
                    break;
                }
            case CHARACTERSTATE.DANCE:
                {
                    if (footsteps.isPlaying)
                    {
                        footsteps.Stop();
                    }
                    animator.SetInteger("state", 7);
                    break;
                }
            case CHARACTERSTATE.HAND:
                {
                    if (footsteps.isPlaying)
                    {
                        footsteps.Stop();
                    }
                    animator.SetInteger("state", 8);
                    break;
                }




        }


    }
}
