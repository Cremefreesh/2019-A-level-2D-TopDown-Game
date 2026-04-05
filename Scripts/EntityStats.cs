using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour {


    public int strength; //each point increases damage by 5%
    public int dexterity; //increases attackSpeed by 5%            
    public int resistance; //increases damage resist by 3.5%
    public int luck; //increases crit chance by 4% and dodge chance by 3%
    //public int faith; //increases health regen outside combat by 1% per second and .5% inside combat per level


    void Start()
    {
        //strength = 0; 
        //dexterity = 0;
        //resistance = 0; 
        //luck = 0;
        //faith = 0;


    }

}