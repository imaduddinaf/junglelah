﻿using System.Collections;
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

    public static void Destroy<T>(this T obj) where T : UnityEngine.Object {
        UnityEngine.Object.Destroy(obj);
    }

    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;

        while (n > 1) {
            n--;
            System.Random random = new System.Random();
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
