using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTester {
    float func1 = 0;
    float func2 = 0;
    float total = 0;
    float sample = 0;
    float probability = 0;

    public RandomTester(float sample, float probability) {
        this.sample = sample;
        this.probability = probability;
    }

    public void ProbabilityTest() {
        if (total >= sample) return;

        if (RandomHelper.ShouldGotValue(probability)) func1++;
        if (RandomHelper.ShouldGotValue2(probability)) func2++;

        total++;

        float func1Prob = (func1 / total) * 100;
        float func2Prob = (func2 / total) * 100;

        Debug.Log("prob test, total: " + total + ", for: " + probability + "%");

        if (total == sample) {
            Debug.Log(probability + "% probtest for f1: " + func1 + "(" + func1Prob + "%)");
            Debug.Log(probability + "% probtest for f2: " + func2 + "(" + func2Prob + "%)");
        }

        ProbabilityTest();
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

        System.Random random = new System.Random();
        int selectedIndex = random.Next(MIN_LIST, MAX_LIST);

        return selectedIndex <= intProbability;
    }

    public static bool ShouldGotValue2(float chance) {
        if (chance > 100) return true;

        int intProbability = (int)(chance * 10);
        System.Random random = new System.Random();
        int randomVal = random.Next(MIN_LIST, MAX_LIST);

        return randomVal <= intProbability;
    }
}
