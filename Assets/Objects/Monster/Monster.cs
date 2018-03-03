using System.Collections;using System.Collections.Generic;using UnityEngine;public class Monster : SpawnableObject, IMoveable, IAttackable, IHittable {    private float _attack;    private float _defend;    private float _critical;    private float _criticalChance;    private float _healthPoint;    private float _movementSpeed;
    
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

    public float defend {
        get { return _defend; }
        set { _defend = value; }
    }

    public void Attack(IHittable target)  {
        target.GetHit(attack);
    }

    public void GetHit(float attack) {
        throw new System.NotImplementedException();
    }

    public void Move(Vector2 direction) {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {			}		// Update is called once per frame	void Update () {			}}