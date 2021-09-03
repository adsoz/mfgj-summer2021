using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardScript;
	// private Text levelText;
	public GameObject levelImage1;
	public GameObject levelImage2;
	public GameObject levelImage3;
	public GameObject levelImage4;

	private int level = 1;
	public float levelStartDelay = 2f;
	private bool doingSetup;


	void Awake() {
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame() {
		doingSetup = true;
		level = 1;

		levelImage1 = GameObject.Find("LevelImage1");
		// levelImage2 = GameObject.Find("LevelImage2");
		// levelImage3 = GameObject.Find("LevelImage3");
		// levelImage4 = GameObject.Find("LevelImage4");

		// transition card
		// levelImage = GameObject.Find("LevelImage1");
		// levelText = GameObject.Find("LevelText1").GetComponent<Text>();
		// levelText.text = "Walk towards the light...";
		levelImage1.SetActive(true);
		Invoke("HideLevelImage", levelStartDelay);

		boardScript.SetupScene(level);
	}

//Hides black image used between levels
	void HideLevelImage() {
			
		levelImage1.SetActive(false);
		levelImage2.SetActive(false);
		levelImage3.SetActive(false);
		levelImage4.SetActive(false);
		
		//Set doingSetup to false allowing player to move again.
		doingSetup = false;
	}

	public void NextLevel() {
		boardScript = GetComponent<BoardManager>();

        // transition card
        doingSetup = true;


		// levelImage = GameObject.Find("LevelImage2");

		// if (levelImage == null) {
		// 	Debug.Log("uh oh! it's null o'clock!");
		// }
		//levelText = GameObject.Find("LevelText").GetComponent<Text>();
		// levelText.text = "At long last, freedom.\n\n\n ... unless...?";

		/*if (level == 2) {
			levelText.text = "At long last, freedom.\n\n\n ... unless...?";
		} else {
			levelText.text = "the end :)";
		}
*/		
		level = 2;

		levelImage2.SetActive(true);

		//Invoke("HideLevelImage", levelStartDelay);

		Debug.Log("next level is " + level);
		Debug.Log("GKJDSKGJDSKFLDSJFLFDJ " + levelImage2.activeSelf + " " + levelImage2.activeInHierarchy);

		foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
            if (o.tag != "MainCamera" && o.tag != "Player" && o.tag != "UI") Destroy(o);
        }

		boardScript.SetupScene(level);
		
	}

	public void RestartLevel() {
		boardScript = GetComponent<BoardManager>();

	    foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
	        if (o.tag != "MainCamera" && o.tag != "Player" && o.tag != "UI") Destroy(o);
	    }

        // transition card
        doingSetup = true;
		levelImage4.SetActive(true);
		Invoke("HideLevelImage", levelStartDelay);

		boardScript.SetupScene(level);
	
	}
}
