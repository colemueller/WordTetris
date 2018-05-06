using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shapeMover : MonoBehaviour {

    public float speed;
    public BoxCollider2D ground;
    private bool move;

	// Use this for initialization
	void Start () {
        move = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (move)
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * speed);
        }		
	}

    void OnTriggerEnter2D()
    {
        move = false;
    }
}
