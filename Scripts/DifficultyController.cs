using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyController : MonoBehaviour {


    public Slider difficultySlider;
    public float difficulty;

	void Start () {
        difficultySlider.value = PlayerPrefs.GetFloat("DifficultySlider");
        //difficulty = difficultySlider.value;
        //Debug.Log("Difficulty: " + difficulty);

    }
	
	
	void Update () {
            //difficulty = difficultySlider.value; //used for testing
            //Debug.Log("Difficulty: " + difficulty);
            PlayerPrefs.SetFloat("DifficultySlider", difficultySlider.value); //stores difficulty when the game is turned off 
    }
}
