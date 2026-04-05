using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour {


    public AudioSource bossMusic;
    private BossScript MusicChange;
    public AudioSource nearDeath;
    public bool neardeath;

    // Use this for initialization
    void Start () {
        MusicChange = FindObjectOfType<BossScript>();
        bossMusic.Play();
        neardeath = false;
    }

	
	// Update is called once per frame
	void Update () {

        nearDeath.volume = PlayerPrefs.GetFloat("VolumeSlider");
        bossMusic.volume = PlayerPrefs.GetFloat("VolumeSlider");

        if(MusicChange.speed > 3 && !neardeath)//if boss is nearly dead other music can play
        {
            bossMusic.Stop();
            nearDeath.Play();
            neardeath = true; //stops music endlessly starting
        }

        if(!GameObject.Find("Boss"))
        {
            nearDeath.Stop();  
        }

    }
}
