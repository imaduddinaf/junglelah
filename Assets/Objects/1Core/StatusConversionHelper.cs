using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusConversionHelper {

    static float SPEED_DIVIDER = 50.0f;

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
}
