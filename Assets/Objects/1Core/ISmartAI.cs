using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SmartAIState {
    Aware, Aggresive, Fleeing, Alert, Idle
}

public interface ISmartAI { // AI Interface
    float alertArea { get; set; } // notice something nearby
    float awarenessArea { get; set; } // see object nearby
    float aggresiveArea { get; set; } // pursue object nearby

    SmartAIState DetermineState(GameObject target);
}
