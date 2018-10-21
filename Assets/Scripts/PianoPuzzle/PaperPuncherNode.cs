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

	// Use this for initialization
	void Start () {
        _mr = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
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
    }
}
