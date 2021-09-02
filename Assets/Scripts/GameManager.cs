using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;
	private BoardManager boardScript;

	private int level = 1;

	void Awake() {
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame() {
		boardScript.SetupScene(1);
	}

	public void NextLevel() {
		boardScript = GetComponent<BoardManager>();
		// enabled = false;

        foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
            if(o.tag != "MainCamera" && o.tag != "Player") Destroy(o);
        }

        // level doesn't reset to 1 between tests? hardcoded in start with then do 2
        // ++level;

		boardScript.SetupScene(2);
		
	}
}
