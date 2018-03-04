using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackState {
    Idle, Full
}

public interface IAttackable {
    float attack { get; set; }
    float attackRange { get; set; }
    float stamina { get; set; }
    float mana { get; set; }
    float critical { get; set; }
    float criticalChance { get; set; }
    Rigidbody2D rigidBody { get; }

    float maxAttackDuration { get; set; }
    float attackDuration { get; set; }
    bool isAttacking { get; set; }
    AttackState attackState { get; set; }

    void Attack(IHittable target);
}
