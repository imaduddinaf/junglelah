using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	private Rigidbody2D rb;
	public float movementSpeed;

	void Awake() {
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MovementInput ();	
	}

	void MovementInput () {
		rb.velocity = Vector2.zero;
		if (Input.GetKey (KeyCode.W)) { //move up
			rb.AddForce (Vector2.up * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
		}
		if (Input.GetKey (KeyCode.D)) { //move right
			rb.AddForce (Vector2.right * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
		}
		if (Input.GetKey (KeyCode.S)) { //move down
			rb.AddForce (Vector2.down * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
		}
		if (Input.GetKey (KeyCode.A)) { //move left
			rb.AddForce (Vector2.left * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
		}
	}
}
