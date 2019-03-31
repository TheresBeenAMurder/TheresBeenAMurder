using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

    public Material dissolveMat;
    public float level;
    public float dissolveMax;
    public float disSpeed;

    void Start()
    {
        dissolveMat.SetFloat("Vector1_8BFA949A", level);
    }

    private void Update()
    {
        if (level <= dissolveMax)
        {
            level += disSpeed;
            dissolveMat.SetFloat("Vector1_8BFA949A", level);
        }
        if (level >= dissolveMax)
        {
            FXEnable.disabled = false;
        }
    }
}
