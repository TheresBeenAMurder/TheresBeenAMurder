using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperDropZone : MonoBehaviour {

    public PaperPuncher _parent;

    public GameObject _leftHand;
    public GameObject _rightHand;
    bool hasLetGo = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PunchPaper"))
        {

            //snap the boi
            other.gameObject.transform.parent = _parent.transform;
            other.transform.localPosition = Vector3.zero;
            other.transform.localScale *= .001f;
            //other.transform.position = _parent.transform.position;

            _parent._currentPaper = other.gameObject.GetComponent<PlayerPianoPaper>();
           

        }
        
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "PunchPaper")
    //    {
    //        other.gameObject.transform.parent = null;
   // _parent._currentPaper = null;
    //       // other.gameObject.transform.localScale = new Vector3(.108f, .105f, .004f);
    //    }
    //}
}
