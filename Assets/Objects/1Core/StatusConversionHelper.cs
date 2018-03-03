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
}
