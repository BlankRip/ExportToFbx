using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blank.Attributes;

public class DisableIfExample : MonoBehaviour
{
    [SerializeField]
    bool hide3Ints = true;
    [SerializeField] [DisableIf("hide3Ints", true, ComparisonType.Equals, DisablingType.DontDraw)]
    int int1, int2, int3;

    [Space]
    [SerializeField]
    bool disableNextString = true;
    [SerializeField]
    [DisableIf("disableNextString", true, ComparisonType.Equals, DisablingType.ReadOnly)]
    string nextString;

    [Space]
    [SerializeField] [Range(0, 100)]
    int showFloatIfGreaterThan50 = 30;
    [SerializeField] [DisableIf("showFloatIfGreaterThan50", 50, ComparisonType.LessThan, DisablingType.DontDraw)]
    float greaterThan50Float = 6.9f;

    [Space]
    [SerializeField] [Range(100, 250)]
    float disableIntBelowIfLessThanEqual150 = 220.0f;
    [SerializeField] [DisableIf("disableIntBelowIfLessThanEqual150", 150.0f, ComparisonType.LessOrEqual, DisablingType.ReadOnly)]
    int lessThan150NotAllowedInt;

}
