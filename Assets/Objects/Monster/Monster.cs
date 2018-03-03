using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;public enum MonsterState {
    Aware, Aggresive, Fleeing, Alert, Idle
}public enum AttackState {
    Idle, Full
}public abstract class Monster : SpawnableObject, IMoveable, IAttackable, IHittable {    private float _attack;    private float _defend;    private float _critical;    private float _criticalChance;    private float _healthPoint;    private float _stamina;    private float _mana;    private float _movementSpeed;    public MonsterState state = MonsterState.Idle;    public GameObject objectToBeObserved;    public float xDistanceToTarget {
        get {
            float targetX = objectToBeObserved.transform.position.x;
            float targetWidth = objectToBeObserved.GetComponent<SpriteRenderer>().bounds.size.x;

            float thisX = gameObject.transform.position.x;
            float thisWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;

            float anchorDistance = targetX - thisX; // + right, - left
            
            float actualTargetX = targetX - (0.5f * targetWidth);
            float actualThisX = thisX + (0.5f * thisWidth);
            float actualDistance = actualTargetX - actualThisX; // target on right

            if (anchorDistance < 0) { // target on left
                actualTargetX = targetX + (1 / 2 * targetWidth);
                actualThisX = thisX - (1 / 2 * thisWidth);
                actualDistance = actualTargetX - actualThisX;
            }

            return actualDistance;
        }
    }    public bool isTargetOnRight {
        get { return xDistanceToTarget > 0; }
    }        public float movementSpeed {        get { return _movementSpeed; }        set { _movementSpeed = value; }    }        public float attack {        get { return _attack; }        set { _attack = value; }    }    public float critical {        get { return _critical; }        set { _critical = value; }    }    public float criticalChance {        get { return _criticalChance; }        set { _criticalChance = value; }    }    public float healthPoint {        get { return _healthPoint; }        set { _healthPoint = value; }    }    public float stamina{        get { return _stamina; }        set { _stamina = value; }    }    public float mana {        get { return _mana; }        set { _mana = value; }    }    public float defend {        get { return _defend; }        set { _defend = value; }    }    public void Attack(IHittable target)  {        float attackDamage = StatusConversionHelper.GetAttackDamage(attack, critical, criticalChance);        target.GetHit(attackDamage);    }    public virtual void AttackAction() {
        // empty
    }    public void GetHit(float attack) {        healthPoint -= StatusConversionHelper.GetHitDamage(attack, defend);    }    public void Move(Vector2 direction) {        // empty, only follows enemy    }

    public override void DoOnStart() {
        base.DoOnStart();

        objectToBeObserved = GameObject.FindGameObjectWithTag("Player");
    }

    public override void DoOnFixedUpdate() {
        base.DoOnFixedUpdate();

        Observe(objectToBeObserved);        UpdateFacing();
    }    public void Observe(GameObject target) {
        state = DetermineState(target);

        switch (state) {
            case MonsterState.Aggresive:
                Follow(target, 100);
                break;
            case MonsterState.Alert:
                break;
            case MonsterState.Aware:
                Follow(target, 60);
                break;
            case MonsterState.Fleeing:
                break;
            case MonsterState.Idle:
                break;
        }
    }    protected MonsterState DetermineState(GameObject target) {
        return MonsterState.Aware;
    }    protected void Follow(GameObject target, float speedPercentage) {
        float desiredSpeed = (speedPercentage / 100) * movementSpeed;
        float actualSpeed = StatusConversionHelper.GetActualMovementSpeed(desiredSpeed);

        //rigidBody.transform.LookAt(target.transform);

        rigidBody.transform.position = Vector2.MoveTowards(
            rigidBody.transform.position, 
            target.transform.position,
            actualSpeed * Time.deltaTime);
    }    public void UpdateFacing() {
        Vector3 currentScale = gameObject.transform.localScale;

        if (isTargetOnRight && gameObject.transform.localScale.x > 0) {
            currentScale.x = Math.Abs(currentScale.x);
        } else if (!isTargetOnRight && gameObject.transform.localScale.x < 0) {
            currentScale.x *= -1;
        }

        gameObject.transform.localScale = currentScale;
    }}