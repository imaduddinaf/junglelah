using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable {
    float healthPoint { get; set; }
    float defense { get; set; }
    Rigidbody2D rigidBody { get; }

    void GetHitBy(IAttackable enemy);
    void KilledBy(IAttackable enemy);
}
