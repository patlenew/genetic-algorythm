using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public bool isStart;
    public bool isGoal;

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
