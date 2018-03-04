using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerableParent {
    void TriggerDetect(GameObject child, GameObject target);
}
