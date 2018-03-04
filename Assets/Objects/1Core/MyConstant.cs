using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tuple<T, S> {
    public T first;
    public S last;

    public Tuple(T first, S last): this() {
        this.first = first;
        this.last = last;
    }
}

public class MyConstant {

    public class Tag {
        public static string HAND = "Hand";
        public static string HIT_AREA = "HitArea";
        public static string PLAYER = "Player";
    }
    
}

public static class MyExtensions { 

}
