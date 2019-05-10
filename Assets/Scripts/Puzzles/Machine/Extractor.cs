using System.Collections;
using UnityEngine;

public class Extractor : MonoBehaviour
{ 
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

    public AudioSource extractorVox;

    public AudioClip powerUpLine;
    public GameObject[] insideObjs;

	void Start ()
    {
		machineMaterial.DisableKeyword("_EMISSION");

        foreach (GameObject obj in insideObjs)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            powerOn();
        }
    }

    public IEnumerator powerOn()
    {
        SFXSource.clip = powerOnSound;
        SFXSource.Play();

        yield return new WaitForSeconds(SFXSource.clip.length);

        extractorVox.clip = powerUpLine;
        extractorVox.Play();

        yield return new WaitForSeconds(extractorVox.clip.length);

        doorSource.Play();

        yield return new WaitForSeconds(doorSource.clip.length);

        foreach (GameObject obj in insideObjs)
        {
            obj.SetActive(true);
        }

        makeItGlow();
        openDoor();
        button.isActive = true;
    }

    public void closeDoor()
    {
        StartCoroutine(Movement.SmoothMove(closedDoorPos.position, moveTime, door));
        doorSource.Play();
    }

    public void openDoor()
    {
        StartCoroutine(Movement.SmoothMove(openDoorPos.position, moveTime, door));
        StartCoroutine(Movement.SmoothRotate(openDoorPos.rotation, moveTime, door));

        StartCoroutine(Movement.SmoothMove(smallDoorOpen.position, moveTime, smallDoor));
        StartCoroutine(Movement.SmoothRotate(smallDoorOpen.rotation, moveTime, smallDoor));
        doorSource.Play();
    }
    
    public void makeItGlow()
    {
        machineMaterial.EnableKeyword("_EMISSION");
        isOn = true;
    }
}
