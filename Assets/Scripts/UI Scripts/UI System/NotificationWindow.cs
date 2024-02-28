using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationWindow : OutputElement
{
    [SerializeField] CanvasRenderer viewport;

    private bool isHidden = true;

    [SerializeField] float fadeTimeSeconds = 2f;
    [SerializeField] float timeBeforeFadingSeconds = 3f;
    [SerializeField] Color mainColor;
    [SerializeField] Color flashingColor;
    [SerializeField] float flashTimeSeconds = 0.3f;
    [SerializeField] int flashCount = 6;

    private void Update()
    {
        if(!isHidden && (currentStream != null && !currentStream.IsPrinting() || currentStream == null))
        {
            StartCoroutine(WaitBeforeFading());
            isHidden = true;
        }

        if (Input.GetKeyDown(desktopKeycodes.keys[ACTIONS.SHOW_NOTIFICATION]) && isHidden)
        {
            Show();
        }
    }

    public override void PrintMessage(Message message)
    {
        Show();
        text.color = mainColor;
        currentStream = new MessageStream(this, message, delay_seconds);
        currentStream.StartStream();
        isHidden = false;
    }

    public override void Show()
    {
        base.Show();
        isHidden = false;
        viewport.SetAlpha(1.0f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    }

    public override void Hide()
    {
        //viewport.SetAlpha(1.0f);
        base.Hide();
    }

    public void HighlightText()
    {
        StartCoroutine(FlashingText());
    }

    IEnumerator FlashingText()
    {
        for (int i = 0; i < flashCount; i++)
        {
            text.color = flashingColor;
            yield return new WaitForSeconds(flashTimeSeconds);
            text.color = mainColor;
            yield return new WaitForSeconds(flashTimeSeconds);
        }
        text.color = mainColor;
    }

    IEnumerator WaitBeforeFading()
    {
        yield return new WaitForSeconds(timeBeforeFadingSeconds);
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;

        float t = 0f;

        while(elapsedTime < fadeTimeSeconds)
        {
            elapsedTime += Time.deltaTime;
            t = Mathf.Lerp(1, 0, elapsedTime / fadeTimeSeconds);
            viewport.SetAlpha(t);
            text.GetComponent<CanvasRenderer>().SetAlpha(t);
            yield return null;
        }

        Hide();
    }
}
