using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPianoPaper : MonoBehaviour {

    public bool[] _nodeStatus = { false, false, false, false, false, false, false, false, false };

    public Material _thisPaperMat;

    // punch nodes are numbered left to right, top to bottom
    //  | 0  1  2 |
    //
    //  | 3  4  5 |
    //
    //  | 6  7  8 |
    //

	// Use this for initialization
	void Start () {
		
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            string[] _punchBools = new string[] { "_Punch0", "_Punch1", "_Punch2", "_Punch3", "_Punch4", "_Punch5", "_Punch6", "_Punch7", "_Punch8" };
            //reset
            for (int i = 0; i< _nodeStatus.Length; i++)
            {
                _nodeStatus[i] = false;

                _thisPaperMat.SetInt(_punchBools[i], 0);
            }
        }
    }

    public void punchMe(List<int> nodesToPunch)
    {
        //reset. not reflective of real world but we can take this out literally whenever
    
        _nodeStatus =  new bool[] { false, false, false, false, false, false, false, false, false };

        string[] _punchBools = new string[] { "_Punch0", "_Punch1", "_Punch2", "_Punch3", "_Punch4", "_Punch5", "_Punch6", "_Punch7", "_Punch8" };

        foreach (string s in _punchBools)
        {
            _thisPaperMat.SetInt(s, 0);
        }

        //mark each hole to punch as punched
        foreach (int punchHole in nodesToPunch)
        {
            _nodeStatus[punchHole] = true;
            _thisPaperMat.SetInt(_punchBools[punchHole], 1);

        }

        //put code here for swapping out materials


    }

    public void activePunch(int i)
    {


        string[] _punchBools = new string[] { "_Punch0", "_Punch1", "_Punch2", "_Punch3", "_Punch4", "_Punch5", "_Punch6", "_Punch7", "_Punch8" };

        if (_nodeStatus[i])
        {
            _thisPaperMat.SetInt(_punchBools[i], 0);

        }
        else
        {
            _thisPaperMat.SetInt(_punchBools[i], 1);

        }

        _nodeStatus[i] = !_nodeStatus[i];

    }
}
