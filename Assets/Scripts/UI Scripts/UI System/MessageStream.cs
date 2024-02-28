using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class MessageStream
{
    protected bool isPrinting;
    protected Message message;
    protected bool skip;
    protected float delay_seconds;

    protected string text_string;

    protected OutputElement uiOwner;

    public MessageStream(OutputElement uiOwner, Message message, float delay_seconds)
    {
        if (message == null)
            message = new Message("", "", null, OUTPUT_MODE.MESSAGESTREAM);

        this.uiOwner = uiOwner;
        this.message = message;
        this.delay_seconds = delay_seconds;

        text_string = "";
        if (!message.sender.Equals(""))
            text_string += message.sender + ": ";
        text_string += message.text;

        uiOwner.StopAllCoroutines();
        uiOwner.text.text = "";
    }

    public void StartStream()
    {
        isPrinting = true;
        uiOwner.StartCoroutine(TextCoroutine());
    }

    public void Skip()
    {
        uiOwner.StopAllCoroutines();
        
        skip = true;

        isPrinting = false;

        uiOwner.text.text = text_string;

        uiOwner.text.ForceMeshUpdate();
        uiOwner.text.pageToDisplay = uiOwner.text.textInfo.pageCount;

        uiOwner.StartCoroutine(CursorCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        for (int i = 0; i < text_string.Length; i++)
        {
            if (uiOwner.text.text.Length > 1 && uiOwner.text.text.Contains('\u0370'))
                uiOwner.text.text = uiOwner.text.text.Substring(0, uiOwner.text.text.Length - 2) + text_string[i] + " " + '\u0370';
            else
                uiOwner.text.text += text_string[i].ToString() + " " + '\u0370';

            uiOwner.text.pageToDisplay = uiOwner.text.textInfo.pageCount;

            yield return new WaitForSeconds(delay_seconds);
        }

        isPrinting = false;

        uiOwner.text.ForceMeshUpdate();
        if(uiOwner.text.textInfo != null)
            uiOwner.text.pageToDisplay = uiOwner.text.textInfo.pageCount;

        uiOwner.StartCoroutine(CursorCoroutine());
    }

    public IEnumerator CursorCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (uiOwner.text.text.Contains(" " + '\u0370'))
                uiOwner.text.text = uiOwner.text.text.Substring(0, uiOwner.text.text.Length - 2);

            yield return new WaitForSeconds(0.5f);

            uiOwner.text.text += " " + '\u0370';
        }
    }

    public bool IsPrinting()
    {
        return isPrinting;
    }
}
