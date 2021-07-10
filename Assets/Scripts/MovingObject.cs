using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	public float moveTime = 0.001f;
	public LayerMask blockingLayer;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2D;
	private float inverseMoveTime;
    public bool isMoving;

    // Start is called before the first frame update
    protected virtual void Start() {
	    boxCollider = GetComponent<BoxCollider2D>();
	    rb2D = GetComponent<Rigidbody2D>();
	    inverseMoveTime = 1f / moveTime;
      
    }

    protected bool Move (Vector2 dir) {

    	boxCollider.enabled = false;
    	RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, blockingLayer);
        boxCollider.enabled = true;

		Vector3 displacement = (hit.distance - 0.5f) * dir;

		Vector3 end = transform.position + displacement;
		if (hit.collider == null) return false;

		StartCoroutine(SmoothMovement(end));
		return true;
    }

    protected IEnumerator SmoothMovement (Vector3 end) {
        isMoving = true;

    	float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
    	
    	while (sqrRemainingDistance > float.Epsilon) {
    		Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
    		rb2D.MovePosition(newPosition);
    		sqrRemainingDistance = (transform.position - end).sqrMagnitude;
    		yield return null;
    	}

        rb2D.MovePosition (end);
        isMoving = false;
    }

    protected abstract void OnCantMove <T> (T component, int xDir, int yDir) where T : Component;
}
