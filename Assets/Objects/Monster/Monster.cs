using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class Monster : SpawnableObject, IMoveable, IAttackable, IHittable, ISmartAI, ISmartBrainDelegate, IDropableObject {
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

    private List<Tuple<IItem, float>> _drops;

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

    public float defense {
        get { return _defend; }
        set { _defend = value; }
    }

    public SmartBrain brain {
        get { return _brain; }
        set { _brain = value; }
    }

    public GameObject body {
        get { return gameObject; }
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

    public List<Tuple<IItem, float>> drops {
        get { return _drops; }
        set { _drops = value; }
    }

    // MONO BEHAVIOUR
    private RandomTester _randomTester30 = new RandomTester(500, 30);
    private RandomTester _randomTester50 = new RandomTester(500, 50);
    private RandomTester _randomTester70 = new RandomTester(500, 70);

    private void RandomTest() { 
        Thread t1 = new Thread(_randomTester30.ProbabilityTest);
        Thread t2 = new Thread(_randomTester50.ProbabilityTest);
        Thread t3 = new Thread(_randomTester70.ProbabilityTest);

        t1.Start();
        t2.Start();
        t3.Start();
    }
    
    public override void DoOnAwake() {
        base.DoOnAwake();

        brain = new SmartBrain(this);
        objectToBeObserved = GameObject.FindGameObjectWithTag(MyConstant.Tag.PLAYER);

        InitAttributes();
        PopulateDrops();

        //RandomTest();
    }

    public override void DoOnFixedUpdate() {
        base.DoOnFixedUpdate();
        
        // weird, sometimes this object become null
        brain.Observe(objectToBeObserved);
    }

    // LOGIC

    public virtual void InitAttributes() {
        // init monster properties by code
    }

    public virtual void PopulateDrops() {
        // init monster properties by code
    }

    public virtual void AttackAction() {
        // empty
    }

    public void Attack(IHittable target)  {
        float attackDamage = StatusConversionHelper.GetAttackDamage(attack, critical, criticalChance);
        target.GetHitBy(this);
    }

    public void Kill(IDropableObject target, GameObject targetGameObject) {
        targetGameObject.Destroy();
    }

    public void GetHitBy(IAttackable enemy) {
        healthPoint -= StatusConversionHelper.GetHitDamage(enemy.attack, defense);

        if (healthPoint <= 0) KilledBy(enemy);
    }

    public void KilledBy(IAttackable enemy) {
        enemy.Kill(this, gameObject);
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

    private void LookIntoTarget() {
        LookTarget(true);
    }

    private void LookAwayFromTarget() {
        LookTarget(false);
    }

    private void LookTarget(bool into) {
        Vector3 currentScale = gameObject.transform.localScale;

        if (into) {
            if (isTargetOnRight && gameObject.transform.localScale.x < 0) {
                currentScale.x = Math.Abs(currentScale.x);
            } else if (!isTargetOnRight && gameObject.transform.localScale.x > 0) {
                currentScale.x *= -1;
            }
        } else {
            if (isTargetOnRight && gameObject.transform.localScale.x > 0) {
                currentScale.x *= -1;
            } else if (!isTargetOnRight && gameObject.transform.localScale.x < 0) {
                currentScale.x = Math.Abs(currentScale.x);
            }
        }
        

        gameObject.transform.localScale = currentScale;
    }

    // SMART BRAIN DELEGATE

    public void OnAlert(GameObject target) {
        LookIntoTarget();
    }

    public void OnAggresive(GameObject target) {
        Follow(target, 100);
        LookAwayIntoTarget();
    }

    public void OnAware(GameObject target) {
        Follow(target, 50);
        LookIntoTarget();
    }

    public void OnFleeing(GameObject target) {
        Follow(target, -100);
        LookAwayFromTarget();
    }

    public void OnIdle(GameObject target) {
        // doin nothing
    }

    public List<IItem> GetDrops() {
        List<IItem> probableDrops = new List<IItem>();

        foreach (Tuple<IItem, float> drop in drops) {
            IItem item = drop.first;
            float probability = drop.last;

            if (RandomHelper.ShouldGotValue(probability)) probableDrops.Add(item);
        }

        return probableDrops;
    }
}
