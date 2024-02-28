using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockFunction : AfterEffect
{
    [SerializeField] TFunction function;
    public override void ApplyEffect()
    {
        function.UnblockEvaluating();
    }
}
