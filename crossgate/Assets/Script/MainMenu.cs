using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public AudioSource AudioSource;
    public delegate void CallBack();
    public void PlayGame(){
       
        // SceneManager.LoadScene(1);
        StartCoroutine(PlayMusic(()=>{
            SceneManager.LoadScene(1);
        }));
    }
     public void QuitGame(){
       StartCoroutine(PlayMusic(()=>{
             Application.Quit();
        }));
    }
    public void LoadGame(){
        StartCoroutine(PlayMusic(()=>{
             Player.instance.LoadPlayer();
        }));
    }

    private void Start() {
        AudioSource = GetComponent<AudioSource>();
    }
    
    IEnumerator PlayMusic(CallBack callback){
        AudioSource.Play();
        yield return StartCoroutine(AudioPlayFinished(AudioSource.clip.length));
        callback();
    }
    private IEnumerator AudioPlayFinished(float time){
         yield return new WaitForSeconds(time);
    }
   

}
