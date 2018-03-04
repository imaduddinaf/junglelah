using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SpawnableObject, IMoveable, IAttackable, IHittable {
    private float _attack;
    private float _defend;
    private float _critical;
    private float _criticalChance;
    private float _healthPoint;
    private float _stamina;
    private float _mana;
    private float _movementSpeed;
    public float defaultMovSpeed;

    public float movementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public float attack {
        get { return _attack; }
        set { _attack = value; }
    }

    public float critical {
        get { return _critical; }
        set { _critical = value; }
    }

    public float criticalChance {
        get { return _criticalChance; }
        set { _criticalChance = value; }
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

    public float defend {
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
        target.GetHit(attackDamage);
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

    public void GetHit(float attack) {
        Debug.Log("player got hit by something");
    }
}
