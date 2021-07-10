using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;
	public BoardManager boardScript;

	private int level = 2;

	void Awake() {
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame() {
		boardScript.SetupScene(2);
	}

	public void GamerOver() {
		enabled = false;
	}
}
