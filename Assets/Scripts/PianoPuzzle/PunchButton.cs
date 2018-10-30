using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchButton : MonoBehaviour {

    public PaperPuncher _parent;


    public GameObject _leftHand;
    public GameObject _rightHand;


    private void OnTriggerEnter(Collider other)
    {


        if ((other.gameObject.name == _leftHand.name || other.gameObject.name == _rightHand.name))
        {


           // if (Gestures.IsPointing(_leftHand, _rightHand) != null)
            //{
               // Debug.Log("PUNCH BOI");
                pressButton();
           // }

        }
    }

    public void pressButton()
    {
        _parent.punchPaper();
        
    }
}
