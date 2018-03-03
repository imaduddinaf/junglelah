using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable {
    float movementSpeed { get; set; }
    Rigidbody2D rigidBody { get; }

    void Move(Vector2 direction);
}