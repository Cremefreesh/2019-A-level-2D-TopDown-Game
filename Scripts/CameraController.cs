using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
 
    //private PlayerMovment player;
    public float camSpeed;
    public GameObject Player;
    private Vector3 playerPos;
   

    void Start () {
        //player = FindObjectOfType<PlayerMovment>();
	}
	
	
	void Update () {

        //gets the player's x, y but keeps the cam's z position
        playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, /*transform.position.z*/ -10);
        //moves the camera from it's current pos, towards the player position by moveSpeed each frame
        transform.position = Vector3.Lerp (transform.position, playerPos, camSpeed * Time.deltaTime); 
        //this.transform.position = playerPos;
    }
}
