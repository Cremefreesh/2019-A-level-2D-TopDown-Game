
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    public Transform Target;
    public float speed;
    public bool isTarget;
    private Rigidbody2D erB;
    private Vector2 movement;
    public float timeLeft; 
    public float accelTime;

    private Vector2 lastMove;
    private bool EnemyMoving;
    private Animator eAnim; 
    private Vector2 Move;

    private Vector2 last;

    private PlayerMovment player; 
    private bool musicCount;

    //private bool moving;
    private float movingFor=1f;

    public int horiz, vert;

    void Start()
    {
        eAnim = GetComponent<Animator>();
        erB = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayerMovment>();
        musicCount = false;

        
    }

    void Update()
    {                                                                                                                         
        isTarget = false;
        
        EnemyMoving = false; 

        if (!isTarget)//if not following the player
        {
            //if(timer2 > 0)
            //{
                if (musicCount)
                {
                    player.targets -= 1; //for music
                    musicCount = false; //stops targets infinitely decreasing every update
                }
                

                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)//counts down time till next movement
                {
                    movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); //creates a random movement
                    EnemyMoving = true;
                    Velocity();
                
                    //moving = true;
                    timeLeft = /*accelTime*/ 5f;
                    //movingFor -= Time.deltaTime;
                }

            /*  if(timer2 <= 0)
              {
                  moving = false;
                  timer2 = 1f;
              }
              timeLeft -= Time.deltaTime;
              timer2 -= Time.deltaTime;
              
              if(movingFor <= 0)
              {
                 movement = Vector2.zero;
                 timeLeft = accelTime;
              }
              */

              if(accelTime > timeLeft)
              {
                movement = Vector2.zero;
                EnemyMoving = false;
                Velocity();
              }
        }
        

            //}

       if (Vector3.Distance(transform.position, Target.position) < 6f) //following range
        {
            isTarget = true;
            transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
            //lastMove = new Vector2(transform.position.x, transform.position.y);
            Move = new Vector2(transform.position.x, transform.position.y);
            EnemyMoving = true;
            eAnim.SetBool("enemyMoving", EnemyMoving);
            //eAnim.SetFloat("enemyMoveX", -Input.GetAxisRaw("Horizontal"));                                                  
            //eAnim.SetFloat("enemyMoveY", -Input.GetAxisRaw("Vertical"));

            if (Move.x > player.transform.position.x)//if to right of player
            {
                horiz = -1; //if to right, look left
            }
            else
            {
                horiz = 1; //else look right
            }
            if (Move.y > player.transform.position.y) //if above player in 2D
            {
                vert = -1; //if above, look down
            }
            else
            {
                vert = 1; //else, look up
            }


            if (Move.x + 1 > player.transform.position.x && Move.x - 1 < player.transform.position.x) 
            {
                eAnim.SetFloat("enemyMoveY", vert);
                eAnim.SetFloat("enemyMoveX", 0);//only want up/down anims
                eAnim.SetFloat("enemyLastMoveX", 0);
                eAnim.SetFloat("enemyLastMoveY", vert);
            }
            else
            {
                eAnim.SetFloat("enemyMoveY", vert);
                eAnim.SetFloat("enemyMoveX", horiz);
                eAnim.SetFloat("enemyLastMoveX", horiz);
                eAnim.SetFloat("enemyLastMoveY", vert);
            }


            if (!musicCount)
            {
                player.targets += 1; //for music   
                musicCount = true;//stops targets infinitely incrementing every update
            }
                                                                                                                        
        }

        //eAnim.SetFloat("enemyMoveX", Move.x);
        //eAnim.SetFloat("enemyMoveY", Move.y);                                                                            

        //eAnim.SetFloat("enemyMoveX", Input.GetAxisRaw("Horizontal")); 
        //eAnim.SetFloat("enemyMoveY", Input.GetAxisRaw("Vertical"));
        //eAnim.SetFloat("enemyLastMoveX", lastMove.x);                                                                    
        //eAnim.SetFloat("enemyLastMoveY", lastMove.y);
                                                                                                                            

    }

    

    void Velocity() //used for automatic movement
    {
        //last = new Vector2(movement.x, movement.y);
        erB.velocity = movement;
        //lastMove = new Vector2(movement.x,movement.y);
        Move = new Vector2(movement.x, movement.y);
        //EnemyMoving = true;
        if (EnemyMoving)
        {
            //if enemy moving then we want to look that direction
            last = new Vector2(movement.x, movement.y); 
            eAnim.SetBool("enemyMoving", EnemyMoving);
            eAnim.SetFloat("enemyMoveX", Move.x);
            eAnim.SetFloat("enemyMoveY", Move.y);
        }
        else
        {
            //if not moving look previous direction
            eAnim.SetBool("enemyMoving", EnemyMoving);
            eAnim.SetFloat("enemyLastMoveX", last.x);                                                                    
            eAnim.SetFloat("enemyLastMoveY", last.y);
        }


    }



}
