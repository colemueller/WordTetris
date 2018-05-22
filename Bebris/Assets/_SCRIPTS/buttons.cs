using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttons : MonoBehaviour {

    public AudioSource pop;
    public Image fadeImg;
    public float fadeSpeed;
    private bool goOn = false;
    private float fadeAlpha = 0;
    private float refNum = 0.0f;
    

	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (goOn)
        {
            fadeImg.gameObject.SetActive(true);
            fadeAlpha = Mathf.SmoothDamp(fadeAlpha, 1, ref refNum, fadeSpeed);
            fadeImg.color = new Color(0,0,0,fadeAlpha);

            if(fadeAlpha > .99f)
            {
                SceneManager.LoadScene(1);
            }
        }
	}

    public void OnBeginPress()
    {
        pop.Play();
        goOn = true;
        //SceneManager.LoadScene(1);
    }
}
