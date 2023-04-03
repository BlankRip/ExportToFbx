using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Just3: MonoBehaviour
{
    [SerializeField] string str;
    private void Something() {
        string st1 = "fro";
        string st2 = "tro";
        if(str.Contains(st1)) {
            
        }
        str.Replace(st1, st2);
    }

    private void StringNullOrEmptyExceptions(string str) {
        if(str == "")
            throw new ArgumentException($"{str.ToString()} is empty");
        if(str == null)
            throw new ArgumentNullException($"{str.ToString()} is null");
    }
}
