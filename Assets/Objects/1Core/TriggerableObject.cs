using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableObject : SpawnableObject {

    private GameObject parent;
    private ITriggerableParent triggerableParent {
        get { return parent.GetComponent<ITriggerableParent>(); }
    }

    public override void DoOnTriggerEnter(Collider other) {
        base.DoOnTriggerEnter(other);

        triggerableParent.TriggerDetect(gameObject, other.gameObject);
        Debug.Log("triggreable hit something");
    }
}
