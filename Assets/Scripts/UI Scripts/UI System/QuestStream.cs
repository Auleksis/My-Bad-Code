using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStream : MessageStream
{
    public QuestStream(OutputElement uiOwner, Message message, float delay_seconds) : base(uiOwner, message, delay_seconds)
    {
        if(!this.message.text.Equals(""))
            text_string += "\nСтатус: не выполнено";
    }

    public void QuestDone()
    {
        text_string = text_string.Remove(text_string.LastIndexOf('\n')) + "\nСтатус: выполнено";
        uiOwner.text.text = text_string;

        uiOwner.text.ForceMeshUpdate();
        uiOwner.text.pageToDisplay = uiOwner.text.textInfo.pageCount;

        uiOwner.StartCoroutine(CursorCoroutine());
    }
}
