using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIONS { UP, DOWN, RIGHT, LEFT, INTERACT, SKIP, GO_ON, SHOW_NOTIFICATION, SHOW_CONSOLE, SHOW_WINDOW, CHANGE_TO, CHANGE_BACK }

public class DesktopKeycodes : MonoBehaviour
{
    public Dictionary<ACTIONS, KeyCode> keys;
    public ActionItem[] action_keys;
    public ActionDescription[] action_descriptions;

    private void Awake()
    {
        keys = new Dictionary<ACTIONS, KeyCode>();
        foreach(ActionItem item in action_keys)
        {
            keys.Add(item.action, item.keyCode);
        }
    }

    public string PasteDescription(string text)
    {
        foreach(ActionDescription description in action_descriptions)
        {
            if (text.Contains(description.description))
            {
                text = text.Replace(description.description, "{0}");
                text = string.Format(text, keys[description.action].ToString());
            }
        }
        return text;
    }
}
