using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Console : ComputerConsole
{
    private string outputConsoleText = "";
    private string computerConsoleText = "";

    [SerializeField] Sprite calmOutput = null;
    [SerializeField] Sprite warningOutput = null;

    [SerializeField] Sprite calmComputer = null;
    [SerializeField] Sprite warningComputer = null;

    [SerializeField] Button outputButton = null;
    [SerializeField] Button computerButton = null;

    [SerializeField] TMP_Text computerText;
    [SerializeField] TMP_Text outputText;

    [SerializeField] Image computerCursor;
    [SerializeField] Image outputCursor;

    private bool isThereNewMessageInComputerConsole = false;
    private bool isThereNewMessageInOutputConsole = false;

    private bool outputConsoleIsActive = false;
    private bool computerConsoleIsActive = false;

    private bool textIsAdded = false;
    private int lastOriginIndex = 0;

    private bool doShowMessageAnyway = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PageUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PageDown();
        }

        if (Input.GetKeyDown(KeyCode.Space) && console.activeSelf && isPrinting)
        {
            skip = true;
        }
    }

    public void ShowOutputConsole()
    {
        if (!outputConsoleIsActive || doShowMessageAnyway)
        {
            StopAllCoroutines();

            doShowMessageAnyway = false;

            //SetActive(outputText);

            outputButton.GetComponent<Image>().sprite = calmOutput;

            outputConsoleIsActive = true;

            if (computerConsoleIsActive)
            {
                computerConsoleText = text_string;
                if (!computerConsoleText.Equals(text.text.Substring(0, text.text.Length - 2)))
                {
                    computerButton.GetComponent<Image>().sprite = warningComputer;

                    //isThereNewMessageInComputerConsole = true;
                }
            }

            computerConsoleIsActive = false;

            if (!isThereNewMessageInOutputConsole)
                skip = true;
            isThereNewMessageInOutputConsole = false;

            isShown = false;

            SetTextAndShow(outputConsoleText, false);
        }
        else if (textIsAdded && outputConsoleIsActive)
        {
            StopAllCoroutines();

            //SetActive(outputText);

            outputButton.GetComponent<Image>().sprite = calmOutput;

            if (!isThereNewMessageInOutputConsole)
                skip = true;
            isThereNewMessageInOutputConsole = false;
                
            isShown = false;

            textIsAdded = false;

            SetTextAndShow(outputConsoleText.Substring(lastOriginIndex), true);
        }

        outputButton.interactable = false;
        computerButton.interactable = true;
    }
    
    public void ShowComputerConsole()
    {
        if (!computerConsoleIsActive || doShowMessageAnyway)
        {
            StopAllCoroutines();

            doShowMessageAnyway = false;

            //SetActive(computerText);

            computerButton.GetComponent<Image>().sprite = calmComputer;

            computerConsoleIsActive = true;

            if (outputConsoleIsActive)
            {
                outputConsoleText = text_string;
                if (!outputConsoleText.Equals(text.text))
                {
                    outputButton.GetComponent<Image>().sprite = warningOutput;

                    //isThereNewMessageInOutputConsole = true;
                }
            }

            outputConsoleIsActive = false;

            if (!isThereNewMessageInComputerConsole)
                skip = true;
            isThereNewMessageInComputerConsole = false;

            isShown = false;

            SetTextAndShow(computerConsoleText, false);
            //SetTextAndShow(computerConsoleText.Substring(lastOriginIndex), textIsAdded);
        }
        else if(textIsAdded && computerConsoleIsActive)
        {
            StopAllCoroutines();

            //SetActive(outputText);

            computerButton.GetComponent<Image>().sprite = calmComputer;

            if (!isThereNewMessageInComputerConsole)
                skip = true;
            isThereNewMessageInComputerConsole = false;

            isShown = false;

            textIsAdded = false;

            SetTextAndShow(computerConsoleText.Substring(lastOriginIndex), true);
        }

        computerButton.interactable = false;
        outputButton.interactable = true;
    }

    public void SetComputerText(string text, bool add)
    {
        computerButton.GetComponent<Image>().sprite = warningComputer;

        isThereNewMessageInComputerConsole = true;
        if (add)
        {
            lastOriginIndex = computerConsoleText.Length;
            computerConsoleText += text;
        }
        else
            computerConsoleText = text;

        textIsAdded = add;
    }
    
    public void SetOutputText(string text, bool add)
    {
        outputButton.GetComponent<Image>().sprite = warningOutput;

        isThereNewMessageInOutputConsole = true;
        if (add)
        {
            lastOriginIndex = outputConsoleText.Length;
            outputConsoleText += text;
        }
        else
            outputConsoleText = text;

        textIsAdded = add;
    }

    public void SetMode(CONSOLE_MODE mode)
    {
        StopAllCoroutines();
        isShown = false;
        switch (mode)
        {
            case CONSOLE_MODE.COMPUTER:
                computerButton.GetComponent<Image>().sprite = calmComputer;

                if(outputConsoleIsActive)
                    outputConsoleText = text_string;

                computerConsoleIsActive = true;
                outputConsoleIsActive = false;

                isThereNewMessageInComputerConsole = true;

                text_string = computerConsoleText;

                computerButton.interactable = false;
                outputButton.interactable = true;

                break;
            case CONSOLE_MODE.OUTPUT:
                outputButton.GetComponent<Image>().sprite = calmOutput;

                if(computerConsoleIsActive)
                    computerConsoleText = text_string;

                computerConsoleIsActive = false;
                outputConsoleIsActive = true;

                isThereNewMessageInOutputConsole = true;

                text_string = outputConsoleText;

                outputButton.interactable = false;
                computerButton.interactable = true;

                break;
        }

        text.text = text_string;
    }

    public void PageUp()
    {
        text.pageToDisplay = text.pageToDisplay > 0 ? text.pageToDisplay - 1 : 0;
    }

    public void PageDown()
    {
        text.pageToDisplay = text.pageToDisplay < (text.textInfo.pageCount) ? text.pageToDisplay + 1 : text.pageToDisplay;
    }

    public void ClearConsole(CONSOLE_MODE mode)
    {
        if (mode == CONSOLE_MODE.OUTPUT)
        {
            outputConsoleText = "";
            if (outputConsoleIsActive)
            {
                text_string = "";
                text.text = "";
            }
        }
        else if(mode == CONSOLE_MODE.COMPUTER)
        {
            computerConsoleText = "";
            if (computerConsoleIsActive)
            {
                text_string = "";
                text.text = "";
            }
        }
    }

    public void SetDoShowMessageAnyway(bool value)
    {
        doShowMessageAnyway = value;
    }
}
