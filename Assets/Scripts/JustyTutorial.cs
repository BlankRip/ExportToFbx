using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustyTutorial : MonoBehaviour
{
    private int Solve(int w1, int w2, int w3) {
        int heavyest = int.MinValue;
        int requiredIndx = -1;
        List<int> values = new List<int>() {w1, w2, w3};
        for (int i = 0; i < values.Count; i++) {
            if(values[i] > heavyest) {
                heavyest = values[i];
                requiredIndx = i;
            }
        }
        return requiredIndx;
    }

    public bool Exists(int[] ints, int k)
    {
        foreach (int i in ints) {
            if(i == k)
                return true;
        }
        return false;
    }
}
