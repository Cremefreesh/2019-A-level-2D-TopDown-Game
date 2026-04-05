using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public TextMeshProUGUI text;
    public GameObject dBox;
    public string[] dialogue;
    public float displaySpeed;
    public bool isActive;
    public string[] postQuest;
    public int currLine;
    public SoundEffects click;
    public bool check;
    private NpcQuestGiver npc;

	void Start () {
        npc = FindObjectOfType<NpcQuestGiver>();
        click = FindObjectOfType<SoundEffects>();
        check = false;
    }
	
	
	void Update () {

        //if NPC's quest not finished yet
      if (!npc.dialogueCheck)                                                                               
        {
        if (isActive && Input.GetKeyDown(KeyCode.Return))
        {
            
            //play a sound //like a pokemon click etc
            click.dialogueClick.Play();
            currLine += 1;
            text.text = dialogue[currLine];
        }
        if(currLine >= dialogue.Length)//once get to the end of dialogue                                                                         
        {
            check = true;
            dBox.SetActive(false);
            isActive = false;//deactivate game objects
            //check = true;                                                                                  
            currLine = 0;
            Time.timeScale = 1.0f;
        }

        //text.text = dialogue[currLine];
        }
        
        else//if beaten quest there is new dialogue
        {
            if (isActive && Input.GetKeyDown(KeyCode.Return))
            {
                //play a sound //like a pokemon click etc
                click.dialogueClick.Play();
                currLine += 1;

            }
            if (currLine >= postQuest.Length) //otherwise ends postQuest at length of dialogue.length
            {
                check = true;
                dBox.SetActive(false);
                isActive = false;
               
                currLine = 0;
                Time.timeScale = 1.0f;
            }


            text.text = "";//resets text and plays new dialogue
            text.text = postQuest[currLine];
        }

    }

    
    
    void OnCollisionEnter2D (Collision2D other) 
    {
        
        if (other.gameObject.name == "Player")
        {
            //if (Input.GetKey(KeyCode.Return))
            //{
                if (!isActive)//if dialogue not active
                {
                    //restart and play dialogue
                    currLine = 0;
                    isActive = true;
                    dBox.SetActive(true);
                    text.text = dialogue[currLine];
                    //makes it so nothing can move 
                    Time.timeScale = 0.0f; 
                }
                
            //}
        }
    }
    

}
