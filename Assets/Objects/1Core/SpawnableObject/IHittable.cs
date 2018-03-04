using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable {
    float healthPoint { get; set; }
    float defend { get; set; }
    Rigidbody2D rigidBody { get; }

    void GetHit(float attack);
}
