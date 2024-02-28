using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    public string sender;
    [Multiline]
    public string text;
    public Sprite sprite;
    public OUTPUT_MODE mode;

    public Message(string sender, string text, Sprite sprite, OUTPUT_MODE mode)
    {
        this.sender = sender;
        this.text = text;
        this.sprite = sprite;
        this.mode = mode;
    }

    public void PasteKeys()
    {
        DesktopKeycodes keycodes = GameObject.Find("GameInput").GetComponent<DesktopKeycodes>();
        text = keycodes.PasteDescription(text);
    }
}
