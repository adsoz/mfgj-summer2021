using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

	public float restartLevelDelay = 1f;

	private Animator animator;

    // Start is called before the first frame update
    protected override void Start() {
    	animator = GetComponent<Animator>();
    	base.Start();

    	// start position
    	transform.position = new Vector3(4,12,0);
        
    }

    // Update is called once per frame
    private void Update() {
    	int horizontal = 0;
    	int vertical = 0;

    	horizontal = (int) (Input.GetAxisRaw("Horizontal"));
    	vertical = (int) (Input.GetAxisRaw("Vertical"));

    	if (horizontal!=0) vertical = 0;
    	
    	if (horizontal!=0 || vertical!=0)

    	AttemptMove<Block>(horizontal, vertical);
        
    }

    protected override void AttemptMove <T> (int xDir, int yDir) {
    	base.AttemptMove <T> (xDir, yDir);
    	RaycastHit2D hit;

    	bool canMove = Move(xDir, yDir, out hit);

    	if (hit.transform == null) return;

    	T hitComponent = hit.transform.GetComponent<T>();

    	if (!canMove && hitComponent!=null) {
    		OnCantMove(hitComponent, xDir, yDir);
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
