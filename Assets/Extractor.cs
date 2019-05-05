using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour {

    
    public Transform openDoorPos;
    public Transform closedDoorPos;
    public Rigidbody door;
    public Material machineMaterial;
    bool isOn = false;

    public AudioClip powerOnSound;

    public AudioSource SFXSource;
    public AudioSource doorSource;


    public bool correctCombo = false;
    public float moveTime = 3f;

    public Combiner button;

    public Rigidbody smallDoor;
    public Transform smallDoorOpen;
    public Transform smallDoorClosed;

   // public AudioSource machinePowerup;
    //public AudioSource doorClicking;


	// Use this for initialization
	void Start () {
		machineMaterial.DisableKeyword("_EMISSION");
    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            powerOn();
        }
    }

   

    // Update is called once per frame
    public void powerOn()
    {
        SFXSource.clip = powerOnSound;
        SFXSource.Play();
        
        doorSource.Play();
        makeItGlow();
        openDoor();
        button.isActive = true;
        //machinePowerup.Play();
        
    
    }
    public void closeDoor()
    {
        StartCoroutine(Movement.SmoothMove(closedDoorPos.position, moveTime, door));
        doorSource.Play();
        //doorClicking.Play();
    }

    public void openDoor()
    {
        StartCoroutine(Movement.SmoothMove(openDoorPos.position, moveTime, door));
        StartCoroutine(Movement.SmoothRotate(openDoorPos.rotation, moveTime, door));

        StartCoroutine(Movement.SmoothMove(smallDoorOpen.position, moveTime, smallDoor));
        StartCoroutine(Movement.SmoothRotate(smallDoorOpen.rotation, moveTime, smallDoor));
        doorSource.Play();
        //doorClicking.Play();
    }

    
    public void makeItGlow()
    {
        machineMaterial.EnableKeyword("_EMISSION");
        isOn = true;
    }
}
