using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip music_a;
    public AudioClip music_b;
    public AudioSource oof;
    public AudioSource pop;
    public AudioSource clap;

    private AudioSource main_music;

	// Use this for initialization
	void Start () {
        main_music = GetComponent<AudioSource>();

        int rand = Random.Range(1, 3);

        if(rand == 1)
        {
            main_music.clip = music_a;
        }
        else
        {
            main_music.clip = music_b;
        }
        main_music.Play();
	}
	

    public void PlayOof()
    {
        oof.Play();
    }

    public void ClearWord()
    {
        pop.Play();
        clap.Play();
    }
}
