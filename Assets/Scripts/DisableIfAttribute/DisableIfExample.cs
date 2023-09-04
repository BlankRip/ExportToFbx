using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blank.Attributes;

public class DisableIfExample : MonoBehaviour
{
    [SerializeField]
    bool hide3Ints = true;
    [SerializeField] [DisableIf("hide3Ints", false, ComparisonType.Equals, DisablingType.DontDraw)]
    int int1, int2, int3;

    [Space]
    [SerializeField]
    bool disableNextString = true;
    [SerializeField]
    [DisableIf("disableNextString", true, ComparisonType.Equals, DisablingType.ReadOnly)]
    string nextString;

}
