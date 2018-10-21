using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableWall : MonoBehaviour {

    public string pianoKey;

    public Vector3 target;
    public float speed;
    
    bool hasMoved = false;
    bool isMoving = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving && !hasMoved)
        {


            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, target) < 0.1)
            {

                hasMoved = true;
            }
        }
	}

    public void moveWall()
    {

        isMoving = true;

    }
}
