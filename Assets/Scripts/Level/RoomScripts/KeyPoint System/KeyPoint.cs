using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyPoint : MonoBehaviour
{
    [SerializeField] WindowTextItem windowText;
    [SerializeField] ConsoleTextItem consoleText;

    [SerializeField] Message message;

    [SerializeField] bool doesItFinishRoom = false;

    [SerializeField] BaseRoom room;

    private bool isAchieved = false;

    public void Achieve()
    {
        //DEPRECATED
        /*if (!windowText.text.Equals(""))
        {
            room.GetUIManager().SetWindowText(windowText.text, windowText.addToExistingText);
        }*/

        /*if (!consoleText.text.Equals(""))
        {
            room.GetUIManager().SetConsoleText(consoleText.text, consoleText.addToExistingText, consoleText.mode);
        }*/

        /*if (consoleText.clearConsole)
        {
            room.GetUIManager().ClearConsole(CONSOLE_MODE.OUTPUT);
            room.GetUIManager().ClearConsole(CONSOLE_MODE.COMPUTER);
        }

        if (consoleText.textItems.Length > 0)
        {
            room.GetUIManager().SetConsoleTextArray(consoleText.textItems);
        }*/
        //DEPRECATED_END

        room.GetUIManager().PrintMessage(message);


        isAchieved = true;

        if (isAchieved && doesItFinishRoom) room.FinishRoom();
    }

    public bool IsAchieved()
    {
        return isAchieved;
    }
}
