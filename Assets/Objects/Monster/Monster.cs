using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : SpawnableObject, IMoveable, IAttackable, IHittable, ISmartAI {
    private float _attack;
    private float _defend;
    private float _critical;
    private float _criticalChance;
    private float _healthPoint;
    private float _stamina;
    private float _mana;
    private float _movementSpeed;

    private float _alertArea;
    private float _awarenessArea;
    private float _aggresiveArea;

    public SmartAIState state = SmartAIState.Idle;
    public GameObject objectToBeObserved; // enemy

    public float xDistanceToTarget {
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
                actualTargetX = targetX + (0.5f * targetWidth);
                actualThisX = thisX - (0.5f * thisWidth);
                actualDistance = actualTargetX - actualThisX;
            }

            return actualDistance;
        }
    }

    public bool isTargetOnRight {
        get {
            float targetX = objectToBeObserved.transform.position.x;
            float thisX = gameObject.transform.position.x;
            float anchorDistance = targetX - thisX;

            return anchorDistance > 0;
        }
    }
    
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

    public float stamina{
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

    public float alertArea {
        get { return _alertArea; }
        set { _alertArea = value; }
    }

    public float awarenessArea {
        get { return _awarenessArea; }
        set { _awarenessArea = value; }
    }

    public float aggresiveArea {
        get { return _aggresiveArea; }
        set { _aggresiveArea = value; }
    }

    public void Attack(IHittable target)  {
        float attackDamage = StatusConversionHelper.GetAttackDamage(attack, critical, criticalChance);
        target.GetHit(attackDamage);
    }

    public virtual void AttackAction() {
        // empty
    }

    public void GetHit(float attack) {
        healthPoint -= StatusConversionHelper.GetHitDamage(attack, defend);
    }

    public void Move(Vector2 direction) {
        // empty, only follows enemy
    }

    public override void DoOnStart() {
        base.DoOnStart();

        objectToBeObserved = GameObject.FindGameObjectWithTag("Player");
    }

    public override void DoOnFixedUpdate() {
        base.DoOnFixedUpdate();

        Observe(objectToBeObserved);
    }

    public void Observe(GameObject target) {
        state = DetermineState(target);

        switch (state) {
            case SmartAIState.Aggresive:
                Follow(target, 100);
                UpdateFacing();
                break;
            case SmartAIState.Alert:
                UpdateFacing();
                break;
            case SmartAIState.Aware:
                Follow(target, 50);
                UpdateFacing();
                break;
            case SmartAIState.Fleeing:
                break;
            case SmartAIState.Idle:
                break;
        }
    }

    public SmartAIState DetermineState(GameObject target) {
        SmartAIState state;

        if (xDistanceToTarget <= StatusConversionHelper.GetActualAIStateArea(aggresiveArea)) {
            state = SmartAIState.Aggresive;
        } else if (xDistanceToTarget <= StatusConversionHelper.GetActualAIStateArea(awarenessArea)) {
            state = SmartAIState.Aware;
        } else if (xDistanceToTarget <= StatusConversionHelper.GetActualAIStateArea(alertArea)) {
            state = SmartAIState.Alert;
        } else {
            state = SmartAIState.Idle;
        }

        return state;
    }

    protected void Follow(GameObject target, float speedPercentage) {
        float desiredSpeed = (speedPercentage / 100) * movementSpeed;
        float actualSpeed = StatusConversionHelper.GetActualMovementSpeed(desiredSpeed);

        //rigidBody.transform.LookAt(target.transform);

        rigidBody.transform.position = Vector2.MoveTowards(
            rigidBody.transform.position, 
            target.transform.position,
            actualSpeed * Time.deltaTime);
    }

    public void UpdateFacing() {
        Vector3 currentScale = gameObject.transform.localScale;

        if (isTargetOnRight && gameObject.transform.localScale.x < 0) {
            currentScale.x = Math.Abs(currentScale.x);
        } else if (!isTargetOnRight && gameObject.transform.localScale.x > 0) {
            currentScale.x *= -1;
        }

        gameObject.transform.localScale = currentScale;
    }
}
