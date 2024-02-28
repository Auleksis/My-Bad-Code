using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] float delay_seconds = 0.1f;
    [SerializeField] Image cursor;

    private string text_string;


    float start_y;

    private void Start()
    {
        text = GetComponent<TMP_Text>();

        start_y = cursor.GetComponent<RectTransform>().localPosition.y;

        Show();
    }

    private void Update()
    {
        float lineHeight = text.textInfo.lineInfo[0].lineHeight;
        Vector3 p1;
        Vector3 p2;

        p1 = text.textInfo.characterInfo[text.textInfo.characterInfo.Length - 1].topRight;
        p2 = text.textInfo.characterInfo[text.textInfo.characterInfo.Length - 1].bottomLeft;

        Vector3 diff = p1 - p2;


        RectTransform cursorTransform = cursor.GetComponent<RectTransform>();

        if (text.textInfo.characterInfo.Length > 0)
        {
            cursorTransform.localPosition = new Vector3(p1.x + 10f, start_y - (lineHeight * (text.textInfo.lineCount - 1)));
        }
    }

    public void Show()
    {
        text_string = text.text;
        text.text = "";
        StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        foreach (char c in text_string)
        {
            cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 1);
            yield return new WaitForSeconds(0.05f);
            cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 0);

            text.text += c;

            //cursor.GetComponent<RectTransform>().localPosition += new Vector3(diff.x, 0, 0);
            yield return new WaitForSeconds(delay_seconds);
        }

        StartCoroutine(CursorCoroutine());
    }

    IEnumerator CursorCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 1);
            yield return new WaitForSeconds(0.5f);
            cursor.color = new Color(cursor.color.r, cursor.color.g, cursor.color.b, 0);
        }
    }
}
