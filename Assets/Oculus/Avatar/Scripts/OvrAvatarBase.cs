using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OvrAvatarBase : MonoBehaviour
{


    public Material newMat;

    bool replaced = false;



    private void Update()
    {
        if (!replaced)
        {
            if (GetComponentInChildren<SkinnedMeshRenderer>() != null)
            {
                if (GetComponentInChildren<SkinnedMeshRenderer>().material.name != newMat.name)
                {
                    replaced = true;
                    SkinnedMeshRenderer smr = GetComponentInChildren<SkinnedMeshRenderer>();
                    smr.material = newMat;

                }
            }

        }
    }

}
