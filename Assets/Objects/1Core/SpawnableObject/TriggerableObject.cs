using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableObject : SpawnableObject {

    private GameObject parent {
        get { return gameObject.transform.parent.gameObject; }
    }
    private ITriggerableParent triggerableParent {
        get { return parent.GetComponent<ITriggerableParent>(); }
    }

    public override void DoOnTriggerEnter(Collider2D other) {
        base.DoOnTriggerEnter(other);

        triggerableParent.TriggerDetect(gameObject, other.gameObject);
    }
}
