using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelGrabbable : SnapGrabbable
{

    public AudioSource crunchSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        if(isInDropZone)
        {
            crunchSound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
