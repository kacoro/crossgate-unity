using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private string newScenePassword;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            PlayerController.Instance.scenePassword = newScenePassword;
           
            //SceneManager.LoadScene(sceneName);

            FindObjectOfType<SceneFader>().FadeTo(sceneName);
        }
    }
}
