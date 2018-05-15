﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public SoundManager sound;
    public charanim charAnim;
    public List<Sprite> sprites = new List<Sprite>();
    public float gravSpeed;
    
    public dictionaryReader dictCheck;

    public Transform Grid;
    private Transform[] rows;

    private int totalLetters;

    public GameObject EndScreen;
    private bool doEnd;
    public ParticleSystem yay;

    private int score;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        doEnd = true;
        EndScreen.SetActive(false);

        totalLetters = 0;

        rows = new Transform[12];

        for(int i = 0; i < 12; i++)
        {
            //7 columns per row
            rows[i] = Grid.GetChild(i);
        }

        StartCoroutine(Gravity());
        MakeShape();
	}

    private void Update()
    {
        scoreText.text = score.ToString();

        //go left
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        //go right
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        //go down
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }

        //End Game
        for (int j = 0; j < 7; j++)
        {
            //If something is in the top row
            if (rows[8].GetChild(j).GetComponent<letter>().isEmpty == false)
            {
                //If it is not moving down anymore
                if (rows[8].GetChild(j).GetComponent<letter>().liveLetter == false)
                {
                    if (doEnd)
                    {
                        EndGame();
                    }
                    
                }
            }
        }
    }

    private void MoveLeft()
    {
        List <int> activeLetters = new List<int>();
        bool validMove = true;

        for (int i = 0; i <= 11; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                //current space has a letter in it
                if (rows[i].GetChild(j).GetComponent<letter>().liveLetter == true)
                {
                    //current row is NOT left column and there is an empty space to the left of current space
                    if ((rows[i].GetChild(j - 1).GetComponent<letter>().isEmpty))
                    {
                        activeLetters.Add(i);
                        activeLetters.Add(j);
                    } else
                    {
                        //Check if destination is not empty because it has part of the shape in it
                        if (rows[i].GetChild(j - 1).GetComponent<letter>().liveLetter)
                        {
                            activeLetters.Add(i);
                            activeLetters.Add(j);
                        }
                        else
                        {
                            validMove = false;
                        }
                    }
                }
            }
        }

        if (validMove)
        {
            int l = 1;
            int row = 0;
            int col = 0;
            foreach (int s in activeLetters)
            {
                if (l % 2 == 0)
                {
                    col = s;

                    SetLetter(rows[row].GetChild(col - 1), rows[row].GetChild(col).GetComponent<letter>().currentChar);
                    ClearLetter(rows[row].GetChild(col));
                }
                else
                {
                    row = s;
                }
                l++;
            }
        }

    }

    private void MoveRight()
    {
        List<int> activeLetters = new List<int>();
        bool validMove = true;

        for (int i = 11; i >= 0; i--)
        {
            for (int j = 6; j >= 0; j--)
            {
                //current space has a letter in it
                if (rows[i].GetChild(j).GetComponent<letter>().liveLetter == true)
                {
                    //current row is NOT left column and there is an empty space to the left of current space
                    if ((rows[i].GetChild(j + 1).GetComponent<letter>().isEmpty))
                    {
                        activeLetters.Add(i);
                        activeLetters.Add(j);
                    }
                    else
                    {
                        //Check if destination is not empty because it has part of the shape in it
                        if (rows[i].GetChild(j + 1).GetComponent<letter>().liveLetter)
                        {
                            activeLetters.Add(i);
                            activeLetters.Add(j);
                        }
                        else
                        {
                            validMove = false;
                        }
                    }
                }
            }
        }

        if (validMove)
        {
            int l = 1;
            int row = 0;
            int col = 0;
            foreach (int s in activeLetters)
            {
                if (l % 2 == 0)
                {
                    col = s;

                    SetLetter(rows[row].GetChild(col + 1), rows[row].GetChild(col).GetComponent<letter>().currentChar);
                    ClearLetter(rows[row].GetChild(col));
                }
                else
                {
                    row = s;
                }
                l++;
            }
        }
    }
	
    private void MoveDown()
    {
        for (int i = 0; i <= 11; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                //current space has a letter in it
                if (rows[i].GetChild(j).GetComponent<letter>().isEmpty == false)
                {
                    //current row is NOT bottom row and there is an empty space below current space
                    if ((i != 0) && (rows[i - 1].GetChild(j).GetComponent<letter>().isEmpty))
                    {
                        SetLetter(rows[i - 1].GetChild(j), rows[i].GetChild(j).GetComponent<letter>().currentChar);
                        ClearLetter(rows[i].GetChild(j));
                    }
                    else
                    {
                        rows[i].GetChild(j).GetComponent<letter>().liveLetter = false;
                    }
                }
            }
        }
    }

    private IEnumerator Gravity()
    {
        yield return new WaitForSeconds(gravSpeed/2);
        //score++;
        bool movement = true;
        int cantMoveCount = 0;

        int checkRow = -1;
        int checkCol = -1;

        if (movement)
        {
            for (int i = 0; i <= 11; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //current space has a letter in it
                    if (rows[i].GetChild(j).GetComponent<letter>().isEmpty == false)
                    {
                        //current row is NOT bottom row and there is an empty space below current space
                        if ((i != 0) && (rows[i - 1].GetChild(j).GetComponent<letter>().isEmpty))
                        {
                            SetLetter(rows[i - 1].GetChild(j), rows[i].GetChild(j).GetComponent<letter>().currentChar);
                            ClearLetter(rows[i].GetChild(j));
                        }
                        else
                        {
                            rows[i].GetChild(j).GetComponent<letter>().liveLetter = false;
                            cantMoveCount++;

                            if (cantMoveCount == totalLetters)
                            {
                                checkRow = i;
                                checkCol = j;
                                movement = false;
                            }
                        }
                    }
                }
            }
        }
        if(!movement)
        {
            //Check dictionary at desired row
            if (checkRow != -1)
            {
                string foundWord = "";
                for (int lim = 0; checkRow >= lim; checkRow--)
                {
                    List<char> rowContents = GetRow(checkRow);

                    for (int length = 0; length < 5; length++)
                    {
                        foundWord = dictCheck.CheckDictionary(rowContents);
                        if (! dictCheck.foundAWord)
                        {
                            List<char> tempContents = new List<char>();
                            foreach (char elem in rowContents)
                            {
                                tempContents.Add(elem);
                            }

                            for (int shorterWord = 0; tempContents.Count > 3; shorterWord++)
                            {
                                tempContents.RemoveAt(tempContents.Count - 1);

                                foundWord = dictCheck.CheckDictionary(tempContents);
                                if (! dictCheck.foundAWord)
                                {
                                    Debug.Log("No word");  
                                }
                                else
                                {
                                    RemoveLetters(foundWord, checkRow);
                                    //MakeShape();
                                    break;
                                }
                            }
                            rowContents.RemoveAt(0);
                        }
                        else
                        {
                            RemoveLetters(foundWord, checkRow);
                            //MakeShape();
                            break;
                        }
                    }
                }
            }

            //Debug.Log("FUCH YOU!");
            MakeShape();
            movement = true;
        }

        StartCoroutine(Gravity());
    }

    public List<char> GetRow(int row)
    {
        List<char> rowContents = new List<char>();
        for (int i = 0; i < 7; i++)
        {
            rowContents.Add(rows[row].GetChild(i).GetComponent<letter>().currentChar);
        }
        return rowContents;
    }

    public void RemoveLetters(string foundWord, int row)
    {
        List<char> charsInWord = new List<char>();
        foreach (char let in foundWord)
        {
            charsInWord.Add(let);
        }
        Debug.Log("Removing Letters: " + foundWord);
        for (int j = 0; j < 7; j++)
        {
            for (int i = 0; i < charsInWord.Count; i++)
            {
                Debug.Log("Chars in word: " + charsInWord[i]);
                Debug.Log("Does it match this: " + rows[row].GetChild(j).GetComponent<letter>().currentChar);
                if (charsInWord[i] == rows[row].GetChild(j).GetComponent<letter>().currentChar)
                {
                    Debug.Log("Delete: " + charsInWord[i]);
                    rows[row].GetChild(j).GetComponent<letter>().Clear();
                    rows[row].GetChild(j).GetComponent<SpriteRenderer>().sprite = null;
                    Instantiate(yay, rows[row].GetChild(j));
                    
                    totalLetters--;
                }
            }
        }

        ClearWordEffects();
    }

    void ClearWordEffects()
    {
        //Debug.Log("POP");
        charAnim.HappyBoi();
        sound.ClearWord();
        score += 100;
    }


    //Picks a random letter to put in the shape
    public void SetRandomLetter(Transform spot)
    {
        int i = Random.Range(0, 24);
        spot.GetComponent<SpriteRenderer>().sprite = sprites[i];
        spot.GetComponent<letter>().isEmpty = false;
        spot.GetComponent<letter>().liveLetter = true;

        switch (i)
        {
            case 0:
                spot.GetComponent<letter>().currentChar = 'a';
                break;
            case 1:
                spot.GetComponent<letter>().currentChar = 'b';
                break;
            case 2:
                spot.GetComponent<letter>().currentChar = 'c';
                break;
            case 3:
                spot.GetComponent<letter>().currentChar = 'd';
                break;
            case 4:
                spot.GetComponent<letter>().currentChar = 'e';
                break;
            case 5:
                spot.GetComponent<letter>().currentChar = 'f';
                break;
            case 6:
                spot.GetComponent<letter>().currentChar = 'g';
                break;
            case 7:
                spot.GetComponent<letter>().currentChar = 'h';
                break;
            case 8:
                spot.GetComponent<letter>().currentChar = 'i';
                break;
            case 9:
                spot.GetComponent<letter>().currentChar = 'j';
                break;
            case 10:
                spot.GetComponent<letter>().currentChar = 'k';
                break;
            case 11:
                spot.GetComponent<letter>().currentChar = 'l';
                break;
            case 12:
                spot.GetComponent<letter>().currentChar = 'm';
                break;
            case 13:
                spot.GetComponent<letter>().currentChar = 'n';
                break;
            case 14:
                spot.GetComponent<letter>().currentChar = 'o';
                break;
            case 15:
                spot.GetComponent<letter>().currentChar = 'p';
                break;
            case 16:
                spot.GetComponent<letter>().currentChar = 'q';
                break;
            case 17:
                spot.GetComponent<letter>().currentChar = 'r';
                break;
            case 18:
                spot.GetComponent<letter>().currentChar = 's';
                break;
            case 19:
                spot.GetComponent<letter>().currentChar = 't';
                break;
            case 20:
                spot.GetComponent<letter>().currentChar = 'u';
                break;
            case 21:
                spot.GetComponent<letter>().currentChar = 'v';
                break;
            case 22:
                spot.GetComponent<letter>().currentChar = 'w';
                break;
            case 23:
                spot.GetComponent<letter>().currentChar = 'y';
                break;
        }

        totalLetters++;
    }

    public void SetLetter(Transform spot, char letter)
    {
        spot.GetComponent<letter>().currentChar = letter;
        spot.GetComponent<letter>().isEmpty = false;
        spot.GetComponent<letter>().liveLetter = true;

        switch (letter)
        {
            case 'a':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case 'b':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 'c':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case 'd':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[3];
                break;
            case 'e':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[4];
                break;
            case 'f':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[5];
                break;
            case 'g':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[6];
                break;
            case 'h':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[7];
                break;
            case 'i':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[8];
                break;
            case 'j':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[9];
                break;
            case 'k':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[10];
                break;
            case 'l':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[11];
                break;
            case 'm':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[12];
                break;
            case 'n':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[13];
                break;
            case 'o':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[14];
                break;
            case 'p':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[15];
                break;
            case 'q':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[16];
                break;
            case 'r':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[17];
                break;
            case 's':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[18];
                break;
            case 't':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[19];
                break;
            case 'u':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[20];
                break;
            case 'v':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[21];
                break;
            case 'w':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[22];
                break;
            case 'y':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[24];
                break;

        }
    }

    public void ClearLetter(Transform spot)
    {
        spot.GetComponent<SpriteRenderer>().sprite = null;
        spot.GetComponent<letter>().Clear();
    }

    //Creates a block formation using the randomLetter() method above
    public void MakeShape()
    {

        //SetRandomLetter(rows[10].GetChild(2));
        SetRandomLetter(rows[10].GetChild(3));

        /*
        int i = Random.Range(0,5);
        
        
        switch (i)
        {
            case 0:
                //L shape
                SetRandomLetter(rows[10].GetChild(2));
                SetRandomLetter(rows[10].GetChild(3));
                SetRandomLetter(rows[10].GetChild(4));

                SetRandomLetter(rows[11].GetChild(4));
                break;


            case 1:
                //T shape
                SetRandomLetter(rows[11].GetChild(2));
                SetRandomLetter(rows[11].GetChild(3));
                SetRandomLetter(rows[11].GetChild(4));

                SetRandomLetter(rows[10].GetChild(3));
                break;


            case 2:
                //Line shape
                SetRandomLetter(rows[11].GetChild(1));
                SetRandomLetter(rows[11].GetChild(2));
                SetRandomLetter(rows[11].GetChild(3));
                SetRandomLetter(rows[11].GetChild(4));

                break;


            case 3:
                //S shape
                SetRandomLetter(rows[10].GetChild(2));
                SetRandomLetter(rows[10].GetChild(3));
                SetRandomLetter(rows[11].GetChild(3));
                SetRandomLetter(rows[11].GetChild(4));

                break;

            case 4:
                //Square shape
                SetRandomLetter(rows[11].GetChild(3));
                SetRandomLetter(rows[11].GetChild(4));

                SetRandomLetter(rows[10].GetChild(3));
                SetRandomLetter(rows[10].GetChild(4));

                break;


            default:
                break;
        }
        */
    }

    public void delShapes()
    {
        
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        sound.PlayOof();
        EndScreen.SetActive(true);
        Debug.Log("Game Over, Man");
        doEnd = false;
    }
}
