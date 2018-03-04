using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Monster {
    private GameObject _hand;

    // MONO BEHAVIOUR

    public override void DoOnAwake() {
        base.DoOnAwake();

        _hand = gameObject.transform.Find("Hand").gameObject;
    }

    public override void DoOnStart() {
        base.DoOnStart();

        attack = 50;
        attackRange = 40;
        healthPoint = 200;
        defend = 20;
        movementSpeed = 100;

        alertArea = 200;
        awarenessArea = 160;
        aggresiveArea = 80;
        maxAttackDuration = 1;
    }

    public override void DoOnFixedUpdate() {
        base.DoOnFixedUpdate();

        // animate the hand   
        if (isAttacking && Time.time > attackDuration) {
            attackDuration += maxAttackDuration;

            switch (attackState) {
                case AttackState.Idle:
                    MoveHandAttack(false);
                    break;
                case AttackState.Full:
                    MoveHandAttack(true);
                    break;
            }
        }

        // attack if inside attack range
        if (!isAttacking && StatusConversionHelper.IsInsideRange(distanceToTarget, attackRange)) {
            AttackAction();
        }
    }

    // LOGIC

    public override void AttackAction() {
        base.AttackAction();

        isAttacking = true;
        attackState = AttackState.Full;
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
            attackState = AttackState.Idle;
        } else { // back
            Vector2 backDirection = direction == Vector2.right ? Vector2.left : Vector2.right;
            _hand.GetComponent<Rigidbody2D>().transform.Translate(direction * Time.deltaTime * -MAGIC_CONST);
            //_hand.GetComponent<Rigidbody2D>().AddForce(direction * Time.deltaTime * -MAGIC_CONST, ForceMode2D.Impulse);
            isAttacking = false;
        }
    }
}
