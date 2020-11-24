using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {Freeroam,Battle}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] NewBattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    GameState state;

    private void Start() {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattel;
    }

    void StartBattle(){
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        battleSystem.StartBattle();
    }

    void EndBattel(bool won){
        state = GameState.Freeroam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void FixedUpdate() {
        if(state == GameState.Freeroam){
            playerController.HandleUpdate();
        }
        else if(state == GameState.Battle){
            battleSystem.HandleUpdate();
        }
    }
}
