using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFunction : MonoBehaviour
{
    public BitElement[] bits;

    public TObjectElement[] targetObjects;

    public BaseRoom room;

    protected int currentValue = 0;

    [SerializeField] bool printsToConsoleItsValue = false;
    [SerializeField] bool evaluatingIsBlocked = false;

    [SerializeField] protected string outputFormatString = "Function: current value = {0}";

    //[SerializeField] CONSOLE_MODE outputMode = CONSOLE_MODE.OUTPUT;

    public virtual void SetBit(bool value, Trigger bit)
    {
        int temp = 1 << Array.FindIndex(bits, element => element.bit == bit);

        if (value) currentValue |= temp;
        else if(!value && (currentValue & temp) == temp) currentValue ^= temp;

        if(!evaluatingIsBlocked)
            Process();
    }

    protected void Process()
    {
        //Сначала всё вернём в стандартное состояние, а уже затем будем активировать
        /*foreach(TObjectElement t in targetObjects)
        {
            t.tObject.Disable();
        }


        foreach(TObjectElement t in targetObjects)
        {
            if (currentValue == t.targetValue)
                t.tObject.Enable();
        }*/
        if (printsToConsoleItsValue)
        {
            /*room.WriteToConsole(string.Format(outputFormatString, currentValue), outputMode);*/
            Message message = new Message("", string.Format(outputFormatString, currentValue), null, OUTPUT_MODE.NOTIFICATION);
            room.GetUIManager().PrintMessage(message);
        }

        foreach (TObjectElement t in targetObjects)
        {
            foreach (Trigger trigger in t.tObjects)
                trigger.Disable();
        }

        foreach (TObjectElement t in targetObjects)
        {
            if (!t.requiresOnlyDefinedValue && (currentValue & t.targetValueIncludesValue) == t.targetValueIncludesValue || t.requiresOnlyDefinedValue && currentValue == t.targetValueIncludesValue)
            {
                foreach (Trigger trigger in t.tObjects)
                    trigger.Use();
                /*if(!t.specialOutputText.Equals(""))
                    room.WriteToConsole(t.specialOutputText, t.outputMode);*/
                room.GetUIManager().PrintMessage(t.message);
            }
        }
    }

    public void BlockFunctionOutput()
    {
        printsToConsoleItsValue = false;
    }

    public void UnblockFunctionOutput()
    {
        printsToConsoleItsValue = true;
    }

    public void DoPrintOutput(bool value)
    {
        printsToConsoleItsValue = value;
    }

    public void SetEvaluating(bool value)
    {
        evaluatingIsBlocked = !value;
    }

    public void BlockEvaluating()
    {
        evaluatingIsBlocked = true;
    }

    public void UnblockEvaluating()
    {
        evaluatingIsBlocked = false;
    }

    public int GetCurrentValue()
    {
        return currentValue;
    }
}
