using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaperPuncherNode : MonoBehaviour {

    public PaperPuncher _parent;
    public int _index;

    public Material _offMat;
    public Material _onMat;

    public MeshRenderer _mr;

    bool isOn = false;

    public GameObject _leftHand;
    public GameObject _rightHand;


	// Use this for initialization
	void Start () {
        _mr = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        

         if((other.gameObject.name == _leftHand.name || other.gameObject.name == _rightHand.name))
         {
        

       // if (Gestures.IsPointing(_leftHand, _rightHand) != null)
       // {

            switchStatus();
       // }

        }
    }

    void turnOn()
    {
       // _parent.setNode(_index);
        _mr.material = _onMat;
        isOn = true;
    }

    void turnOff()
    {
        //_parent.setNode(_index);
        _mr.material = _offMat;
        isOn = false;
    }

    public void switchStatus()
    {
        if(isOn)
        {
            turnOff();
        }
        else
        {
            turnOn();

        }

        _parent.setNode(_index);
    }
}
