using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerConsole : MonoBehaviour
{
    [SerializeField] protected GameObject console;

    [SerializeField] protected TMP_Text text;
    [SerializeField] protected float delay_seconds = 0.1f;
    [SerializeField] protected Image cursor;

    protected string text_string;

    protected float start_y = 0f;

    protected int lastCharIndex = 0;

    [SerializeField] protected bool isShown = false;
    [SerializeField] protected bool isShownAtStart = true;

    protected bool skip = false;

    protected Vector3 startCursorPosition = new Vector3(0, 0, -1);

    protected bool isPrinting = false;

    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        /*if (isShown && isActive())
        {
            SetCursorAfterLastChar();
        }*/

        if (Input.GetKeyDown(KeyCode.Space) && console.activeSelf && isPrinting)
        {
            skip = true;
        }
    }

    public void Show()
    {
        console.SetActive(true);

        isShown = true;
        //text_string = text.text;
        //text.text = "";

        //cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 1);

        StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        isPrinting = true;

        for (int i = lastCharIndex; i < text_string.Length; i++)
        {
            /*if (!skip) { 
                cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 1);
                yield return new WaitForSeconds(0.05f);
                cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 0);
            }*/

            if (text.text.Length > 1 && text.text.Contains('\u0370'))
                text.text = text.text.Substring(0, text.text.Length - 2) + text_string[i] + " " + '\u0370';
            else
                text.text += text_string[i].ToString() + " " + '\u0370';

            if (!skip)
            {
                yield return new WaitForSeconds(delay_seconds);
            }

            lastCharIndex = i + 1;
        }

        skip = false;

        isPrinting = false;

        text.ForceMeshUpdate();
        text.pageToDisplay = text.textInfo.pageCount;

        //SetCursorAfterLastChar();

        StartCoroutine(CursorCoroutine());
    }

    IEnumerator CursorCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            //cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 1);
            if(text.text.Contains(" " + '\u0370'))
                text.text = text.text.Substring(0, text.text.Length - 2);
            yield return new WaitForSeconds(0.5f);
            //cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 0);
            text.text += " " + '\u0370';
        }
    }

    //window parametr can be used overriding this method in other classes
    public void SetTextAndShow(string text, bool addText)
    {
        if (!isShown)
        {
            StopAllCoroutines();
            if (!addText)
            {
                text_string = text;
                this.text.text = "";
                lastCharIndex = 0;
            }
            else
            {
                this.text.text = text_string;
                lastCharIndex = text_string.Length;
                text_string += text;
            }

            int beforeForced = this.text.text.Length;

            this.text.text = this.text.text;

            Show();
        }
    }

    public void ShowWithExistingText()
    {
        console.SetActive(true);
        StartCoroutine(CursorCoroutine());
    }

    public bool isActive()
    {
        return console.activeSelf;
    }

    public void Close()
    {
        if (!isPrinting)
        {
            StopAllCoroutines();
            console.SetActive(false);
        }
    }

    public virtual void SetCursorAfterLastChar()
    {
        RectTransform cursorTransform = cursor.GetComponent<RectTransform>();

        if (text.textInfo.characterInfo.Length > 0 && text.text.Length > 0)
        {
            float lineHeight = text.textInfo.lineInfo[0].lineHeight;
            Vector3 p1;
            Vector3 p2;

            p1 = text.textInfo.characterInfo[text.textInfo.characterInfo.Length - 1].topRight;
            p2 = text.textInfo.characterInfo[text.textInfo.characterInfo.Length - 1].bottomLeft;

            Vector3 diff = p1 - p2;

            cursorTransform.GetComponent<RectTransform>().localPosition = new Vector3(p1.x + 10f, start_y - (lineHeight * (text.textInfo.lineCount - 1)));
        }
        else
        {
            cursorTransform.localPosition = startCursorPosition;
        }
    }

    public bool IsPrinting()
    {
        return isPrinting;
    }
}
