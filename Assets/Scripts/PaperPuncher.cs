using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPuncher : MonoBehaviour {

    private bool[] _punchNodes;

    public PaperPuncherNode[] _nodeObjects;

    public PlayerPianoPaper _currentPaper;
    

    // punch nodes are numbered left to right, top to bottom
    //  | 0  1  2 |
    //
    //  | 3  4  5 |
    //
    //  | 6  7  8 |
    //

    // Use this for initialization
    void Start () {

        _punchNodes = new bool[] { false, false, false, false, false, false, false, false, false };
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setNode(0);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setNode(1);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setNode(2);

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            setNode(3);

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            setNode(4);

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            setNode(5);

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            setNode(6);

        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            setNode(7);

        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            setNode(8);

        }



        if(Input.GetKeyDown(KeyCode.Space))
        {


            punchPaper();
        }
    }

    public void punchPaper()
    {
        if (_currentPaper != null)
        {
            List<int> toPunch = new List<int>();
            toPunch.Clear();

            //note which nodes are to be punched
            for (int i = 0; i < _punchNodes.Length; i++)
            {
                if (_punchNodes[i])
                {
                    toPunch.Add(i);

                }

            }

            _currentPaper.punchMe(toPunch);

            //_currentPaper.gameObject.transform.position = new Vector3(_currentPaper.gameObject.transform.position.x + 1, _currentPaper.gameObject.transform.position.y, _currentPaper.gameObject.transform.position.z);
            //_currentPaper = null;
        }
    }

    public void insertPaper(PlayerPianoPaper paperToInsert)
    {
        
        
            _currentPaper = paperToInsert;
        

    }

    public void setNode(int index)
    {
        _punchNodes[index] = !_punchNodes[index];
        _nodeObjects[index].switchStatus();
        


    }
}
