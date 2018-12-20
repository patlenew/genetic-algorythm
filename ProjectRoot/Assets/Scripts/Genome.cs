using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome : MonoBehaviour {

    public enum MoveDirection { LEFT, RIGHT, JUMP}
    public MoveDirection currentDir;
    private Vector3 dir;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, Vector3.right,Color.white);
        MoveInputs();
    }

    void MoveInputs() {
        dir = Vector3.zero;

        if (currentDir == MoveDirection.LEFT) {
            dir = new Vector3(-1f,0f,0f);
        }
        else if (currentDir == MoveDirection.RIGHT) {
            dir = new Vector3(1f,0f,0f);
        }
        else if (currentDir == MoveDirection.JUMP) {
            dir = new Vector3(0f,1f,0f);
        }

        transform.position += dir;
    }

    void Jump() {

    }


    //le fitness rate dans ce cas ci
    //serait le plus proche que le character peut arrivé du goal
    public void FitnessRate()
    {

    }
}
