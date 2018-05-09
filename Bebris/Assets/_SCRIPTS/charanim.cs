using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charanim : MonoBehaviour {

    public Sprite sprite_1;
    public Sprite sprite_2;

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
        else
        {
            current = sprite_1;
        }

        this.GetComponent<SpriteRenderer>().sprite = current;
        timer = animTimer;
    }
}
