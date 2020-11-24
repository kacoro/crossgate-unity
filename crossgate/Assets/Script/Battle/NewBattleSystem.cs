using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum NewBattleState {Start,PlayerAction,PlayerMove,EnemyMove,Busy}

public class NewBattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleUnit EnemyUnit;
    [SerializeField] BattleHud EnemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    NewBattleState state;
    private Vector2 move;
    int currentAction;
    int currentMove;
    private PlayerInputActions controls;
    private void Start()
    {
        StartCoroutine(SetupBattle());
    }
     private void Awake() {
        controls = new PlayerInputActions();
        // controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //  controls.GamePlay.Move.performed += ctx => HandleSelection(ctx.ReadValue<Vector2>());
        // controls.GamePlay.Move.canceled  += ctx => move = Vector2.zero;
        
        controls.GamePlay.Select.performed  += ctx => HandleSelection(ctx.ReadValue<Vector2>());
        controls.GamePlay.Ok.performed += ctx => HandleActionConfirm();
    }
    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        playerHud.setData(playerUnit.Pet);
        EnemyUnit.Setup();
        EnemyHud.setData(EnemyUnit.Pet);

        dialogBox.SetMoveNames(playerUnit.Pet.Moves);

       // dialogBox.SetDialog($"A wild {EnemyUnit.Pet.Base.Name} appeared.");
        //    StartCoroutine(dialogBox.TypeDialog($"A wild {EnemyUnit.Pet.Base.Name} appeared."));
        yield return dialogBox.TypeDialog($"A wild {EnemyUnit.Pet.Base.Name} appeared.");
        yield return new WaitForSeconds(1f);
        PlayerAction();
    }

    void PlayerAction(){
        state = NewBattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);

    }

    private void Update() {
        // if (move != Vector2.zero){
        //     if(state == NewBattleState.PlayerAction){
        //     HandleActionSelection();
        //     }

        //     if(state == NewBattleState.PlayerMove ){
        //         HandleMovesSelection();
        //     }
        // }
        
    }

    void HandleSelection(Vector2 content){
        move = content;
        if(state == NewBattleState.PlayerAction){
            HandleActionSelection();
        }

        if(state == NewBattleState.PlayerMove ){
                HandleMovesSelection();
         }
    }

     private void OnEnable() {
        controls.GamePlay.Enable();
    }

     private void OnDisable() {
         controls.GamePlay.Disable();
     }

    void PlayerMove(){
        state = NewBattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove(){
        state = NewBattleState.Busy;
        var palyerMove = playerUnit.Pet.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.Pet.Base.Name} use {palyerMove.Base.Name}");

        yield return new WaitForSeconds(1f);
        bool isFainted = EnemyUnit.Pet.TakeDamage(palyerMove,playerUnit.Pet);
        yield return EnemyHud.UpdateHP();
        if(isFainted){
            yield return dialogBox.TypeDialog($"{EnemyUnit.Pet.Base.Name} Fainted");
        }else{
            StartCoroutine(enemyMove());
        }
    }

    IEnumerator enemyMove(){
          state = NewBattleState.EnemyMove;
          var randomMove = EnemyUnit.Pet.GetRandomMove();
            yield return dialogBox.TypeDialog($"{EnemyUnit.Pet.Base.Name} use {randomMove.Base.Name}");

            yield return new WaitForSeconds(1f);
            bool isFainted = playerUnit.Pet.TakeDamage(randomMove,EnemyUnit.Pet);
            yield return playerHud.UpdateHP();
            if(isFainted){
                yield return dialogBox.TypeDialog($"{playerUnit.Pet.Base.Name} Fainted");
            }else{
               PlayerAction();
            }
    }

    void HandleActionSelection(){
        if(move.y<0){
            if(currentAction <1)
                ++currentAction;
        }else if(move.y>0){
            if(currentAction >0)
                --currentAction;
        }
        dialogBox.UpdateActionSelection(currentAction);
    }
    void HandleMovesSelection(){
       
        if(move.x>0){
             Debug.Log(move.x);
            if(currentMove < playerUnit.Pet.Moves.Count -1)
                ++currentMove;
        }else if(move.x<0){
             Debug.Log(move.x);
            if(currentMove >0)
                --currentMove;
        }else if(move.y<0){
            if(currentMove <  playerUnit.Pet.Moves.Count -2)
                 currentMove +=2;
        }else if(move.y>0){
            if(currentMove >1)
               currentMove -=2;
        }
        dialogBox.UpdateMovesSelection(currentMove,playerUnit.Pet.Moves[currentMove]);


    }
    void HandleActionConfirm(){

       
         if(state == NewBattleState.PlayerAction){
              if(currentAction ==0){
                //Fight
                PlayerMove();
            }else if(currentAction == 1){
                //Run
            }
         }else if(state == NewBattleState.PlayerMove ){
              dialogBox.EnableMoveSelector(false);
              dialogBox.EnableDialogText(true);
              StartCoroutine(PerformPlayerMove());
          }
         
    }

  


   
}
