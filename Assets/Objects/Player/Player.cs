using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SpawnableObject, IMoveable, IAttackable, IHittable {
    private float _attack;
    private float _attackRange;
    private float _defend;
    private float _critical;
    private float _criticalChance;
    private float _healthPoint;
    private float _stamina;
    private float _mana;
    private float _movementSpeed;
    public float defaultMovSpeed;

    private float _maxAttackDuration = 1;
    private float _attackDuration = 0;
    private bool _isAttacking = false;
    private AttackState _attackState = AttackState.Idle;

    public float movementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public float attack {
        get { return _attack; }
        set { _attack = value; }
    }

    public float attackRange {
        get { return _attackRange; }
        set { _attackRange = value; }
    }

    public float critical {
        get { return _critical; }
        set { _critical = value; }
    }

    public float criticalChance {
        get { return _criticalChance; }
        set { _criticalChance = value; }
    }

    public float maxAttackDuration {
        get { return _maxAttackDuration; }
        set { _maxAttackDuration = value; }
    }

    public float attackDuration {
        get { return _attackDuration; }
        set { _attackDuration = value; }
    }

    public bool isAttacking {
        get { return _isAttacking; }
        set { _isAttacking = value; }
    }

    public AttackState attackState {
        get { return _attackState; }
        set { _attackState = value; }
    }

    public float healthPoint {
        get { return _healthPoint; }
        set { _healthPoint = value; }
    }

    public float stamina {
        get { return _stamina; }
        set { _stamina = value; }
    }

    public float mana {
        get { return _mana; }
        set { _mana = value; }
    }

    public float defense {
        get { return _defend; }
        set { _defend = value; }
    }

    public void Move(Vector2 direction) {
        float actualSpeed = StatusConversionHelper.GetActualMovementSpeed(movementSpeed);
        float MAGIC_CONST = 32; // const convert addtoforce speed matching movetowards speed

        rigidBody.AddForce(direction * Time.deltaTime * actualSpeed * MAGIC_CONST, ForceMode2D.Impulse);
    }

    public void Attack(IHittable target) {
        float attackDamage = StatusConversionHelper.GetAttackDamage(attack, critical, criticalChance);
        target.GetHitBy(this);
    }

    public void Kill(IDropableObject target, GameObject targetGameObject) {
        List<IItem> drops = target.GetDrops();

        // save drops

        targetGameObject.Destroy();
    }

    public void GetHitBy(IAttackable enemy) {
        Debug.Log("player got hit by something");
    }

    public void KilledBy(IAttackable enemy) { 
        // empty
    }

    public override void DoOnStart() {
        base.DoOnStart();

        if (defaultMovSpeed > 0) {
            movementSpeed = defaultMovSpeed;
        } else {
            movementSpeed = 50; // dummy speed
        }
	}

    public override void DoOnTriggerEnter(Collider2D other) {
        base.DoOnTriggerEnter(other);

        // empty
    }
}
