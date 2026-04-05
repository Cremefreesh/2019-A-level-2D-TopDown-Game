using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour {

    public int totalHealth;
    public float currentHealth;   
    public Slider healthBar;
    public EntityStats res;

    private SoundEffects noise;
    
    void Start () {
        currentHealth = totalHealth;
        healthBar.value = CalculateHealth();
        res = FindObjectOfType<EntityStats>();
        noise = FindObjectOfType<SoundEffects>();

    }

    public float CalculateHealth()//used to calculate the percentage health of player
    {
        return currentHealth / totalHealth;
    }

    public void displayHp()
    {
        healthBar.value = CalculateHealth();
    }


    public void TakeDamage(float attackDamage)//player take damage procedure
    {
        currentHealth -= attackDamage * (1 - (0.075f * res.resistance));//resistance lowers damage
        healthBar.value = CalculateHealth();

        if (currentHealth<= 0)
            Dead();
    }

     
    void Dead()
    {
        noise.death.Play();
        //reloads level and restores users stats using previous scripts
        Application.LoadLevel(Application.loadedLevel); 
    }
    
    
    void Update () {

        if (currentHealth > totalHealth)
        {
            //so if health regens, you can't have infinite hp 
            currentHealth = totalHealth;
            //healthBar.value = CalculateHealth();
        }

	}
}
