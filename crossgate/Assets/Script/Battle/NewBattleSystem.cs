using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum NewBattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class NewBattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleUnit EnemyUnit;
    [SerializeField] BattleHud EnemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    public event Action<bool> OnBattleOver;

    NewBattleState state;
    private Vector2 move;
    int currentAction;
    int currentMove;
    private PlayerInputActions controls;
    public void StartBattle()
    {
        StartCoroutine(SetupBattle());
    }
    private void Awake()
    {
        controls = new PlayerInputActions();
        // controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //  controls.GamePlay.Move.performed += ctx => HandleSelection(ctx.ReadValue<Vector2>());
        // controls.GamePlay.Move.canceled  += ctx => move = Vector2.zero;

        controls.GamePlay.Select.performed += ctx => HandleSelection(ctx.ReadValue<Vector2>());
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
        PlayerAction();
    }

    void PlayerAction()
    {
        state = NewBattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);

    }

  
    void HandleSelection(Vector2 content)
    {
        move = content;
        if (state == NewBattleState.PlayerAction)
        {
            HandleActionSelection();
        }

        if (state == NewBattleState.PlayerMove)
        {
            HandleMovesSelection();
        }
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    public void HandleUpdate(){

    }

    void PlayerMove()
    {
        state = NewBattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = NewBattleState.Busy;
        var playerMove = playerUnit.Pet.Moves[currentMove];
        playerMove.PP--;
        yield return dialogBox.TypeDialog($"{playerUnit.Pet.Base.Name} use {playerMove.Base.Name}");

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        EnemyUnit.PlayHitAnimation();

        var damageDetails = EnemyUnit.Pet.TakeDamage(playerMove, playerUnit.Pet);
        yield return EnemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{EnemyUnit.Pet.Base.Name} Fainted");
            EnemyUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        }
        else
        {
            StartCoroutine(enemyMove());
        }
    }

    IEnumerator enemyMove()
    {
        state = NewBattleState.EnemyMove;
        var randomMove = EnemyUnit.Pet.GetRandomMove();
        randomMove.PP--;
        yield return dialogBox.TypeDialog($"{EnemyUnit.Pet.Base.Name} use {randomMove.Base.Name}");

        EnemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();

        var damageDetails = playerUnit.Pet.TakeDamage(randomMove, EnemyUnit.Pet);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Pet.Base.Name} Fainted");
            playerUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleOver(false);
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog($" A Critical hit");
        }
        if (damageDetails.TypeEffectiveness > 1f)
            yield return dialogBox.TypeDialog($"It's A super effective!");
        else if (damageDetails.TypeEffectiveness < 1f)
            yield return dialogBox.TypeDialog($"It's not very effective!");
    }

    void HandleActionSelection()
    {
        if (move.y < 0)
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if (move.y > 0)
        {
            if (currentAction > 0)
                --currentAction;
        }
        dialogBox.UpdateActionSelection(currentAction);
    }
    void HandleMovesSelection()
    {

        if (move.x > 0)
        {
            Debug.Log(move.x);
            if (currentMove < playerUnit.Pet.Moves.Count - 1)
                ++currentMove;
        }
        else if (move.x < 0)
        {
            Debug.Log(move.x);
            if (currentMove > 0)
                --currentMove;
        }
        else if (move.y < 0)
        {
            if (currentMove < playerUnit.Pet.Moves.Count - 2)
                currentMove += 2;
        }
        else if (move.y > 0)
        {
            if (currentMove > 1)
                currentMove -= 2;
        }
        dialogBox.UpdateMovesSelection(currentMove, playerUnit.Pet.Moves[currentMove]);


    }
    void HandleActionConfirm()
    {

        if (state == NewBattleState.PlayerAction)
        {
            if (currentAction == 0)
            {
                //Fight
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                //Run
            }
        }
        else if (state == NewBattleState.PlayerMove)
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }

}
