using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blank.Attributes;

public class ReadOnlyExample : MonoBehaviour
{
    [ReadOnly, SerializeField] string str = "This is a string";
    [ReadOnly, SerializeField] int intiger = 75;
    [ReadOnly, SerializeField] Material material;
    [ReadOnly, SerializeField] List<float> floatList = new List<float>();
}
