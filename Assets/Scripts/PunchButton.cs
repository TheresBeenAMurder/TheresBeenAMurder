using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchButton : MonoBehaviour {

    public PaperPuncher _parent;

	public void pressButton()
    {
        _parent.punchPaper();
        
    }
}
