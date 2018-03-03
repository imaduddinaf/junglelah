using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour {

    void Awake() {
        DoOnAwake();
    }

    void Start() {
        DoOnStart();
    }

    void Update() {
        DoOnUpdate();
    }

    void FixedUpdate() {
        DoOnFixedUpdate();
    }

    void OnTriggerEnter(Collider other) {
        DoOnTriggerEnter(other);
    }

    public virtual void DoOnAwake() {
        // empty
    }

    public virtual void DoOnStart() {
        // empty
    }

    public virtual void DoOnUpdate() {
        // empty
    }

    public virtual void DoOnFixedUpdate() {
        // empty
    }

    public virtual void DoOnTriggerEnter(Collider other) {
        // empty
    }
}

public abstract class SpawnableObject : BaseMonoBehaviour, ITriggerableParent {

    public Rigidbody2D rigidBody {
        get { return gameObject.GetComponent<Rigidbody2D>(); }
    }

    public void TriggerDetect(GameObject child, GameObject target) {
        TriggerDetectOnChild(child, target);
    }

    public virtual void TriggerDetectOnChild(GameObject child, GameObject target) {
        // empty
    }
}
