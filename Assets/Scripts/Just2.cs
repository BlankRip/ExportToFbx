using System;
using System.Collections;
using System.Collections.Generic;

public class Just2
{
    // KeyValuePair<string, int> thePair = new KeyValuePair<string, int>();
    Dictionary<string, int> thePair = new Dictionary<string, int>();

    public int Count {
        get {
            return thePair.Count;
        }
    }

    public int DefaultValue { get; set; }

    private void SomeFunction() {
        int count = thePair.Count;

        bool vlaueOverride = false;
        if(thePair.ContainsKey("1")) {
            vlaueOverride = true;
            thePair["1"] = 1;
        } else {
            thePair.Add("1", 1);
        }

        bool removed = false;
        if(thePair.ContainsKey("1")) {
            thePair.Remove("1");
            removed = true;
        }

        if(thePair.ContainsKey("1"))
            count = thePair["1"];
        else
            count = DefaultValue;
    }

    private void KeyBasedExceptions(string key) {
        if(key == "")
                throw new ArgumentException("The key is a empty string, please pass a value");
        if(string.IsNullOrEmpty(key))
            throw new ArgumentNullException("The key is null");
    }
}
