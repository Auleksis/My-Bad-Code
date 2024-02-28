using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAppActivator : KeyPointAbstractActivator
{
    [SerializeField] App app;

    public override bool CheckToActivate()
    {
        if (app.interacted)
        {
            app.interacted = false;
            return true;
        }
        else
            return false;
    }
}
