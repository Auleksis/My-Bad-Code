using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class OutputElement : MonoBehaviour
{
    [SerializeField] protected GameObject uiElement;
    [SerializeField] public TMP_Text text;
    [SerializeField] protected float delay_seconds = 0.06f;
    protected MessageStream currentStream;
    protected DesktopKeycodes desktopKeycodes;

    private void Awake()
    {
        desktopKeycodes = GameObject.Find("GameInput").GetComponent<DesktopKeycodes>();
    }

    public abstract void PrintMessage(Message message);

    public virtual void Show()
    {
        uiElement.SetActive(true);
        StopAllCoroutines();
        if(currentStream != null)
            StartCoroutine(currentStream.CursorCoroutine());
    }

    public virtual void Hide()
    {
        SkipPrinting();
        StopAllCoroutines();
        uiElement.SetActive(false);
    }

    public virtual void SkipPrinting()
    {
        if(currentStream != null)
            currentStream.Skip();
    }

    public bool IsActive()
    {
        return uiElement.activeSelf;
    }

    public bool IsPrinting()
    {
        return currentStream != null && currentStream.IsPrinting();
    }
}
