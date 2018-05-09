using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letter : MonoBehaviour {

    public bool isEmpty;
    public char currentChar;
    public bool liveLetter;

    private void Awake()
    {
        isEmpty = true;
        liveLetter = false;
    }

    public void Clear()
    {
        isEmpty = true;
        liveLetter = false;
        currentChar = ' ';
    }

}
