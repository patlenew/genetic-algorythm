using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static GameController instance;
    public static GameController GetInstance() { return instance; }

	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
