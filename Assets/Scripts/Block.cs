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

    public static bool boxMoving = false;
 
    void Start() {
        block  = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        startX = transform.position.x;
        startY = transform.position.y;
    }


    protected bool Move (int xDir, int yDir, out RaycastHit2D hit) {
        Vector2 start = new Vector2(transform.position.x, transform.position.y);
        Vector2 end = start;

        if (xDir!=0) {
            end += new Vector2(xDir, 0);
        } else if (yDir!=0) {
            end += new Vector2(0, yDir);
        }
        Debug.Log(transform.position);
        Debug.Log(start);
        Debug.Log(end);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        Debug.Log("Box Ray fired");
        Debug.Log(hit.point);
        Debug.Log(hit.collider);
        Debug.Log("Hit distance:" + hit.distance);
        boxCollider.enabled = true;

        if (hit.collider == null) return true;
        return false;
    }


    public void PushBlock(int xDir, int yDir) {
    	RaycastHit2D hit;
    	bool canMove = Move(xDir, yDir, out hit);
        Debug.Log(canMove);
        Vector3 end = transform.position + new Vector3(xDir, yDir);
    	if (canMove) {
            StartCoroutine(SmoothMovement(end));
		}
    }

    protected IEnumerator SmoothMovement (Vector3 end) {
        boxMoving = true;

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        
        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPosition = Vector3.MoveTowards(block.position, end, 30 * Time.deltaTime);
            block.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        block.MovePosition (end);
        boxMoving = false;
        
    }

    
}
