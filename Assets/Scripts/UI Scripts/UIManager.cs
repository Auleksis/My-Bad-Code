using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum OUTPUT_MODE { MESSAGESTREAM, QUESTSTREAM, WINDOW, NOTIFICATION};
public class UIManager : MonoBehaviour
{
    [SerializeField] private Window window;
    [SerializeField] private NConsole nConsole;
    [SerializeField] private NotificationWindow notificationWindow;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void CloseWindow() 
    {
        window.Hide();
    }

    public void FinishQuest()
    {
        nConsole.FinishQuest();
    }

    public void HighlightNoticication()
    {
        notificationWindow.HighlightText();
    }

    public OUTPUT_MODE GetConsoleOutputMode()
    {
        return nConsole.GetCurrentMode();
    }

    public void PrintMessage(Message message)
    {
        if (message.text.Equals(""))
            return;

        message.PasteKeys();

        switch (message.mode)
        {
            case OUTPUT_MODE.MESSAGESTREAM:
            case OUTPUT_MODE.QUESTSTREAM:
                nConsole.PrintMessage(message);
                break;

            case OUTPUT_MODE.WINDOW:
                window.PrintMessage(message);
                break;

            case OUTPUT_MODE.NOTIFICATION:
                notificationWindow.PrintMessage(message);
                break;
        }
    }

    public void HideOutput(OUTPUT_MODE mode)
    {
        switch (mode)
        {
            case OUTPUT_MODE.MESSAGESTREAM:
            case OUTPUT_MODE.QUESTSTREAM:
                nConsole.Hide();
                break;

            case OUTPUT_MODE.WINDOW:
                window.Hide();
                break;

            case OUTPUT_MODE.NOTIFICATION:
                notificationWindow.Hide();
                break;
        }
    }

    public bool IsOutputActive(OUTPUT_MODE mode)
    {
        switch (mode)
        {
            case OUTPUT_MODE.MESSAGESTREAM:
            case OUTPUT_MODE.QUESTSTREAM:
                return nConsole.IsActive();

            case OUTPUT_MODE.WINDOW:
                return window.IsActive();

            case OUTPUT_MODE.NOTIFICATION:
                return notificationWindow.IsActive();
        }

        return false;
    }

    public bool IsOutputPrinting(OUTPUT_MODE mode)
    {
        switch (mode)
        {
            case OUTPUT_MODE.QUESTSTREAM:
            case OUTPUT_MODE.MESSAGESTREAM:
                return nConsole.IsPrinting();
            case OUTPUT_MODE.WINDOW:
                return window.IsPrinting();
            case OUTPUT_MODE.NOTIFICATION:
                return notificationWindow.IsPrinting();
        }
        return false;
    }

    public NConsole GetConsole()
    {
        return nConsole;
    }

    public Window GetWindow()
    {
        return window;
    }

    public NotificationWindow GetNotificationWindow()
    {
        return notificationWindow;
    }
}
