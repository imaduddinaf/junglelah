using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTester {
    float func1 = 0;
    float func2 = 0;
    float total = 0;
    float MAX_SAMPLE = 10000;

    public void ProbabilityTest(float probability) {
        if (total >= MAX_SAMPLE) return;

        if (RandomHelper.ShouldGotValue(probability)) func1++;
        if (RandomHelper.ShouldGotValue2(probability)) func2++;

        total++;

        float func1Prob = (func1 / total) * 100;
        float func2Prob = (func2 / total) * 100;

        Debug.Log("prob test, total: " + total + ", for: " + probability + "%");
        Debug.Log("prob for f1: " + func1 + "(" + func1Prob + "%)");
        Debug.Log("prob for f2: " + func2 + "(" + func2Prob + "%)");
    }
}

public static class RandomHelper {

    public static int MIN_LIST = 1;
    public static int MAX_LIST = 1000;

    public static List<int> GetInts(int amount) {
        List<int> ints = new List<int>();

        return ints;
    }

    public static List<int> aThousandInts = GetInts(1000);

    public static bool ShouldGotValue(float chance) {
        if (chance > 100) return true;
        // can only handles probability until 1 digit after comma ',' (xx.x%)
        
        int intProbability = (int)(chance * 10);
        List<int> indexes = aThousandInts;

        indexes.Shuffle<int>();

        int selectedIndex = UnityEngine.Random.Range(MIN_LIST, MAX_LIST);

        return selectedIndex <= intProbability;
    }

    public static bool ShouldGotValue2(float chance) {
        if (chance > 100) return true;

        int intProbability = (int)(chance * 10);
        int random = UnityEngine.Random.Range(MIN_LIST, MAX_LIST);

        return random <= intProbability;
    }
}
