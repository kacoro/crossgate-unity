using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {Freeroam,Battle}

public class GameController : MonoBehaviour
{
    //  PlayerController playerController;
    [SerializeField] NewBattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    GameState state;

    private void Awake() {
        // playerController = PlayerController.Instance;
        ConditionsDB.Init();
    }

    private void Start() {
       
            PlayerController.Instance.OnEncountered += StartBattle;
            battleSystem.OnBattleOver += EndBattel;
        
    }

    void StartBattle(){
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        battleSystem.gameObject.transform.position = 
        new Vector3(PlayerController.Instance.transform.position.x,PlayerController.Instance.transform.position.y,-10);
        PlayerController.Instance.GetComponent<SpriteRenderer>().enabled = false;
        worldCamera.gameObject.SetActive(false);

        var playerParty = PlayerController.Instance.GetComponent<PetParty>();
        var wildPet = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildPet();

        battleSystem.StartBattle(playerParty,wildPet);
    }

    void EndBattel(bool won){
        state = GameState.Freeroam;
        var playerParty = PlayerController.Instance.GetComponent<PetParty>();
        playerParty.HealthyAllFaintPetsToOne();
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        PlayerController.Instance.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Update() {
        if(state == GameState.Freeroam){
            PlayerController.Instance.HandleUpdate();
        }
        else if(state == GameState.Battle){
            battleSystem.HandleUpdate();
        }
    }
}
