using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

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
        if (moveable == null || moveable.rigidBody == null) return;

        moveable.rigidBody.velocity = Vector2.zero;
        
		if (Input.GetKey(KeyCode.W)) { 
            moveable.Move(Vector2.up);
        }

        if (Input.GetKey(KeyCode.D)) { 
            moveable.Move(Vector2.right);
        }

        if (Input.GetKey(KeyCode.S)) {
            moveable.Move(Vector2.down);
        }

        if (Input.GetKey(KeyCode.A)) {
            moveable.Move(Vector2.left);
        }

        
    }
}
