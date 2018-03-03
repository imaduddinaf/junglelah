using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState {
    Aware, Aggresive, Fleeing, Alert, Idle
}

public interface ISmartAI { // AI Interface
    float awarenessArea { get; set; }
    float aggresiveArea { get; set; }
}
