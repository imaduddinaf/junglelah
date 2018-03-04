using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusConversionHelper {

    static float SPEED_DIVIDER = 50;
    static float AREA_DIVIDER = 10;

    public static float GetAttackDamage(float attack, float critical, float criticalChance) {
        // wait for designer formulas
        return attack; 
    }

    public static float GetHitDamage(float attack, float defend) {
        // wait for designer formulas
        return attack;
    }

    public static float GetActualMovementSpeed(float writtenSpeed) {
        // wait for designer formulas
        return writtenSpeed / SPEED_DIVIDER;
    }

    public static float GetActualAIStateArea(float stateArea) { // alert, awareness, aggresive (unit)
        // wait for designer formulas
        return stateArea / AREA_DIVIDER;
    }

    public static float GetActualAttackRange(float attackRange) { // unit
        // wait for designer formulas
        return attackRange / AREA_DIVIDER;
    }

    public static bool IsInsideRange(Vector2 distance, float range) {
        return Math.Abs(distance.x) <= range && Math.Abs(distance.y) <= range;
    }

    public static Vector2 GetDistanceBetween(GameObject currentObject, GameObject target) {
        // value
        float targetX = target.transform.position.x;
        float targetY = target.transform.position.y;
        float targetWidth = target.GetComponent<SpriteRenderer>().bounds.size.x;
        float targetHeight = target.GetComponent<SpriteRenderer>().bounds.size.y;

        float currentX = currentObject.transform.position.x;
        float currentY = currentObject.transform.position.y;
        float currentWidth = currentObject.GetComponent<SpriteRenderer>().bounds.size.x;
        float currentHeight = currentObject.GetComponent<SpriteRenderer>().bounds.size.y;

        return new Vector2(
            GetActualDistanceBetween(currentX, currentWidth, targetX, targetWidth), 
            GetActualDistanceBetween(currentY, currentHeight, targetY, targetHeight) // -> still buggy on y distance
            );
    }

    private static float GetActualDistanceBetween(
        float currentPosition, // x or y
        float currentSize, // width or height
        float targetPosition, 
        float targetSize) {

        float anchorDistance = targetPosition - currentPosition; // + right/top, - left/bottom

        float actualTargetPosition = targetPosition - (0.5f * targetSize);
        float actualCurrentPosition = currentPosition + (0.5f * currentSize);
        float actualDistance = actualTargetPosition - actualCurrentPosition; // target on right/top

        if (actualDistance < 0) { // target on left/bottom
            actualTargetPosition = targetPosition + (0.5f * targetSize);
            actualCurrentPosition = currentPosition - (0.5f * currentSize);
            actualDistance = actualTargetPosition - actualCurrentPosition;
        }

        return actualDistance;
    }
}
