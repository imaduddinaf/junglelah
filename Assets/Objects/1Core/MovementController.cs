using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public GameObject moveableObject;
    public IMoveable moveable;

    void Awake() {
        moveable = moveableObject.GetComponent<IMoveable>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MovementInput ();	
	}

	void MovementInput () {
        moveable.rigidBody.velocity = Vector2.zero;
        Vector2 movementDirection = Vector2.zero;
        
		if (Input.GetKey(KeyCode.W)) { 
            movementDirection = Vector2.up;
        }

        if (Input.GetKey(KeyCode.D)) { 
            movementDirection = Vector2.right;
        }

        if (Input.GetKey(KeyCode.S)) {
            movementDirection = Vector2.down;
        }

        if (Input.GetKey(KeyCode.A)) {
            movementDirection = Vector2.left;
        }

        moveable.Move(movementDirection);
    }
}
