using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISmartAI { // AI Interface
    GameObject objectToBeObserved { get; set; } // enemy 

    float alertArea { get; set; } // notice something nearby
    float awarenessArea { get; set; } // see object nearby
    float aggresiveArea { get; set; } // pursue object nearby

    SmartBrain brain { get; set; }
}
