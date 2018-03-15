using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable {
	bool isSprinting { get; set; }
	void Move (Vector2 direction);
	void Sprint (Vector2 direction);
	void Attack ();
	void Dash (Vector2 direction);
}
