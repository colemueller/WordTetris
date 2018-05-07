using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shapeMover : MonoBehaviour {

    public float speed;
    public BoxCollider2D ground;
    private bool fall;
    private bool moveLeft;
    private bool moveRight;

	// Use this for initialization
	void Start () {
        fall = true;
        moveLeft = true;
        moveRight = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (fall)
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * speed);

            if (Input.GetKey(KeyCode.A))
            {
                if (moveLeft)
                {
                    this.transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
            } else if (Input.GetKey(KeyCode.D))
            {
                if (moveRight)
                {
                    this.transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
            } else if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(Vector3.down * Time.deltaTime * speed * 2);
            }
        }		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ground")
        {
            fall = false;
        }
        if (other.gameObject.tag == "Block")
        {
            Vector3 direction = other.transform.position - this.transform.position;
            Debug.Log(Vector3.Dot(this.transform.up, direction));
            if (Vector3.Dot(this.transform.up, direction) != 0)
            {
                fall = false;
            }
        }
        if (other.gameObject.name == "leftSide")
        {
            moveLeft = false;
        }
        if (other.gameObject.name == "rightSide")
        {
            moveRight = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "leftSide")
        {
            moveLeft = true;
        }
        if (collision.gameObject.name == "rightSide")
        {
            moveRight = true;
        }
    }
}
