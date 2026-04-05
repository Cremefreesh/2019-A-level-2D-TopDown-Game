using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour {

    public bool bossOpen;
    public NpcQuestGiver checker;
    public GameObject gate;

	void Start () {

        bossOpen = false;
        checker = FindObjectOfType<NpcQuestGiver>();
	}
	
	
	void Update () {

        //if quest linking to boss is done,
        //open the boss room
        if (checker.bossopen)
        {
            gate.SetActive(true);
        }

	}
}
