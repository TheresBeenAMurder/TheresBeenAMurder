public class PianoCartridge : SnapGrabbable
{
    public CartridgeSlot slotA;
    public CartridgeSlot slotB;
    public CartridgeSlot slotC;

    public CartridgeDisc[] discs = new CartridgeDisc[3];

    public void addDisc(CartridgeDisc toAdd, string slotName)
    {   
        if (slotName.Equals(slotA.name))
        {
            discs[0] = toAdd;
        }
        if (slotName.Equals(slotB.name))
        {
            discs[1] = toAdd;
        }
        if (slotName.Equals(slotC.name))
        {
            discs[2] = toAdd;
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
