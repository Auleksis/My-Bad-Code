using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window : OutputElement
{
    [SerializeField] Button closeButton;
    void Update()
    {
        if (Input.GetKeyDown(desktopKeycodes.keys[ACTIONS.SKIP]) && uiElement.activeSelf && currentStream != null && currentStream.IsPrinting())
        {
            currentStream.Skip();
        }

        if (Input.GetKeyDown(desktopKeycodes.keys[ACTIONS.SHOW_WINDOW]))
        {
            if (uiElement.activeSelf) Hide();
            else Show();
        }
    }

    public override void PrintMessage(Message message)
    {
        Show();
        currentStream = new MessageStream(this, message, delay_seconds);
        currentStream.StartStream();
    }

    public override void Hide()
    {
        closeButton.interactable = false;
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
        closeButton.interactable = true;
    }
}
