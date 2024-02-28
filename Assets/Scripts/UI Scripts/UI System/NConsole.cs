using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NConsole : OutputElement
{
    MessageStream messageStream;
    QuestStream questStream;

    [SerializeField] Button messageStreamButton;
    [SerializeField] Button questStreamButton;

    [SerializeField] Color mainColor;
    [SerializeField] Color flashingColor;

    private void Start()
    {
        messageStream = new MessageStream(this, null, delay_seconds);
        questStream = new QuestStream(this, null, delay_seconds);
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(desktopKeycodes.keys[ACTIONS.SKIP]) && uiElement.activeSelf && currentStream != null && currentStream.IsPrinting())
        {
            currentStream.Skip();
        }

        if (Input.GetKeyDown(desktopKeycodes.keys[ACTIONS.SHOW_CONSOLE]))
        {
            if (uiElement.activeSelf) Hide();
            else Show();
        }
    }
    
    public override void PrintMessage(Message message)
    {
        Show();
        if (message.mode == OUTPUT_MODE.MESSAGESTREAM)
        {
            messageStream = new MessageStream(this, message, delay_seconds);
            currentStream = messageStream;

            questStreamButton.interactable = true;
            messageStreamButton.interactable = false;
        }
        if (message.mode == OUTPUT_MODE.QUESTSTREAM)
        {
            questStream = new QuestStream(this, message, delay_seconds);
            currentStream = questStream;

            questStreamButton.interactable = false;
            messageStreamButton.interactable = true;
        }

        currentStream.StartStream();
    }

    public void ShowQuestStream()
    {
        questStreamButton.interactable = false;
        messageStreamButton.interactable = true;

        if (questStream == null)
        {
            text.text = "";
            return;
        }

        currentStream = questStream;
        currentStream.Skip();
    }

    public void ShowMessageStream()
    {

        questStreamButton.interactable = true;
        messageStreamButton.interactable = false;

        if (messageStream == null)
        {
            text.text = "";
            return;
        }

        currentStream = messageStream;
        currentStream.Skip();
    }

    public void FinishQuest()
    {
        if(currentStream != null)
            currentStream.Skip();

        StopAllCoroutines();

        questStreamButton.interactable = false;
        messageStreamButton.interactable = true;

        currentStream = questStream;

        questStream.QuestDone();
    }

    public override void Show()
    {
        base.Show();

        if(currentStream == questStream)
        {
            questStreamButton.interactable = false;
            messageStreamButton.interactable = true;
        }
        else
        {
            questStreamButton.interactable = true;
            messageStreamButton.interactable = false;
        }
    }

    public OUTPUT_MODE GetCurrentMode()
    {
        if (currentStream == null)
            return OUTPUT_MODE.MESSAGESTREAM;

        if (currentStream == messageStream)
            return OUTPUT_MODE.MESSAGESTREAM;
        else
            return OUTPUT_MODE.QUESTSTREAM;
    }

    public void Bug()
    {
        RectTransform canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        RectTransform uiElementTransform = uiElement.GetComponent<RectTransform>();
        uiElementTransform.localPosition = new Vector3(0, canvas.rect.height - uiElementTransform.rect.height, 0);

        text.color = flashingColor;

        text.overflowMode = TMPro.TextOverflowModes.Overflow;
    }

    public void DeBug()
    {
        RectTransform uiElementTransform = uiElement.GetComponent<RectTransform>();
        uiElementTransform.localPosition = new Vector3(0, 0, 0);

        text.color = mainColor;

        text.overflowMode = TMPro.TextOverflowModes.Page;
    }

    public int GetPageCount()
    {
        return text.textInfo.pageCount;
    }
}
