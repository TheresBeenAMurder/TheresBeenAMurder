using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public Transform centerPoint;
    public GameObject ellis;
    public Transform ellisStop;
    public float flyTime;
    public NPCAnimator[] npcs;
    public NPCAnimator[] npcDoubles;
    public GameObject sun;
    public float waitDanceTime;
    public float waitRotateTime;

    private float rotateSpeed = 20;

    private IEnumerator Dance(float wait, NPCAnimator[] npcs)
    {        
        yield return new WaitForSeconds(wait);

        foreach (NPCAnimator npc in npcs)
        {
            npc.changeState(NPCAnimator.CHARACTERSTATE.DANCE);
        }
    }

    private IEnumerator EllisFly()
    {
        Rigidbody ellisRb = ellis.GetComponent<Rigidbody>();
        yield return StartCoroutine(Movement.SmoothMove(ellisStop.position, flyTime, ellisRb));
        yield return StartCoroutine(Movement.SmoothRotate(Vector3.left, 90f, 1.8f, ellisRb));
        ellis.GetComponent<Animator>().SetInteger("state", (int)NPCAnimator.CHARACTERSTATE.DANCE);
    }

    private IEnumerator FadeIn(GameObject obj)
    {
        float fadeAmount = 0.001f;
        Renderer renderer = obj.GetComponent<Renderer>();
        renderer.enabled = true;
        Material material = renderer.material;
        Color color = material.color;
        color.a = 0f;

        while (fadeAmount <= 1f)
        {
            fadeAmount += (0.002f * Time.deltaTime);
            material.color = new Color(color.r, color.g, color.b, fadeAmount);
            yield return null;
        }
    }

    private IEnumerator Rotate(float wait)
    {
        yield return new WaitForSeconds(wait);
        enabled = true;

        yield return new WaitForSeconds(50 - wait);
        for (int i = 0; i < 18; i++)
        {
            rotateSpeed += (i * 2);
            yield return new WaitForSeconds(.975f);
        }
        enabled = false;

        yield return StartCoroutine(EllisFly());

        rotateSpeed = 20f;
        yield return new WaitForSeconds(14);
        enabled = true;

        yield return new WaitForSeconds(12);
        for (int i = 0; i < 18; i++)
        {
            rotateSpeed += (i * 2);
            yield return new WaitForSeconds(.85f);
        }
        enabled = false;
        ToggleDoubles();
        StartCoroutine(Dance(0f, npcDoubles));
        //StartCoroutine(FadeIn(sun));
    }

    private void Start()
    {
        enabled = false;

        ToggleDoubles();
        //sun.GetComponent<Renderer>().enabled = false;
        
        StartCoroutine(Dance(waitDanceTime, npcs));
        StartCoroutine(Rotate(waitRotateTime));
    }

    private void ToggleDoubles()
    {
        foreach (NPCAnimator npc in npcDoubles)
        {
            npc.gameObject.SetActive(!npc.gameObject.activeSelf);
        }
    }

    private void Update()
    {
        foreach (NPCAnimator npc in npcs)
        {
            npc.gameObject.transform.RotateAround(centerPoint.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
