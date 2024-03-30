using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalController : MonoBehaviour
{
    public TMP_Text[] LinesArr;
    public LinkedList<TMP_Text> Lines = new LinkedList<TMP_Text>();
    public TMP_Text NewLinePrefab;
    public Interactor interactor;

    void Start()
    {
        Lines.AddLast(LinesArr[0]);
        Lines.AddLast(LinesArr[1]);
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
                if (Input.inputString.Length > 0 && (char.IsLetterOrDigit(Input.inputString[0]) || Input.inputString[0] == ' '))
                {
                    Lines.Last.Value.text += Input.inputString;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    TMP_Text newLine = Instantiate(NewLinePrefab, transform);
                    newLine.rectTransform.localPosition = new Vector3(0, 235*2-180 - (17.5f * (Lines.Count)), 0);

                    Lines.AddLast(newLine);
                }
            }
        }
    }
}
