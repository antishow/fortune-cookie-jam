using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour {
	public AudioClip[] Tracks;
	private AudioSource audioSource;

	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}
	void Start () {
		PlayRandomTrack();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			PlayRandomTrack();
		}
	}

	public void PlayRandomTrack(){
		if(Tracks.Length == 0){
			return;
		}

		if(audioSource.isPlaying){
			audioSource.Stop();
		}

		int i = Random.Range(0, Tracks.Length);
		AudioClip clip = Tracks[i];
		Debug.LogFormat("NOW PLAYING “{0}”", clip.name);

		audioSource.clip = clip;
		audioSource.Play(44100);

		float l = clip.length;
		Invoke("PlayRandomTrack", l + 2.0f);
	}
}
