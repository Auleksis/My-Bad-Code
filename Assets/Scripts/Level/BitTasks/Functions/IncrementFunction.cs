using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementFunction : TFunction
{
    public override void SetBit(bool value, Trigger bit)
    {
        if (value) currentValue++;
        else currentValue--;

        Process();
    }
}
