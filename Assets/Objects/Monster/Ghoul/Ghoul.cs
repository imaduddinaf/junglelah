using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Monster {
    private float _maxAttackDuration = 1;
    private float _attackDuration = 0;
    private bool _isAttacking = false;
    private AttackState _attackState = AttackState.Idle;
    private GameObject _hand;

    public override void DoOnAwake() {
        base.DoOnAwake();

        _hand = gameObject.transform.Find("Hand").gameObject;
    }

    public override void DoOnStart() {
        base.DoOnStart();

        attack = 50;
        healthPoint = 200;
        defend = 20;
        movementSpeed = 100;

        alertArea = 200;
        awarenessArea = 160;
        aggresiveArea = 80;
    }

    public override void DoOnFixedUpdate() {
        base.DoOnFixedUpdate();

        // animate the hand   
        if (_isAttacking && Time.time > _attackDuration) {
            _attackDuration += _maxAttackDuration;

            switch (_attackState) {
                case AttackState.Idle:
                    MoveHandAttack(false);
                    break;
                case AttackState.Full:
                    MoveHandAttack(true);
                    break;
            }
        }

        // attack if distance < 4 unit
        if (!_isAttacking && Math.Abs(xDistanceToTarget) < 4) {
            AttackAction();
        }
    }

    public override void AttackAction() {
        base.AttackAction();

        _isAttacking = true;
        _attackState = AttackState.Full;
    }

    public override void TriggerDetectOnChild(GameObject child, GameObject target) {
        base.TriggerDetectOnChild(child, target);

        IHittable hittableTarget = target.GetComponent<IHittable>();

        if (child.tag == MyConstant.Tag.HIT_AREA && hittableTarget != null) {
            Attack(hittableTarget);
        }
    }

    public void MoveHandAttack(bool punch) {
        float MAGIC_CONST = 60; // hit length
        Vector2 direction = Vector2.left;

        if (isTargetOnRight) {
            direction = Vector2.right;
        }

        if (punch) { // do punch
            _hand.GetComponent<Rigidbody2D>().transform.Translate(direction * Time.deltaTime * MAGIC_CONST);
            //_hand.GetComponent<Rigidbody2D>().AddForce(direction * Time.deltaTime * MAGIC_CONST, ForceMode2D.Impulse);
            _attackState = AttackState.Idle;
        } else { // back
            Vector2 backDirection = direction == Vector2.right ? Vector2.left : Vector2.right;
            _hand.GetComponent<Rigidbody2D>().transform.Translate(direction * Time.deltaTime * -MAGIC_CONST);
            //_hand.GetComponent<Rigidbody2D>().AddForce(direction * Time.deltaTime * -MAGIC_CONST, ForceMode2D.Impulse);
            _isAttacking = false;
        }
    }
}
