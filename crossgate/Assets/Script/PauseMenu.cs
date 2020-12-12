using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject PauseMenuUI;
    public GameObject MenuUI;
    public GameObject BagUI;
 
    private PlayerInputActions controls;

    private void Awake() {
         controls = new PlayerInputActions();
         controls.GamePlay.Start.started += ctx => Pause();
    }
     private void OnEnable() {
        controls.GamePlay.Enable();
    }

     private void OnDisable() {
         controls.GamePlay.Disable();
     }
    public void Resume(){
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f; //
        GameIsPause = false;
    }

    public void OpenBag(){
        MenuUI.SetActive(false);
        BagUI.SetActive(true);
    }

    public void Pause(){
        if(GameIsPause) {
            Resume(); 
            return ;
        };
         Debug.Log("pause");
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f; //
        GameIsPause = true;
    }

    public void SaveGame(){
        GameIsPause = false;
        Time.timeScale = 1.0f; //
        Player target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target.SavePlayer();
         PauseMenuUI.SetActive(false);

    }

    public void LoadGame(){
        GameIsPause = false;
        Time.timeScale = 1.0f; //
        Player target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PauseMenuUI.SetActive(false);
       target.LoadPlayer();
    }


    public void MainMenu(){
        GameIsPause = false;
        Time.timeScale = 1.0f; //
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    }

    private void Update() {
        
    }
}
