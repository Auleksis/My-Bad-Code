using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppWrapper : AbstractProgramm
{
    private AbstractHandler[] handlers;

    protected bool canMove = true;

    protected override void Start()
    {
        base.Start();
        handlers = GetComponents<AbstractHandler>();
    }

    public void CallHandlers(AbstractProgramm callingProgramm)
    {
        foreach (AbstractHandler handler in handlers)
        {
            handler.Handle(callingProgramm);
        }
    }

    public override StateInfo GetOriginalStateInfo()
    {
        return stateInfo;
    }

    public override void RequestToStop()
    {
        canMove = false;
    }
}
