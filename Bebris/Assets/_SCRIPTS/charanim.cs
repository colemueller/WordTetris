using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charanim : MonoBehaviour {

    public Sprite happyBoi_1;
    public Sprite happyBoi_2;
    public Sprite happyBoi_3;
    public Sprite happyBoi_4;
    public Sprite happyBoi_5;
    public Sprite happyBoi_6;

    public Sprite sprite_1;
    public Sprite sprite_2;
    public Sprite sprite_3;
    public Sprite sprite_4;

    private Sprite current;
    public float animTimer;
    private float timer;

	// Use this for initialization
	void Start () {
        current = sprite_1;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            ToggleSprite();
        }
	}

    void ToggleSprite()
    {
        if(current == sprite_1)
        {
            current = sprite_2;
        }
        else if(current == sprite_2)
        {
            current = sprite_3;
        }
        else if (current == sprite_3)
        {
            current = sprite_4;
        }
        else
        {
            current = sprite_1;
        }

        this.GetComponent<SpriteRenderer>().sprite = current;
        timer = animTimer;
    }

    public void HappyBoi()
    {
        if (current == happyBoi_1)
        {
            current = happyBoi_2;
        }
        else if (current = happyBoi_2)
        {
            current = happyBoi_3;
        }
        else if (current = happyBoi_3)
        {
            current = happyBoi_4;
        }
        else if (current = happyBoi_4)
        {
            current = happyBoi_5;
        }
        else if (current = happyBoi_5)
        {
            current = happyBoi_6;
        }
        else
        {
            current = happyBoi_1;
        }

        this.GetComponent<SpriteRenderer>().sprite = current;
        timer = 1;
    }
}
