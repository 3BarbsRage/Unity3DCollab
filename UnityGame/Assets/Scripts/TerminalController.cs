using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalController : MonoBehaviour
{
    public TMP_Text[] Lines;
    //n is the count of Lines since Lines is gonna be maxxed out at all times
    public int n;
    public TMP_Text NewLinePrefab;
    public Interactor interactor;
    public Dictionary<string, Action> commands = new Dictionary<string, Action>();
    public Dictionary<string, Action> files = new Dictionary<string, Action>();

    void Start()
    {
        files["README.txt"] = () =>
        {
            Out("Thank you for reading me!");
            Out("EOF - Lex");
            TakeInput();
        };
        commands["help"] = () =>
        { 
            Out("Of Course! Here is a list of commands:");
            Out("help - display list of commands");
            Out("directory - display a directory of files");
            Out("open [filename] - opens [filename]");
            TakeInput();
        };
        commands["dire"] = () =>
        {
            Out("README.txt");
            TakeInput();
        };
        commands["open"] = () =>
        {
            //Debug.Log(Lines[n - 1].text.Substring(8, Lines[n - 1].text.Length));
            try { files[Lines[n - 1].text.Substring(7)].Invoke(); }
            catch { Out("Directory Not Found"); TakeInput(); }
        };
    }

    void TakeInput()
    {
        Lines[n].text = "> ";
        n++;
    }

    void CheckLength()
    {
        if (n > 16)
        {
            n = 8;
            int i = 0;
            while (i <= 14)
            {
                Lines[i].text = Lines[i + 8].text;
                Lines[i + 8].text = "";
                i++;
            }
        }
    }

    void Out(string OutText)
    {
        CheckLength();
        Lines[n].text = OutText;
        n++;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Lines.Last.Value);
        if (interactor.inComputer)
        {
            if (Input.anyKeyDown)
            {
                // Check if the pressed key is a letter or a number (you can adjust this condition based on your requirements)
                if (Input.inputString.Length > 0 && (char.IsLetterOrDigit(Input.inputString[0]) || Input.inputString[0] == ' ' || Input.inputString[0] == '.'))
                {
                    Lines[n-1].text += Input.inputString;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Debug.Log(Lines[n - 1].text.Substring(2, 4));
                    try { commands[Lines[n - 1].text.Substring(2,4)].Invoke(); }
                    catch { Out("Unkown command"); TakeInput(); }
                }
            }
        }
    }
}
