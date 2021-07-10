using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    
    private float startX, startY;
    private int blockLength = 1; // distance to move one block-length 
    private float rayLength = 0.5f;
 
    public LayerMask blockingLayer;
	private BoxCollider2D boxCollider;
    private Rigidbody2D block;

 
    void Start() {
        block  = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        startX = transform.position.x;
        startY = transform.position.y;
    }


    protected bool Move (int xDir, int yDir, out RaycastHit2D hit) {
    	Vector2 start = transform.position;
    	Vector2 end = start;

    	if (xDir!=0) {
    		end += new Vector2(xDir, 0);
		} else if (yDir!=0) {
			end += new Vector2(0, yDir);
		}

    	boxCollider.enabled = false;
    	hit = Physics2D.Linecast(start, end, blockingLayer);
    	boxCollider.enabled = true;

    	if (hit.transform == null) return true;

    	return false;
    }


    public void PushBlock(int xDir, int yDir) {
    	RaycastHit2D hit;
    	block.isKinematic = true;

    	bool canMove = Move(xDir, yDir, out hit);

    	if (canMove) {
	    	if (xDir != 0) {
	    		transform.position = new Vector2(transform.position.x + xDir*blockLength, transform.position.y);
			} else if (yDir != 0) {
				transform.position = new Vector2(transform.position.x, transform.position.y + yDir*blockLength);
			}

		} 	
    }
    
}
