public class PianoCartridge : SnapGrabbable
{
    public CartridgeSlot slotA;
    public CartridgeSlot slotB;
    public CartridgeSlot slotC;

    public CartridgeDisc[] discs;

    public void addDisc(CartridgeDisc toAdd, string slotName)
    {
        if (slotName.Equals(slotA.name))
        {
            if (slotB.child != toAdd && slotC.child != toAdd)
            {
                discs[0] = toAdd;
            }
        }
        if (slotName.Equals(slotB.name))
        {
            if (slotA.child != toAdd && slotC.child != toAdd)
            {

                discs[1] = toAdd;
            }
        }
        if (slotName.Equals(slotC.name))
        {
            if (slotB.child != toAdd && slotA.child != toAdd)
            {

                discs[2] = toAdd;
            }
        }
    }

    public void removeDisc(string slotName)
    {
        if(slotName.Equals(slotA.name))
        {
            discs[0] = null;
        }
        if(slotName.Equals(slotB.name))
        {
            discs[1] = null;
        }
        if(slotName.Equals(slotC.name))
        {
            discs[2] = null;
        }
    }
}
