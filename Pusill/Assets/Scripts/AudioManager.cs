using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    ScoreKeeper Keeper;


    public AudioClip[] Clips;
    AudioSource audi;
    public AudioSource audiStart;
    public AudioSource audiSong;


    
    // Use this for initialization
    void Start () {

        audi = this.GetComponent<AudioSource>();
        Keeper = GameObject.FindObjectOfType<ScoreKeeper>();
        StartCoroutine(ReadSetSong());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AudioClipSwitch()
    {

        //TODO: FIX THIS. 
               if (Keeper.streak > Clips.Length) { Keeper.streak = 1; }

        audi.clip = Clips[(Keeper.streak-1)];
       // audi.Play();
        Debug.Log("Played: " +(Keeper.streak - 1));
        Debug.Log("length: " + Clips.Length);

    }

    public IEnumerator ReadSetSong()
    {
        audiStart.Play();
        yield return new WaitForSeconds(audiStart.clip.length - 3.8f);
        PlaySong();
    }

    public void PauseSong()
    {
        audiSong.Pause();
    }

    public void PlaySong()
    {
        audiSong.Play();
    }
}
