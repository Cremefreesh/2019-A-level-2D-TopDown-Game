using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public Transform Target;
    public float speed;
    public float distance;
    public float attackRange;
    //public float timebetweenAttacks;
    public bool EnemyMoving;
    private SoundEffects swing;
    private Animator anim;
    public float timer;
    public Transform boxPos;
    public float boxSize;
    //public Transform boxSize;
    public LayerMask hero;
    public EnemyStats damage;
    private Vector2 Move;
    public float attackCooldown; //cooldown for attacks
    private Vector2 lastMove;
    public PlayerMovment pl;
    public int horiz, vert;

    void Start () {

        anim = GetComponent<Animator>();
        swing = FindObjectOfType<SoundEffects>();
        damage = FindObjectOfType<EnemyStats>();
        pl = FindObjectOfType<PlayerMovment>();
    }
	                                                                                                                        
                                                                                                                            
	
	void Update () {                            

        //2nd stage of boss fight
        /*
        if (damage.EnemycurrentHealth < damage.EnemytotalHealth*.3)
        {
            //enrage sound effect 
            //
            if(speed == 3)
            {
                swing.scream.Play();
                speed *= 1.5f;
                damage.baseDamage *= 2;
                //boss gets stronger and quicker near death 
            }
            //Not implemented due to lack of music and animations
        }
        */

       
        if (Vector3.Distance(transform.position, Target.position) < 50f)//always true in the boss arena
        {
            attackCooldown -= Time.deltaTime; //cooldown for if he can attack lowers
            
            distance = Vector3.Distance(Target.position, transform.position);//dist between player and boss
            transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime); //move towards player
            Move = new Vector2(transform.position.x, transform.position.y);
            //anim.SetFloat("enemyMoveX", Move.x);
            //anim.SetFloat("enemyMoveY", Move.y);
            if(Move.x > pl.transform.position.x) 
            {
                horiz = -1; //if boss to right then look left
            }
            else
            {
                horiz = 1;
            }
            if(Move.y > pl.transform.position.y)
            {
                vert = -1; //if boss above player look down
            }
            else
            {
                vert = 1;
            }

            //anim.SetFloat("enemyMoveX", horiz);                              
            //anim.SetFloat("enemyMoveY", vert);
            if(Move.x+1 > pl.transform.position.x && Move.x - 1 < pl.transform.position.x) //if their x values are nearly/are the same then only display up and down
            {                                                                                                                                                                   
                anim.SetFloat("enemyMoveY", vert);
                anim.SetFloat("enemyMoveX", 0);//only want to display up/down
                anim.SetFloat("enemyLastMoveX", 0);
                anim.SetFloat("enemyLastMoveY", vert);
            }
            else
            {
                anim.SetFloat("enemyMoveY", vert);
                anim.SetFloat("enemyMoveX", horiz);
                anim.SetFloat("enemyLastMoveX", horiz);
                anim.SetFloat("enemyLastMoveY", vert);
            }


            //lastMove = new Vector2(Move.x, Move.y);
            EnemyMoving = true;
            anim.SetBool("enemyMoving", EnemyMoving);
            if (attackRange >= distance)//if close enough to attack
            {
                
                //before was there was no cooldown, meaning that the boss just infinitely attacked dealing infinite damage
                if(attackCooldown <= 0)
                {
                    timer = .15f;
                    swing.Swing.Play();
                    attackCooldown = 1f;
                    anim.SetBool("enemyisAttacking", true);
                    //RB.velocity = Vector2.zero;
                    Collider2D[] HeroHit = Physics2D.OverlapCircleAll(boxPos.position, boxSize, hero); //same as in the hero attack controller but changed the layerMask to 'hero'
                    for (int x = 0; x < HeroHit.Length; x++)
                    {
                        HeroHit[x].GetComponent<HeroHealth>().TakeDamage(damage.baseDamage); 

                    }
                }

            }
            if (timer > 0) //cooldown for animation
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)//can attack again after this
            {
                anim.SetBool("enemyisAttacking", false); 
            }
        }
        //anim.SetFloat("enemyLastMoveX", horiz); //prioritzes horizontal lol - changed blend tree
        //anim.SetFloat("enemyLastMoveY", vert);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxPos.position, boxSize);
    }
}


