using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Justy : MonoBehaviour
{
    string s;
    Tuple<int, int> thisIS;
    void Start()
    {
        print(s);
    }

    private void ReadONlyTest(in int test) {

    }
    private static List<int> a = new List<int>();
    private static List<int> b = new List<int>();
    private static List<int> c = new List<int>();
    private static int aIndex;
    private static int bIndex;
    private static int cIndex;

    public static string[] Solve(int[] boxesA, int[] boxesB, int[] boxesC)
    {
        foreach (int box in boxesA)
            a.Add(box);
        foreach (int box in boxesB)
            b.Add(box);
        foreach (int box in boxesC)
            c.Add(box);

        int aIndex = a.Count - 1;
        int bIndex = b.Count - 1;
        int cIndex = c.Count - 1;
        List<int> currentTop = new List<int>();
        for (int i = aIndex; i < a.Count; aIndex--) {
            FillCurrentTop(ref currentTop);
            int heavyestOn = GetHeaviestIndex(currentTop);
            //Cause of time constarain I could not figure it out and finsh it
            //Thank you
            i = aIndex;
        }
        
        return null;
    }

    private static int GetHeaviestIndex(List<int> theList) {
        int heavyiest = int.MinValue;
        int heavyIndex = -1;
        for (int i = 0; i < theList.Count; i++) {
            if(theList[i] > heavyiest) {
                heavyiest = theList[i];
                heavyIndex = i;
            }
        }
        return heavyIndex;
    }

    private static void FillCurrentTop(ref List<int> theList) {
        theList.Clear();
        theList.Add(a[aIndex]);
        theList.Add(b[bIndex]);
        theList.Add(c[cIndex]);
    }


    private static int count = 0;

    /// Increments count in a thread-safe​​​​​​‌​​‌​‌‌‌‌​​‌​‌‌‌‌‌‌​​​​‌​ manner.
	public static int Increment()
    {
        return Interlocked.Increment(ref count);
	}
}

public abstract class Teser
{
    public void Methord1() {
        Debug.Log("Values");
    }
}
