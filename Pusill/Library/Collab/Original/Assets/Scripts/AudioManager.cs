using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    ScoreKeeper Keeper;


    public AudioClip[] Clips;
    
    //AudioSource audi;
    public AudioSource audiStart;
    public AudioSource audiSong;

    public AudioClip A;
    public AudioClip B;
    public AudioClip C;
    public AudioClip D;
    public AudioClip E;
    public AudioClip F;
    public AudioClip G;

    public List<AudioClip> scale;


    public AudioSource[] Sources;
    public int noteCount;


    void Awake()
    {
       // Clips = new AudioClip[6];

 
       // audi = this.GetComponent<AudioSource>();
        Keeper = GameObject.FindObjectOfType<ScoreKeeper>();
        StartCoroutine(ReadSetSong());
        noteCount = 0;

        scale.Add(A);
        scale.Add(B);
        scale.Add(C);
        scale.Add(D);
        scale.Add(E);
        scale.Add(F);
        scale.Add(G);

        Sources = new AudioSource[scale.Count];


        for (int i = 0; i < scale.Count; i++)
        {
            Sources[i] = gameObject.AddComponent<AudioSource>();
            Sources[i].clip = scale[i];
            // set up the properties such as distance for 3d sounds here if you need to.
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void PlaySound(int clipIdx)
    {
        if (Keeper.streak > Clips.Length) { Keeper.streak = 0; }
        if (clipIdx >= 0 && clipIdx < Clips.Length)
            Sources[Keeper.streak].Play();
        Debug.Log("length: " + Clips.Length);
    }

    public void AudioClipSwitch()
    {

        //TODO: FIX THIS. 
               

       //Sources[()].Play();
       // audi.Play();
        Debug.Log("Played: " +(Keeper.streak - 1));
        Debug.Log("length: " + Clips.Length);

    }

    public IEnumerator ReadSetSong()
    {
        audiStart.Play();
        yield return new WaitForSeconds(audiStart.clip.length - 3.8f);
        //PlaySong();
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
