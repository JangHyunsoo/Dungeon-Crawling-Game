using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat
{
    public virtual void init() { }

    public virtual float getValue() { return 0f; }
}
