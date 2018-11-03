using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgresionFlags
{
    public event Action<string> onFlagAdded = delegate { };

    HashSet<string> flagHashSet = new HashSet<string>();

    public void AddFlag(string flagName)
    {
        if (!HasFlag(flagName))
        {
            flagHashSet.Add(flagName);
            onFlagAdded(flagName);
        }
    }

    public bool HasFlag(string flagName)
    {
        return flagHashSet.Contains(flagName);
    }
}
