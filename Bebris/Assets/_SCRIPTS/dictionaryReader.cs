using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class dictionaryReader : MonoBehaviour {

    public TextAsset dictionaryList_a;
    public TextAsset dictionaryList_b;
    public TextAsset dictionaryList_c;
    public TextAsset dictionaryList_d;
    public TextAsset dictionaryList_e;
    public TextAsset dictionaryList_f;
    public TextAsset dictionaryList_g;
    public TextAsset dictionaryList_h;
    public TextAsset dictionaryList_i;
    public TextAsset dictionaryList_j;
    public TextAsset dictionaryList_k;
    public TextAsset dictionaryList_l;
    public TextAsset dictionaryList_m;
    public TextAsset dictionaryList_n;
    public TextAsset dictionaryList_o;
    public TextAsset dictionaryList_p;
    public TextAsset dictionaryList_q;
    public TextAsset dictionaryList_r;
    public TextAsset dictionaryList_s;
    public TextAsset dictionaryList_t;
    public TextAsset dictionaryList_u;
    public TextAsset dictionaryList_v;
    public TextAsset dictionaryList_w;
    public TextAsset dictionaryList_y;

    public bool foundAWord = false;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string CheckDictionary(List<char> rowToCheck)
    {
        string word = "";
        foreach (char l in rowToCheck)
        {
            word += l;
        }

        string suf = "";
        if (word.StartsWith("a"))
        {
            suf = "_a";
        }
        else if (word.StartsWith("b"))
        {
            suf = "_b";
        }
        else if (word.StartsWith("c"))
        {
            suf = "_c";
        }
        else if (word.StartsWith("d"))
        {
            suf = "_d";
        }
        else if (word.StartsWith("e"))
        {
            suf = "_e";
        }
        else if (word.StartsWith("f"))
        {
            suf = "_f";
        }
        else if (word.StartsWith("g"))
        {
            suf = "_g";
        }
        else if (word.StartsWith("h"))
        {
            suf = "_h";
        }
        else if (word.StartsWith("i"))
        {
            suf = "_i";
        }
        else if (word.StartsWith("j"))
        {
            suf = "_j";
        }
        else if (word.StartsWith("k"))
        {
            suf = "_k";
        }
        else if (word.StartsWith("l"))
        {
            suf = "_l";
        }
        else if (word.StartsWith("m"))
        {
            suf = "_m";
        }
        else if (word.StartsWith("n"))
        {
            suf = "_n";
        }
        else if (word.StartsWith("o"))
        {
            suf = "_o";
        }
        else if (word.StartsWith("p"))
        {
            suf = "_p";
        }
        else if (word.StartsWith("q"))
        {
            suf = "_q";
        }
        else if (word.StartsWith("r"))
        {
            suf = "_r";
        }
        else if (word.StartsWith("s"))
        {
            suf = "_s";
        }
        else if (word.StartsWith("t"))
        {
            suf = "_t";
        }
        else if (word.StartsWith("u"))
        {
            suf = "_u";
        }
        else if (word.StartsWith("v"))
        {
            suf = "_v";
        }
        else if (word.StartsWith("w"))
        {
            suf = "_w";
        }
        else if (word.StartsWith("y"))
        {
            suf = "_y";
        }
        string path = "Assets/_SCRIPTS/BebrisListTest_Max7" + suf + ".txt";

        StreamReader reader = new StreamReader(path);
        string line = reader.ReadLine();
        while ((line != null))
        {
            line = reader.ReadLine();
            if (line == word.ToString())
            {
                Debug.Log("Found: " + word);
                reader.Close();
                foundAWord = true;
                return word;
            }
        }

        reader.Close();
        foundAWord = false;
        return "";
    }    

}
