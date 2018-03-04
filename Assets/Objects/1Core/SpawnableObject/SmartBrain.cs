using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SmartBrainState {
    Aware, Aggresive, Fleeing, Alert, Idle // game designer to decide
}

public interface SmartBrainDelegate {
    Vector2 distanceToTarget { get; }

    GameObject body { get; }
    GameObject objectToBeObserved { get; } 

    void OnAlert(GameObject target);
    void OnAggresive(GameObject target);
    void OnAware(GameObject target);
    void OnFleeing(GameObject target);
    void OnIdle(GameObject target);
}

public class SmartBrain {
    public SmartBrainDelegate brainDelegate;

    public SmartBrainState state = SmartBrainState.Idle;
    public float alertArea;
    public float awarenessArea;
    public float aggresiveArea;

    public SmartBrain(SmartBrainDelegate brainDelegate) {
        this.brainDelegate = brainDelegate;
    }

    public void Observe(GameObject target) {
        state = DetermineState(target);

        switch (state) {
            case SmartBrainState.Aggresive:
                brainDelegate.OnAggresive(target);
                break;
            case SmartBrainState.Alert:
                brainDelegate.OnAlert(target);
                break;
            case SmartBrainState.Aware:
                brainDelegate.OnAware(target);
                break;
            case SmartBrainState.Fleeing:
                brainDelegate.OnFleeing(target);
                break;
            case SmartBrainState.Idle:
                brainDelegate.OnIdle(target);
                break;
        }
    }

    public SmartBrainState DetermineState(GameObject target) {
        SmartBrainState state;

        if (ShouldFlee()) {
            state = SmartBrainState.Fleeing;
        } else if (StatusConversionHelper.IsInsideRange(brainDelegate.distanceToTarget, StatusConversionHelper.GetActualAIStateArea(aggresiveArea))) {
            state = SmartBrainState.Aggresive;
        } else if (StatusConversionHelper.IsInsideRange(brainDelegate.distanceToTarget, StatusConversionHelper.GetActualAIStateArea(awarenessArea))) {
            state = SmartBrainState.Aware;
        } else if (StatusConversionHelper.IsInsideRange(brainDelegate.distanceToTarget, StatusConversionHelper.GetActualAIStateArea(alertArea))) {
            state = SmartBrainState.Alert;
        } else {
            state = SmartBrainState.Idle;
        }

        return state;
    }

    private bool ShouldFlee() {
        // TODO:
        // complete this func
        // check if delegate has custom flee logic
        // lazy init?

        bool shouldFlee = false;

        IAttackable attacker = brainDelegate.objectToBeObserved.GetComponent<IAttackable>();
        IHittable defender = brainDelegate.body.GetComponent<IHittable>();

        return shouldFlee;
    }
}
