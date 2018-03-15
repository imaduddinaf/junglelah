using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {
    public GameObject controllableObject;
    public IMoveable moveable;
	public IControllable controllable;

    void Awake() {
        moveable = controllableObject.GetComponent<IMoveable> ();
		controllable = controllableObject.GetComponent<IControllable> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MovementInput ();
		ActionInput ();
	}

	void MovementInput () {
		if (moveable == null || moveable.rigidBody == null || controllable == null) return;
		Vector2 direction = Vector2.zero;
		moveable.rigidBody.velocity = Vector2.zero;

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			controllable.isSprinting = !controllable.isSprinting;
		}
			
		if (Input.GetKey(KeyCode.W)) {
			direction.y += 1; //up
        }

        if (Input.GetKey(KeyCode.D)) {
			direction.x += 1; //right
        }

        if (Input.GetKey(KeyCode.S)) {
			direction.y -= 1; //down
        }

        if (Input.GetKey(KeyCode.A)) {
			direction.x -= 1; //left
        }

		if (controllable.isSprinting) {
			controllable.Sprint (direction);
			Debug.Log ("Sprinting");
		} else {
			controllable.Move (direction);
		}
    }

	void ActionInput() {
		if (moveable == null || moveable.rigidBody == null || controllable == null) return;

		if (Input.GetKey (KeyCode.J)) {
			controllable.Attack ();
		}
	}
}
