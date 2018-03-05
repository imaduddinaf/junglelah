using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SmartBrainState {
    Aware, Aggresive, Fleeing, Alert, Idle // game designer to decide
}

public interface ISmartBrainDelegate {
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
    public WeakReference brainDelegate;
    private ISmartBrainDelegate _brainDelegate {
        get { return brainDelegate.Target as ISmartBrainDelegate; }
    }

    public SmartBrainState state = SmartBrainState.Idle;
    public float alertArea;
    public float awarenessArea;
    public float aggresiveArea;

    public SmartBrain(ISmartBrainDelegate brainDelegate) {
        this.brainDelegate = new WeakReference(brainDelegate);
    }

    public void Observe(GameObject target) {
        if (_brainDelegate == null) return; 

        state = DetermineState(target);

        switch (state) {
            case SmartBrainState.Aggresive:
                _brainDelegate.OnAggresive(target);
                break;
            case SmartBrainState.Alert:
                _brainDelegate.OnAlert(target);
                break;
            case SmartBrainState.Aware:
                _brainDelegate.OnAware(target);
                break;
            case SmartBrainState.Fleeing:
                _brainDelegate.OnFleeing(target);
                break;
            case SmartBrainState.Idle:
                _brainDelegate.OnIdle(target);
                break;
        }
    }

    public SmartBrainState DetermineState(GameObject target) {
        if (_brainDelegate == null) return SmartBrainState.Idle;

        SmartBrainState state;

        if (ShouldFlee()) {
            state = SmartBrainState.Fleeing;
        } else if (StatusConversionHelper.IsInsideRange(_brainDelegate.distanceToTarget, StatusConversionHelper.GetActualAIStateArea(aggresiveArea))) {
            state = SmartBrainState.Aggresive;
        } else if (StatusConversionHelper.IsInsideRange(_brainDelegate.distanceToTarget, StatusConversionHelper.GetActualAIStateArea(awarenessArea))) {
            state = SmartBrainState.Aware;
        } else if (StatusConversionHelper.IsInsideRange(_brainDelegate.distanceToTarget, StatusConversionHelper.GetActualAIStateArea(alertArea))) {
            state = SmartBrainState.Alert;
        } else {
            state = SmartBrainState.Idle;
        }

        return state;
    }

    private bool ShouldFlee() {
        if (_brainDelegate == null) return false;

        // TODO:
        // complete this func
        // check if delegate has custom flee logic
        // lazy init?

        bool shouldFlee = false;

        IAttackable attacker = _brainDelegate.objectToBeObserved.GetComponent<IAttackable>();
        IHittable defender = _brainDelegate.body.GetComponent<IHittable>();

        return shouldFlee;
    }
}
