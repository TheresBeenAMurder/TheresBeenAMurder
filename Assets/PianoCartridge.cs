using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoCartridge : SnapGrabbable {


    public CartridgeSlot slotA;
    public CartridgeSlot slotB;
    public CartridgeSlot slotC;

    public CartridgeDisc[] discs;



    public void addDisc(CartridgeDisc toAdd, string slotName)
    {
        if(discs.Length == 0)
        {

            discs = new CartridgeDisc[] { null, null, null };
        }
        
            if (slotName == slotA.name)
            {
                discs[0] = toAdd;
            }
            if (slotName == slotB.name)
            {
                discs[1] = toAdd;
            }
            if (slotName == slotC.name)
            {
                discs[2] = toAdd;
            }
        

    }

    public void removeDisc(string slotName)
    {
        if(slotName == slotA.name)
        {
            discs[0] = null;
        }
        if(slotName == slotB.name)
        {
            discs[1] = null;
        }
        if(slotName == slotC.name)
        {
            discs[2] = null;
        }
    }
}
