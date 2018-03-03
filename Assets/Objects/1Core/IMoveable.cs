using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable {
    Rigidbody2D rigidBody { get; }
    float movementSpeed { get; set; }

    void Move(Vector2 direction);
}