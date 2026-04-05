using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    public DashState dashState;
    public float dashTimer; //dash cooldown
    private Rigidbody2D rigidbody;

    private int direc;
    private int HV;
    //PlayerController Player;
    //public float dashDistance = 10;
    public float dashSpeed = 5;
    //public Vector3 moveDirection;

    public Vector2 savedVelocity;
    public float timer=0.1f; 
    public bool dashing;                                                                                                

    private SoundEffects noise;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //Player = GetComponent<PlayerController>();
        savedVelocity = rigidbody.velocity;
        noise = FindObjectOfType<SoundEffects>();
    }

    void Update()
    {
        switch (dashState)
        {
            case DashState.Ready:
                dashing = false;
                dashTimer = .75f;//reset dashTimer,timer and dashing
                timer = 0.3f;
                savedVelocity = rigidbody.velocity;//save current (regular) velocity
                var isDashKey = Input.GetKeyDown(KeyCode.LeftShift);
                if (isDashKey)
                {
                                                                                                        
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))//player wants to dash left
                    {
                        //rigidbody.velocity = Vector2.left * dashSpeed * Time.deltaTime; 
                        direc = -1;
                        HV = 1;
                    }
                    else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))//right
                    {
                        //rigidbody.velocity = Vector2.right * dashSpeed * Time.deltaTime;
                        direc = 1;
                        HV = 1;
                    }
                    else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))//down
                    {
                        //rigidbody.velocity = Vector2.down * dashSpeed * Time.deltaTime;
                        direc = -1;
                        HV = 0;
                    }
                    else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))//up
                    {
                        //rigidbody.velocity = Vector2.up * dashSpeed * Time.deltaTime;
                        direc = 1;
                        HV = 0; 
                    }
                                                                                                       
                                                                                                        
                    dashing = true; 
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                if(dashTimer > 0)
                {//timer is the time spent dashing whereas dashTimer is the cooldown for dashing
                    if(timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    if(timer <= 0)
                    {
                        dashing = false;                                                                
                        rigidbody.velocity = savedVelocity;                                            
                    }
                    dashTimer -= Time.deltaTime;
                }
                if (dashTimer <= 0)
                {
                    rigidbody.velocity = savedVelocity;//change velocity back to normal
                    dashState = DashState.Ready;                                                            
                }                                                                                              
                break;                                                                                          
        }
    }

    void FixedUpdate()
    {
        //if dashing
        if(/*dashTimer < 1*/ dashing)                                                                   
        {
            if (HV > 0)//horizontal dash
            {
                rigidbody.velocity = new Vector2(direc * dashSpeed, rigidbody.velocity.y);
                noise.dash.Play();
                                                                                                        
            }
            if(HV <= 0)//vertical dash
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x,direc*dashSpeed);
                noise.dash.Play();                                                                      
            }
        
        }
        
    }

    
}
                                                                                                        
                                                                                                        
public enum DashState
{
    Ready,
    Cooldown
}


