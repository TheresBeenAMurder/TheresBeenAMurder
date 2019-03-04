using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class NPCAnimator : MonoBehaviour {

    public Animator animator;

    //idle = 0
    //left = 1
    //right = 2
    //forward = 3
    //backward = 4
    //talk normal = 5
    //talk agitated = 6
    //dance = 7

    public enum CHARACTERSTATE { IDLE, WALKLEFT, WALKRIGHT, WALKFORWARD, WALKBACKWARD, TALKNORMAL, TALKAGITATED, DANCE };
    public CHARACTERSTATE currentState;

	// Use this for initialization
	void Start () {
        currentState = CHARACTERSTATE.TALKNORMAL;
	}
	
    public void changeState(CHARACTERSTATE newState)
    {
        currentState = newState;

        switch(newState)
        {
            case CHARACTERSTATE.IDLE:
                {
                    animator.SetInteger("state", 0);
                    break;
                }
            case CHARACTERSTATE.WALKLEFT:
                {

                    animator.SetInteger("state", 1);
                    break;
                }
            case CHARACTERSTATE.WALKRIGHT:
                {

                    animator.SetInteger("state", 2);
                    break;
                }
            case CHARACTERSTATE.WALKFORWARD:
                {

                    animator.SetInteger("state", 3);
                    break;
                }
            case CHARACTERSTATE.WALKBACKWARD:
                {

                    animator.SetInteger("state", 4);
                    break;
                }
            case CHARACTERSTATE.TALKNORMAL:
                {

                    animator.SetInteger("state", 5);
                    break;
                }
            case CHARACTERSTATE.TALKAGITATED:
                {

                    animator.SetInteger("state", 6);
                    break;
                }
            case CHARACTERSTATE.DANCE:
                {

                    animator.SetInteger("state", 7);
                    break;
                }




        }


    }
}
