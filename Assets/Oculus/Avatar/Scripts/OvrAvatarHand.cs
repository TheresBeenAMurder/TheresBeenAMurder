using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OvrAvatarHand : MonoBehaviour
{

    public Material handMat;

    bool replaced = false;

  

    private void Update()
    {
        if (!replaced)
        {
            if (GetComponentInChildren<SkinnedMeshRenderer>() != null)
            {
                if (GetComponentInChildren<SkinnedMeshRenderer>().material.name != handMat.name)
                {
                    replaced = true;
                    GetComponentInChildren<SkinnedMeshRenderer>().material = handMat;

                }
            }

        }
    }
}
