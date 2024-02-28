using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CONSOLE_MODE { COMPUTER, OUTPUT };
public class OldUIManager : MonoBehaviour
{
    [SerializeField] private ComputerConsole oldWindow;
    [SerializeField] private Console console;

    [SerializeField] private Window window;

    Action<String> windowCloseAction;
    Action<String> consoleCloseAction;

    string consoleText;
    string windowText;

    CONSOLE_MODE consoleStartMode;

    bool toShowWindow = false;
    bool toShowConsole = false;
    bool toAddConsoleText = false;
    bool toAddComputerText = false;

    Queue<TextItem> consoleMessageQueue;
    bool toShowConsoleMessageArray = false;
    TextItem currentMessage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (toShowConsole)
        {
            if (!toShowWindow && !isWindowActive())
                toShowConsole = false;
            if (!toShowConsole && !isWindowActive())
            {
                console.SetDoShowMessageAnyway(true);
                ShowConsole(consoleText, toAddConsoleText, consoleStartMode);
            }
        }

        if (toShowWindow)
        {
            toShowWindow = false;
            ShowWindow(windowText, toAddComputerText);
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isConsoleActive()) CloseConsole();
            else if (!isConsoleActive() && !isWindowActive()) ShowConsole(false, CONSOLE_MODE.OUTPUT);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (toShowConsoleMessageArray && isConsoleActive() && !console.IsPrinting())
            {
                if (consoleMessageQueue.Count > 0)
                {
                    console.SetDoShowMessageAnyway(true);
                    currentMessage = consoleMessageQueue.Dequeue();
                    SetConsoleText(currentMessage.text, currentMessage.addToExistingText, currentMessage.mode);
                }
                else
                {
                    toShowConsoleMessageArray = false;
                }
            }
        }
    }

    /*public void InitForRoom(string consoleText, Action<string> f1, string windowText, Action<string> f2)
    {
*//*        this.windowCloseAction = f1;
        this.consoleCloseAction = f2;*//*
        this.consoleText = consoleText;
        this.windowText = windowText;
    }*/

    public void ShowWindow()
    {
        oldWindow.ShowWithExistingText();
    }

    public void ShowWindow(string text, bool addText)
    {
        windowText = text;
        oldWindow.SetTextAndShow(text, addText);
        console.Close();
    }

    public void ShowConsole(bool playAnim, CONSOLE_MODE mode)
    {
        if (playAnim)
        {
            switch (mode)
            {
                case CONSOLE_MODE.COMPUTER:
                    console.SetComputerText(consoleText, false);
                    console.ShowComputerConsole();
                    break;
                case CONSOLE_MODE.OUTPUT:
                    console.SetOutputText(consoleText, true);
                    console.ShowOutputConsole();
                    break;
            }
        }
        else
        {
            console.SetMode(mode);
            console.ShowWithExistingText();
        }
    }

    public void SetConsoleTextArray(TextItem[] textItems)
    {
        consoleMessageQueue = new Queue<TextItem>(textItems);
        toShowConsoleMessageArray = true;
        if (textItems.Length > 0)
        {
            currentMessage = consoleMessageQueue.Dequeue();
            SetConsoleText(currentMessage.text, currentMessage.addToExistingText, currentMessage.mode);
        }
    }

    public void SetConsoleText(string text, bool add, CONSOLE_MODE mode)
    {
        consoleText = text;
        consoleStartMode = mode;
        toShowConsole = true;
        toAddConsoleText = add;
    }

    public void SetWindowText(string text, bool add)
    {
        windowText = text;
        toShowWindow = true;
        toAddComputerText = add;
    }

    public void ShowConsole(string text, bool addText, CONSOLE_MODE mode)
    {
        consoleText = text;
        //console.SetDoShowMessageAnyway(true);
        switch (mode)
        {
            case CONSOLE_MODE.COMPUTER:
                console.SetComputerText(text, addText);
                console.ShowComputerConsole();
                break;
            case CONSOLE_MODE.OUTPUT:
                console.SetOutputText(text, addText);
                console.ShowOutputConsole();
                break;
        }
    }

    public bool isWindowActive()
    {
        return oldWindow.isActive();
    }

    public bool isConsoleActive()
    {
        return console.isActive();
    }

    public void CloseWindow()
    {
        oldWindow.Close();
        /*if(windowCloseAction != null)
            windowCloseAction(paramForConsoleAfterClosingWindow);*/
    }

    public void CloseConsole()
    {
        console.Close();
        /*if(consoleCloseAction != null)
            consoleCloseAction(paramForWindowAfterClosingConsole);*/
    }

    public void ClearConsole(CONSOLE_MODE mode)
    {
        console.ClearConsole(mode);
    }

    public void PrintMessage(Message message)
    {
        switch (message.mode)
        {
            case OUTPUT_MODE.MESSAGESTREAM:
                break;

            case OUTPUT_MODE.QUESTSTREAM:
                break;

            case OUTPUT_MODE.WINDOW:
                window.PrintMessage(message);
                break;

            case OUTPUT_MODE.NOTIFICATION:
                break;
        }
    }
}
