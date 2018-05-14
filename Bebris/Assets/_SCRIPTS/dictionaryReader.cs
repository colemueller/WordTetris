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

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckDictionary(List<string> rowToCheck)
    {

        

        foreach (string wordToCheck in rowToCheck)
        {
            string suf = "";
            if (wordToCheck.StartsWith("a"))
            {
                suf = "_a";
            }
            else if (wordToCheck.StartsWith("b"))
            {
                suf = "_b";
            }
            else if (wordToCheck.StartsWith("c"))
            {
                suf = "_c";
            }
            else if (wordToCheck.StartsWith("d"))
            {
                suf = "_d";
            }
            else if (wordToCheck.StartsWith("e"))
            {
                suf = "_e";
            }
            else if (wordToCheck.StartsWith("f"))
            {
                suf = "_f";
            }
            else if (wordToCheck.StartsWith("g"))
            {
                suf = "_g";
            }
            else if (wordToCheck.StartsWith("h"))
            {
                suf = "_h";
            }
            else if (wordToCheck.StartsWith("i"))
            {
                suf = "_i";
            }
            else if (wordToCheck.StartsWith("j"))
            {
                suf = "_j";
            }
            else if (wordToCheck.StartsWith("k"))
            {
                suf = "_k";
            }
            else if (wordToCheck.StartsWith("l"))
            {
                suf = "_l";
            }
            else if (wordToCheck.StartsWith("m"))
            {
                suf = "_m";
            }
            else if (wordToCheck.StartsWith("n"))
            {
                suf = "_n";
            }
            else if (wordToCheck.StartsWith("o"))
            {
                suf = "_o";
            }
            else if (wordToCheck.StartsWith("p"))
            {
                suf = "_p";
            }
            else if (wordToCheck.StartsWith("q"))
            {
                suf = "_q";
            }
            else if (wordToCheck.StartsWith("r"))
            {
                suf = "_r";
            }
            else if (wordToCheck.StartsWith("s"))
            {
                suf = "_s";
            }
            else if (wordToCheck.StartsWith("t"))
            {
                suf = "_t";
            }
            else if (wordToCheck.StartsWith("u"))
            {
                suf = "_u";
            }
            else if (wordToCheck.StartsWith("v"))
            {
                suf = "_v";
            }
            else if (wordToCheck.StartsWith("w"))
            {
                suf = "_w";
            }
            else if (wordToCheck.StartsWith("y"))
            {
                suf = "_y";
            }
            string path = "Assets/_SCRIPTS/BebrisListTest_Max7" + suf + ".txt";

            StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();
            Debug.Log(wordToCheck);
            while ((line != null))
            {
                line = reader.ReadLine();
                if (line == wordToCheck)
                {
                    Debug.Log("Found it!");
                    break;
                }
            }



            reader.Close();
        }
    }
}
