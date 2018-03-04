using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : SpawnableObject, IMoveable, IAttackable, IHittable, ISmartAI, SmartBrainDelegate {
    private float _attack;
    private float _attackRange;
    private float _defend;
    private float _critical;
    private float _criticalChance;
    private float _healthPoint;
    private float _stamina;
    private float _mana;
    private float _movementSpeed;

    private SmartBrain _brain;
    public GameObject _objectToBeObserved; // enemy

    private float _maxAttackDuration;
    private float _attackDuration = 0;
    private bool _isAttacking = false;
    private AttackState _attackState = AttackState.Idle;
        

    // SETTER GETTER

    public Vector2 distanceToTarget {
        get { return StatusConversionHelper.GetDistanceBetween(gameObject, _objectToBeObserved); }
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

    public SmartBrain brain {
        get { return _brain; }
        set { _brain = value; }
    }

    public GameObject body {
        get { return gameObject; }
        set { gameObject = value; }
    }

    public GameObject objectToBeObserved {
        get { return _objectToBeObserved; }
        set { _objectToBeObserved = value; }
    }

    public float alertArea {
        get { return brain.alertArea; }
        set { brain.alertArea = value; }
    }

    public float awarenessArea {
        get { return brain.awarenessArea; }
        set { brain.awarenessArea = value; }
    }

    public float aggresiveArea {
        get { return brain.aggresiveArea; }
        set { brain.aggresiveArea = value; }
    }

    // MONO BEHAVIOUR

    public override void DoOnAwake() {
        base.DoOnAwake();

        brain = new SmartBrain(this);
        objectToBeObserved = GameObject.FindGameObjectWithTag(MyConstant.Tag.PLAYER);
    }

    public override void DoOnFixedUpdate() {
        base.DoOnFixedUpdate();

        brain.Observe(objectToBeObserved);
    }

    // LOGIC

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

    protected void Follow(GameObject target, float speedPercentage) {
        float desiredSpeed = (speedPercentage / 100) * movementSpeed;
        float actualSpeed = StatusConversionHelper.GetActualMovementSpeed(desiredSpeed);

        rigidBody.transform.position = Vector2.MoveTowards(
            rigidBody.transform.position, 
            target.transform.position,
            actualSpeed * Time.deltaTime);
    }

    public void LookIntoTarget() {
        Vector3 currentScale = gameObject.transform.localScale;

        if (isTargetOnRight && gameObject.transform.localScale.x < 0) {
            currentScale.x = Math.Abs(currentScale.x);
        } else if (!isTargetOnRight && gameObject.transform.localScale.x > 0) {
            currentScale.x *= -1;
        }

        gameObject.transform.localScale = currentScale;
    }

    // SMART BRAIN DELEGATE

    public void OnAlert(GameObject target) {
        LookIntoTarget();
    }

    public void OnAggresive(GameObject target) {
        Follow(target, 100);
        LookIntoTarget();
    }

    public void OnAware(GameObject target) {
        Follow(target, 50);
        LookIntoTarget();
    }

    public void OnFleeing(GameObject target) {
        // doin nothing
    }

    public void OnIdle(GameObject target) {
        // doin nothing
    }
}
