using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letter : MonoBehaviour {

    public bool isEmpty;
    public char currentChar;

    private void Awake()
    {
        isEmpty = true;
    }

    public void Clear()
    {
        isEmpty = true;
        currentChar = ' ';
    }

}
