using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject block;
    public GameObject shapeContainer;
    public List<Sprite> sprites = new List<Sprite>();

    public float YOS;
    public float XOS;

    private List<GameObject> boardBlocks = new List<GameObject>();
    private List<GameObject> boardShapes = new List<GameObject>();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    //Picks a random letter to put in the shape
    public void randomLetter(GameObject container, Vector3 spot)
    {
        GameObject newBlock = Instantiate(block, spot, Quaternion.identity);
        int i = Random.Range(0, 25);
        newBlock.GetComponent<SpriteRenderer>().sprite = sprites[i];
        newBlock.transform.parent = container.transform;

        boardBlocks.Add(newBlock);
    }

    //Creates a block formation using the randomLetter() method above
    public void blockFormation()
    {

        //Deletes all existing shapes and clears lists
        //delShapes();

        int i = Random.Range(0,4);
        Vector3 spawnCoords = new Vector3(0,0,0);
        GameObject container = Instantiate(shapeContainer, new Vector3(0,0,0), Quaternion.identity);

        switch (i)
        {
            case 0:
                //L shape
                spawnCoords = new Vector3(-1 - XOS, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(-1 - XOS, 1 + YOS, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(1 + XOS, 0, 0);
                randomLetter(container, spawnCoords);

                break;


            case 1:
                //T shape
                spawnCoords = new Vector3(-1 - XOS, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, -1 - YOS, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(1 + XOS, 0, 0);
                randomLetter(container, spawnCoords);

                break;


            case 2:
                //Line shape
                spawnCoords = new Vector3(0, 2 + YOS*2, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 1 + YOS, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, -1 - YOS, 0);
                randomLetter(container, spawnCoords);

                break;


            case 3:
                //S shape
                spawnCoords = new Vector3(-1 - XOS, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 1 + YOS, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(1 + XOS, 1 + YOS, 0);
                randomLetter(container, spawnCoords);

                break;


            default:
                break;
        }
        container.transform.position = new Vector3(-5f,5f,0f);
        boardShapes.Add(container);
    }

    public void delShapes()
    {
        foreach (GameObject instance in boardShapes)
        {
            Destroy(instance);
        }
        boardShapes.Clear();
        boardBlocks.Clear();
    }
}
