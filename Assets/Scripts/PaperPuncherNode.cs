using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PaperPuncherNode : MonoBehaviour {

    public PaperPuncher _parent;
    public int _index;

    private VRTK_InteractableObject _interactableObject;

    bool isOn = false;

	// Use this for initialization
	void Start () {
        _interactableObject = GetComponent<VRTK_InteractableObject>();
        _interactableObject.InteractableObjectUsed += new InteractableObjectEventHandler(switchStatus);

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

    void switchStatus(object sender, InteractableObjectEventArgs e)
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
