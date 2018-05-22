using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloop : MonoBehaviour {

    public float speed;
    private RectTransform trans;

	// Use this for initialization
	void Start () {
        trans = this.GetComponent<RectTransform>();
        StartCoroutine(DoTheBloop());
	}
	
	IEnumerator DoTheBloop()
    {
        yield return new WaitForSeconds(speed);

        trans.localScale = new Vector3(1, (trans.localScale.y * -1), 1);
        StartCoroutine(DoTheBloop());
    }
}
