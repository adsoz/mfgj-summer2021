using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {

	public GameManager gameManager;
    
    void Awake() {
    	gameManager.RestartLevel();
    }


}
