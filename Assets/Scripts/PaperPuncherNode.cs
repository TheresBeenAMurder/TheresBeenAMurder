using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaperPuncherNode : MonoBehaviour {

    public PaperPuncher _parent;
    public int _index;
    

    bool isOn = false;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void turnOn()
    {
        _parent.setNode(_index, true);
        isOn = true;
    }

    void turnOff()
    {
        _parent.setNode(_index, false);
        isOn = false;
    }

    void switchStatus()
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
