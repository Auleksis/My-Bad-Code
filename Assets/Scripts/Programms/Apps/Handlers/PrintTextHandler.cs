using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintTextHandler : AbstractHandler
{
    [SerializeField] PrintDescription[] descriptions;
    
    private UIManager uiManager;

    private string handlerName;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        handlerName = GetComponent<AbstractProgramm>().GetProgrammName();
    }

    public override void Handle(AbstractProgramm callingProgramm)
    {
        foreach(PrintDescription desc in descriptions)
        {
            if(desc.expectedProgrammStateOwner.GetOriginalStateInfo().EqualsToState(callingProgramm.GetStateInfo(), desc.compareType))
            {
                Message message = new Message(handlerName, desc.description, null, desc.outputMode);
                uiManager.PrintMessage(message);
            }
        }
    }
}
