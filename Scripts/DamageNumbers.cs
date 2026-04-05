using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbers : MonoBehaviour {

    //public float moveSpeed;
    //private Vector3 ePos;
    //public GameObject enemyTarget;

    public float dmgNum;
    public TextMeshProUGUI displayDmg;

    void Start () {

        //ePos = new Vector3(enemyTarget.transform.position.x, enemyTarget.transform.position.y + 1, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, ePos, moveSpeed * Time.deltaTime); 
    }
  
    public void Num()
    {
        displayDmg.text = " " + dmgNum; //update the text with the correct damage number
    }

}
