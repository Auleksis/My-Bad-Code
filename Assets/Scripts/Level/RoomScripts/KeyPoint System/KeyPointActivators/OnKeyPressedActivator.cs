using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyPressedActivator : KeyPointAbstractActivator
{
    [SerializeField] ACTIONS key = ACTIONS.GO_ON;
    private DesktopKeycodes desktopKeycodes;

    private void Awake()
    {
        desktopKeycodes = GameObject.Find("GameInput").GetComponent<DesktopKeycodes>();
    }

    public override bool CheckToActivate()
    {
        if (Input.GetKeyDown(desktopKeycodes.keys[key]))
            return true;
        return false;
    }
}
