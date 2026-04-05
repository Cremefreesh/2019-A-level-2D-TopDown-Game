using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneBool : MonoBehaviour {


    public bool returned;
    public GameObject start;

	void Start () {

        returned = false;

	}
	
	void Update () {
        if (returned)
        {
            start.SetActive(true);
        }


	}
}
