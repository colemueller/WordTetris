using System.Collections;
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
    private int checkRow;
    private int checkCol;

    public Transform nextUp;
    public Transform Grid;
    private Transform[] rows;

    private int totalLetters;

    public GameObject EndScreen;
    private bool doEnd;
    public ParticleSystem yay;

    private int score;
    public Transform scorePlace;
    public List<Sprite> numbers = new List<Sprite>();
    private Transform[] scoreLine;

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

        scoreLine = new Transform[6];
        for (int i = 0; i < 6; i++)
        {
            //6 digits
            scoreLine[i] = scorePlace.GetChild(5 - i); 
        }

        StartCoroutine(Gravity());
        SetNextUp();
        SetRandomLetter();
        SetNextUp();
	}

    private void Update()
    {

        //go left
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetButtonDown("xbox_x"))
        {
            MoveLeft();
        }

        //go right
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButtonDown("xbox_b"))
        {
            MoveRight();
        }

        
        //Speed up
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown("xbox_a"))
        {
            IncreaseSpeed();
        }

        //Slow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            DecreaseSpeed();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
	
    private void IncreaseSpeed()
    {
        gravSpeed -= .01f;
    }

    private void DecreaseSpeed()
    {
        gravSpeed += .01f;
    }

    private IEnumerator Gravity()
    {
        yield return new WaitForSeconds(gravSpeed / 2);

        bool movement = true;
        int cantMoveCount = 0;


        //Debug.Log("Movement: " + movement);
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
                            if (rows[i].GetChild(j).GetComponent<letter>().liveLetter)
                            {
                                checkRow = i;
                                checkCol = j;
                            }
                            rows[i].GetChild(j).GetComponent<letter>().liveLetter = false;
                            cantMoveCount++;

                            if (cantMoveCount == totalLetters)
                            {                                
                                movement = false;
                            }
                        }
                    }
                }
            }
        }

        if (!movement)
        {
            //Check dictionary at desired row
            if (checkRow != -1 && checkCol != -1)
            {
                string foundWord = "";
                string vertWord = "";

                List<char> rowContents = GetRow(checkRow);
                List<char> colContents = GetCol(checkRow, checkCol);

                for (int startCol = 0; startCol < 5; startCol++)
                {
                    if (foundWord != "")
                    {
                        break;
                    }

                    foundWord = dictCheck.CheckDictionary(rowContents);
                    if (foundWord == "")
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
                            if (!dictCheck.foundAWord)
                            {
                                //Debug.Log("No word");  
                            }
                            else
                            {
                                if (colContents.Count >= 3) {
                                    vertWord = dictCheck.CheckDictionary(colContents);
                                    if (vertWord == "")
                                    {
                                        for (int shorterVertWord = 0; colContents.Count >= 3; shorterVertWord++)
                                        {
                                            colContents.RemoveAt(colContents.Count - 1);

                                            vertWord = dictCheck.CheckDictionary(colContents);
                                            if (vertWord != "")
                                            {
                                                RemoveColLetters(vertWord, checkCol, checkRow);
                                                break;
                                            }
                                        }
                                    }
                                }
                                RemoveLetters(foundWord, checkRow, startCol);
                                break;
                            }
                        }
                        rowContents.RemoveAt(0);
                    }
                    else
                    {
                        RemoveLetters(foundWord, checkRow, startCol);
                        break;
                    }
                }

                if (colContents.Count >= 3)
                {
                    vertWord = dictCheck.CheckDictionary(colContents);
                    if (vertWord == "")
                    {
                        for (int shorterVertWord = 0; colContents.Count >= 3; shorterVertWord++)
                        {
                            colContents.RemoveAt(colContents.Count - 1);

                            vertWord = dictCheck.CheckDictionary(colContents);
                            if (vertWord != "")
                            {
                                RemoveColLetters(vertWord, checkCol, checkRow);
                                break;
                            }
                        }
                    }
                    else
                    {
                        RemoveColLetters(vertWord, checkCol, checkRow);
                    }
                }
            }

            bool needsGrav = false;
            for (int i = 0; i <= 11; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (rows[i].GetChild(j).GetComponent<letter>().liveLetter)
                    {
                        needsGrav = true;
                        break;
                    }
                }
                if(needsGrav)
                {
                    break;
                }
            }
            if (!needsGrav)
            {
                SetRandomLetter();
                SetNextUp();
                movement = true;
            }
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

    public List<char> GetCol (int row, int col)
    {
        List<char> colContents = new List<char>();
        for (int i = row; i >= 0; i--)
        {
            colContents.Add(rows[i].GetChild(col).GetComponent<letter>().currentChar);
        }
        return colContents;
    }

    public void RemoveColLetters(string vertWord, int col, int startRow)
    {
        List<char> charsInWord = new List<char>();
        List<char> charsToDel = new List<char>();

        foreach (char c in vertWord)
        {
            charsInWord.Add(c);
        }


        Debug.Log("Deleting vertWord: " + vertWord + " Starting at row: " + startRow + " Length is: " + vertWord.Length);
        for (int i = 0; i < vertWord.Length; i++)
        {
            rows[startRow - i].GetChild(col).GetComponent<letter>().Clear();
            rows[startRow - i].GetChild(col).GetComponent<SpriteRenderer>().sprite = null;
            Instantiate(yay, rows[startRow - i].GetChild(col));

            totalLetters--;
        }

        ClearWordEffects();
    }

    public void RemoveLetters(string foundWord, int row, int startCol)
    {
        List<char> charsInWord = new List<char>();
        List<char> charsToDel = new List<char>();
        foreach (char let in foundWord)
        {
            charsInWord.Add(let);
        }

        for (int j = 0; j < foundWord.Length; j++)
        {
            rows[row].GetChild(j +startCol).GetComponent<letter>().Clear();
            rows[row].GetChild(j + startCol).GetComponent<SpriteRenderer>().sprite=null;
            Instantiate(yay, rows[row].GetChild(j + startCol));

            totalLetters--;
        }

        ClearWordEffects();
    }

    void ClearWordEffects()
    {
        charAnim.HappyBoi();
        sound.ClearWord();
        score += 100;
        UpdateScore();
    }

    public void UpdateScore()
    {
        int scoreDigits = score.ToString().Length;

        foreach (char c in score.ToString())
        {
            
            int targetIndex = (scoreLine.Length - scoreDigits);
            
            switch (c)
            {
                case '0':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[0];
                    break;
                case '1':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[1];
                    break;
                case '2':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[2];
                    break;
                case '3':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[3];
                    break;
                case '4':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[4];
                    break;
                case '5':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[5];
                    break;
                case '6':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[6];
                    break;
                case '7':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[7];
                    break;
                case '8':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[8];
                    break;
                case '9':
                    scoreLine[targetIndex].GetComponent<SpriteRenderer>().sprite = numbers[9];
                    break;
            }
            
            scoreDigits--;
        }
    }

    void SetNextUp()
    {
        int picker = Random.Range(0, 98);

        if (picker >= 0 && picker < 12)
        {
            //e
            nextUp.GetComponent<letter>().currentChar = 'e';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[4];
        }
        else if (picker >= 12 && picker < 21)
        {
            //a
            nextUp.GetComponent<letter>().currentChar = 'a';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if (picker >= 21 && picker < 30)
        {
            //i
            nextUp.GetComponent<letter>().currentChar = 'i';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[8];
        }
        else if (picker >= 30 && picker < 38)
        {
            //o
            nextUp.GetComponent<letter>().currentChar = 'o';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[14];
        }
        else if (picker >= 38 && picker < 44)
        {
            //n
            nextUp.GetComponent<letter>().currentChar = 'n';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[13];
        }
        else if (picker >= 44 && picker < 50)
        {
            //r
            nextUp.GetComponent<letter>().currentChar = 'r';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[17];
        }
        else if (picker >= 50 && picker < 56)
        {
            //t
            nextUp.GetComponent<letter>().currentChar = 't';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[19];
        }
        else if (picker >= 56 && picker < 60)
        {
            //l
            nextUp.GetComponent<letter>().currentChar = 'l';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[11];
        }
        else if (picker >= 60 && picker < 64)
        {
            //s
            nextUp.GetComponent<letter>().currentChar = 's';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[18];
        }
        else if (picker >= 64 && picker < 68)
        {
            //u
            nextUp.GetComponent<letter>().currentChar = 'u';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[20];
        }
        else if (picker >= 68 && picker < 72)
        {
            //d
            nextUp.GetComponent<letter>().currentChar = 'd';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
        else if (picker >= 72 && picker < 75)
        {
            //g
            nextUp.GetComponent<letter>().currentChar = 'g';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[6];
        }
        else if (picker >= 75 && picker < 77)
        {
            //b
            nextUp.GetComponent<letter>().currentChar = 'b';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        else if (picker >= 77 && picker < 79)
        {
            //c
            nextUp.GetComponent<letter>().currentChar = 'c';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        else if (picker >= 79 && picker < 81)
        {
            //m
            nextUp.GetComponent<letter>().currentChar = 'm';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[12];
        }
        else if (picker >= 81 && picker < 83)
        {
            //p
            nextUp.GetComponent<letter>().currentChar = 'p';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[15];
        }
        else if (picker >= 83 && picker < 85)
        {
            //f
            nextUp.GetComponent<letter>().currentChar = 'f';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[5];
        }
        else if (picker >= 85 && picker < 87)
        {
            //h
            nextUp.GetComponent<letter>().currentChar = 'h';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[7];
        }
        else if (picker >= 87 && picker < 89)
        {
            //v
            nextUp.GetComponent<letter>().currentChar = 'v';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[21];
        }
        else if (picker >= 89 && picker < 91)
        {
            //w
            nextUp.GetComponent<letter>().currentChar = 'w';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[22];
        }
        else if (picker >= 91 && picker < 93)
        {
            //y
            nextUp.GetComponent<letter>().currentChar = 'y';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[24];
        }
        else if (picker == 93 || picker == 97)
        {
            //k
            nextUp.GetComponent<letter>().currentChar = 'k';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[10];
        }
        else if (picker == 94 || picker == 95)
        {
            //j
            nextUp.GetComponent<letter>().currentChar = 'j';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[9];
        }
        else if (picker == 99)
        {
            //x
            nextUp.GetComponent<letter>().currentChar = 'x';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[23];
        }
        else if (picker == 96)
        {
            //q
            nextUp.GetComponent<letter>().currentChar = 'q';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[16];
        }
        else if (picker == 100)
        {
            //z
            nextUp.GetComponent<letter>().currentChar = 'z';
            nextUp.GetComponent<SpriteRenderer>().sprite = sprites[25];
        }
        nextUp.GetComponent<letter>().isEmpty = false;
}
    //Picks a random letter to put in the shape
    public void SetRandomLetter()
    {
        rows[11].GetChild(3).GetComponent<SpriteRenderer>().sprite = nextUp.GetComponent<SpriteRenderer>().sprite;
        rows[11].GetChild(3).GetComponent<letter>().currentChar = nextUp.GetComponent<letter>().currentChar;
        rows[11].GetChild(3).GetComponent<letter>().isEmpty = false;
        rows[11].GetChild(3).GetComponent<letter>().liveLetter = true;
            
        nextUp.GetComponent<letter>().Clear();

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
            case 'x':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[23];
                break;
            case 'y':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[24];
                break;
            case 'z':
                spot.GetComponent<SpriteRenderer>().sprite = sprites[25];
                break;

        }
    }

    public void ClearLetter(Transform spot)
    {
        spot.GetComponent<SpriteRenderer>().sprite = null;
        spot.GetComponent<letter>().Clear();
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
