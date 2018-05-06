using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject block;
    public GameObject shapeContainer;
    public List<Sprite> sprites = new List<Sprite>();

    private List<GameObject> boardBlocks = new List<GameObject>();
    private List<GameObject> boardShapes = new List<GameObject>();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void randomLetter(GameObject container, Vector3 spot)
    {
        GameObject newBlock = Instantiate(block, spot, Quaternion.identity);
        int i = Random.Range(0, 25);
        newBlock.GetComponent<SpriteRenderer>().sprite = sprites[i];
        newBlock.transform.parent = container.transform;

        boardBlocks.Add(newBlock);
    }

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
                spawnCoords = new Vector3(-1,0,0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(-1, 1, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(1, 0, 0);
                randomLetter(container, spawnCoords);

                break;


            case 1:
                //T shape
                spawnCoords = new Vector3(-1, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, -1, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(1, 0, 0);
                randomLetter(container, spawnCoords);

                break;


            case 2:
                //Line shape
                spawnCoords = new Vector3(0, 2, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 1, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, -1, 0);
                randomLetter(container, spawnCoords);

                break;


            case 3:
                //S shape
                spawnCoords = new Vector3(-1, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 0, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(0, 1, 0);
                randomLetter(container, spawnCoords);

                spawnCoords = new Vector3(1, 1, 0);
                randomLetter(container, spawnCoords);

                break;


            default:
                break;
        }
        boardShapes.Add(container);
        Debug.Log(boardShapes.Count);
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
