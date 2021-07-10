using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

	public float restartLevelDelay = 1f;

	private Animator animator;

	public Vector2 playerDir;

    // Start is called before the first frame update
    protected override void Start() {
    	animator = GetComponent<Animator>();
    	base.Start();

    	// start position

        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        transform.position = spawnPoint.transform.position;
        // transform.position = new Vector3(4,12,0);
        
    }

    // Update is called once per frame
    private void Update() {
		if (!isMoving) {
    		playerDir.x = (Input.GetAxisRaw("Horizontal"));
    		playerDir.y = (Input.GetAxisRaw("Vertical"));
		}

    	if (playerDir.x!=0) playerDir.y = 0;
    	
    	if (playerDir.x!=0 || playerDir.y!=0) {
			Debug.Log(playerDir);
    		if (!(Move(playerDir))) {
				RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDir);
				OnCantMove(hit.collider, (int) playerDir.x, (int) playerDir.y);
			}
    	}
        
    }

    private void OnTriggerEnter2D (Collider2D other) {
    	if (other.tag == "Exit") {
    		Invoke("Restart", restartLevelDelay);
    		enabled = false;
    	}
    }

    protected override void OnCantMove <T> (T component, int xDir, int yDir) {
    	Block pushBlock = component as Block;
    	pushBlock.PushBlock(xDir, yDir);
    	animator.SetTrigger("playerChop");
    }

    private void Restart() {
    	Application.LoadLevel(Application.loadedLevel);
    }
}
