using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    
    float startX, startY;
 
    Rigidbody2D block;
 
    void Start() {
        block  = GetComponent<Rigidbody2D>();
        startX = transform.position.x;
        startY = transform.position.y;
    }

    public void PushBlock(int xDir, int yDir) {
    	block.isKinematic = true;
    	transform.position = new Vector2(transform.position.y + xDir, transform.position.y+yDir);
    }

    
}
