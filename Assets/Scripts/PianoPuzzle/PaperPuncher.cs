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

            //_currentPaper.transform.localScale *= 20;
            _currentPaper.transform.parent = null;
            _currentPaper.transform.localScale = Vector3.one;
            _currentPaper.transform.position = (transform.position + new Vector3(1, 0, 0));
            _currentPaper.transform.rotation = new Quaternion(0, 0, 0, 0);
            //_currentPaper.gameObject.transform.position = new Vector3(_currentPaper.gameObject.transform.position.x + 1, _currentPaper.gameObject.transform.position.y, _currentPaper.gameObject.transform.position.z);
            //_currentPaper = null;
        }
    }

    public void insertPaper(PlayerPianoPaper paperToInsert)
    {
        
        
            _currentPaper = paperToInsert;
        paperToInsert.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        paperToInsert.transform.rotation = new Quaternion(0, 0, 0, 0);

    }

    public void setNode(int index)
    {
        _punchNodes[index] = !_punchNodes[index];
      //  _nodeObjects[index].switchStatus();
        


    }
}
