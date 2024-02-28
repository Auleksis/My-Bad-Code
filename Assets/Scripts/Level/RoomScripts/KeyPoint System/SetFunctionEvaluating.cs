using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFunctionEvaluating : AfterEffect
{
    [SerializeField] TFunction function;
    [SerializeField] bool doEvaluate = true;
    public override void ApplyEffect()
    {
        function.SetEvaluating(doEvaluate);
    }

    private void Start()
    {
        Debug.Log(transform.position);
    }
}
