using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyStats : MonoBehaviour
{

    private XpController xp;
    public float EnemytotalHealth;
    public float EnemycurrentHealth;
    public float baseDamage;
    public float attackSpeed;
    public int xpWorth;
    private float difficulty;
    private DifficultyController D;                                                                         
    public float stunDur;
    public GameObject bloodBurst;
    private EnemyAI isTargetController;
    private HeroHealth damagePlayer;
    public float damageTimer;
    public float temp=.25f;
    private SoundEffects noise;
    public EntityStats luck;
    private NpcQuestGiver questObj;
    public int slimeCount;
    public GameObject damageNum;
    //public TextMeshProUGUI dmgText;
    private PlayerMovment player;

    void Start()
    {
        xp = FindObjectOfType<XpController>();
        D = FindObjectOfType<DifficultyController>();
                                                      

        questObj = FindObjectOfType<NpcQuestGiver>();
        luck = FindObjectOfType<EntityStats>();

        isTargetController = FindObjectOfType<EnemyAI>();
        //temp = damageTimer;

        noise = FindObjectOfType<SoundEffects>();
        difficulty = PlayerPrefs.GetFloat("DifficultySlider");
        //Debug.Log("Diff: " + difficulty); 
        //difficulty = diffSelector.value; 
        EnemytotalHealth *= (1 + (0.5f * difficulty));  //scale enemies to difficulty                
        EnemycurrentHealth = EnemytotalHealth;
        baseDamage *= (1 + (0.25f * difficulty));
        attackSpeed *= (1 + (0.15f * difficulty));                      
        damagePlayer = FindObjectOfType<HeroHealth>();
        player = FindObjectOfType<PlayerMovment>();
    }

    public void TakeDamage(float attackDamage, int strength)
    {
        stunDur = .1f; 
        //Instantiate(bloodBurst, transform.position, transform.rotation);
        GameObject blood = (GameObject)Instantiate(bloodBurst, transform.position, transform.rotation); //spawns blood
        Destroy(blood, .4f); //deletes clones

        if (luck.luck > 0)  //allows the player to crit                                                                 
        {
            int critChance = luck.luck * 5; //five percent chance to crit per level
            int random = Random.Range(0, 100);
            if (critChance > random)                                                       
            {
                EnemycurrentHealth -= 4*attackDamage * (1 + (0.05f * strength));

                //dmgText.text = "" + 4 * attackDamage * (1 + (0.05f * strength)); 
                //calculates and shows damage numbers 
                var damageNumber = (GameObject)Instantiate(damageNum, transform.position, transform.rotation);              
                damageNumber.GetComponent<DamageNumbers>().dmgNum = 4*attackDamage * (1 + (0.05f * strength));
                damageNumber.GetComponent<DamageNumbers>().Num();
                damageNumber.GetComponent<DamageNumbers>().displayDmg.color = Color.yellow;                                 
                Destroy(damageNumber, .3f);
                
                //Debug.Log("random: " + random);
                //Debug.Log("Crit:" + critChance);
                Debug.Log("Critdamage:" + 4 * attackDamage * (1 + (0.05f * strength)));   
            }
            else
            {
                EnemycurrentHealth -= attackDamage * (1 + (0.05f * strength));

                //dmgText.text = "" + attackDamage * (1 + (0.05f * strength)); 
                var damageNumber = (GameObject)Instantiate(damageNum, transform.position, transform.rotation);                  
                damageNumber.GetComponent<DamageNumbers>().dmgNum = attackDamage * (1 + (0.05f * strength));                    
                damageNumber.GetComponent<DamageNumbers>().Num();
                Destroy(damageNumber, .3f);

                Debug.Log("damage: " + attackDamage * (1 + (0.05f * strength)));
            }

        }                                                                                      
        else
        {
           EnemycurrentHealth -= attackDamage * (1 + (0.05f * strength));
            //dmgText.text = "" + attackDamage * (1 + (0.05f * strength));
            var damageNumber = (GameObject)Instantiate(damageNum, transform.position, transform.rotation); 
            damageNumber.GetComponent<DamageNumbers>().dmgNum = attackDamage * (1 + (0.05f * strength));
            damageNumber.GetComponent<DamageNumbers>().Num();
            //damageNumber.GetComponent<TextMeshProUGUI>().text = " " + attackDamage * (1 + (0.05f * strength)); 
            Destroy(damageNumber, .3f);
            //Debug.Log("damage: " + attackDamage * (1 + (0.05f * strength)));
        }
        noise.EnemyHurt.Play(); 
        Debug.Log("Damage Taken");
   
        if (EnemycurrentHealth <= 0)
            Dead();
    }


    void Update()
    {
        if(gameObject.name != "Boss" && gameObject.tag != "Enemy") //slimes get stunned for a short period after being hit
        {
            if (stunDur <= 0) 
            {
                //GetComponent<EnemyAI>().speed = 1.5f; 
                GetComponent<SlimeController>().speed = 1.5f;
            }
            if(stunDur > 0)
            {
                //GetComponent<EnemyAI>().speed = 0f; //stuns the enemy when hit by setting speed = 0 so movement script won't work
                GetComponent<SlimeController>().speed = 0f;
                stunDur -= Time.deltaTime;                                                                                                                              
            }
        }

    }

    void Dead()
    {
        noise.enemyDeath.Play();
        if(gameObject.name != "Boss")
        {
            isTargetController.isTarget = false;                                                                                                                       
        }
        Destroy(gameObject); //not working 26.12.25
        xp.XpGain(xpWorth);//grant player xp
                                                                                                                                                                        
        //if(gameObject.name != "Enemy")
        if(gameObject.tag == questObj.testing.quest.targetEnemy)                                                                                                        
        {               
            //add to an amount to quest
            questObj.testing.quest.currAmount += 1;                                                   
            //slimeCount += 1;                                                                                                                                          
        }
        if(gameObject.tag == "Enemy")
        {
            player.targets -= 1; //for music
        }
    }


    /*void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.name == "Player")
        {
            damagePlayer.TakeDamage(baseDamage); /version 1
        }
        */

    private void OnCollisionStay2D(Collision2D other)
    {
        if(gameObject.name != "Boss")
        {
            if (other.gameObject.name == "Player")
            {
                if(damageTimer > 0f) 
                {
                    damageTimer -= Time.deltaTime;
                }
                if(damageTimer <= 0f) //so player cannot take damage every update
                {
                    damagePlayer.TakeDamage(baseDamage);//deal player damage
                    damageTimer = temp;
                }
            }
        }

       
    }

   
}


        
