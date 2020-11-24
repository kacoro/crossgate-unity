using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum NewBattleState { Start, ActionSelection, MoveSelection, PerformMove, Busy, PartyScreen,BattleOver }

public class NewBattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] PartyScreen partyScreen;

    public event Action<bool> OnBattleOver;

    NewBattleState state;
    private Vector2 move;
    int currentAction = 0;
    int currentMove = 0;

    int currentMember;

    private PlayerInputActions controls;

    PetParty playerParty;
    Pet wildPet;

    private void Awake()
    {
        controls = new PlayerInputActions();
        // controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //  controls.GamePlay.Move.performed += ctx => HandleSelection(ctx.ReadValue<Vector2>());
        // controls.GamePlay.Move.canceled  += ctx => move = Vector2.zero;

        controls.GamePlay.Select.performed += ctx => HandleSelection(ctx.ReadValue<Vector2>());
        controls.GamePlay.Ok.performed += ctx => HandleActionConfirm();
        controls.GamePlay.Cancel.performed += ctx => HandleActionCancel();
    }
    public void StartBattle(PetParty playerParty, Pet wildPet)
    {
        this.playerParty = playerParty;
        this.wildPet = wildPet;
        StartCoroutine(SetupBattle());
    }
    public IEnumerator SetupBattle()
    {
        playerUnit.Setup(playerParty.GetHealthyPet());
        enemyUnit.Setup(wildPet);

        partyScreen.Init();

        dialogBox.SetMoveNames(playerUnit.Pet.Moves);

        // dialogBox.SetDialog($"A wild {enemyUnit.Pet.Base.Name} appeared.");
        //    StartCoroutine(dialogBox.TypeDialog($"A wild {enemyUnit.Pet.Base.Name} appeared."));
        yield return dialogBox.TypeDialog($"A wild {enemyUnit.Pet.Base.Name} appeared.");
        ActionSelection();
    }

    void BattleOver(bool won){
        state = NewBattleState.BattleOver;
        OnBattleOver(won);
    }

    void ActionSelection()
    {
        state = NewBattleState.ActionSelection;
        dialogBox.SetDialog("Choose an action");
        // StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }
    void OpenPartyScreen()
    {
        state = NewBattleState.PartyScreen;
        partyScreen.SetPartyData(playerParty.Pets);
        partyScreen.gameObject.SetActive(true);
    }


    void HandleSelection(Vector2 content)
    {
        move = content;
        if (state == NewBattleState.ActionSelection)
        {
            HandleActionSelection();
        }
        else if (state == NewBattleState.MoveSelection)
        {
            HandleMovesSelection();
        }
        else if (state == NewBattleState.PartyScreen)
        {
            HandlePartyScreenSelection();
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

    public void HandleUpdate()
    {

    }

    void MoveSelection()
    {
        state = NewBattleState.MoveSelection;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PlayerMove()
    {
        state = NewBattleState.PerformMove;
        var playerMove = playerUnit.Pet.Moves[currentMove];
        yield return RunMove(playerUnit,enemyUnit,playerMove);

        // If the battle stat was not changed by RunMove, the go to next step
        if(state == NewBattleState.PerformMove)
            StartCoroutine(enemyMove());
    }

    IEnumerator enemyMove()
    {
        state = NewBattleState.PerformMove;
        var randomMove = enemyUnit.Pet.GetRandomMove();
        yield return RunMove(enemyUnit,playerUnit,randomMove);
         // If the battle stat was not changed by RunMove, the go to next step
        if(state == NewBattleState.PerformMove)  
             ActionSelection();
    }

    void CheckForBattleOver(BattleUnit faintedUnit){
        if(faintedUnit.IsPlayerUnit){
             var nextPet = playerParty.GetHealthyPet();
            if (nextPet != null)
            {
                OpenPartyScreen();
            }else{
                BattleOver(false);
            }
        }else{
            BattleOver(true);
        }
    }

    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit,Move sourceMove){
         sourceMove.PP--;
        yield return dialogBox.TypeDialog($"{sourceUnit.Pet.Base.Name} use {sourceMove.Base.Name}");

        sourceUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        targetUnit.PlayHitAnimation();

        var damageDetails = targetUnit.Pet.TakeDamage(sourceMove, sourceUnit.Pet);
        yield return targetUnit.Hud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{targetUnit.Pet.Base.Name} Fainted");
            targetUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);

            CheckForBattleOver(targetUnit);
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
        if (move.x > 0)
            ++currentAction;
        else if (move.x < 0)
            --currentAction;
        else if (move.y < 0)
            currentAction += 2;
        else if (move.y > 0)
            currentAction -= 2;

        currentAction = Mathf.Clamp(currentAction, 0, 3);
        dialogBox.UpdateActionSelection(currentAction);
    }
    void HandleMovesSelection()
    {
        if (move.x > 0)
            ++currentMove;
        else if (move.x < 0)
            --currentMove;
        else if (move.y < 0)
            currentMove += 2;
        else if (move.y > 0)
            currentMove -= 2;

        currentMove = Mathf.Clamp(currentMove, 0, playerUnit.Pet.Moves.Count - 1);
        dialogBox.UpdateMovesSelection(currentMove, playerUnit.Pet.Moves[currentMove]);


    }
    void HandleActionConfirm()
    {

        if (state == NewBattleState.ActionSelection)
        {
            if (currentAction == 0)
            {
                //Fight
                MoveSelection();
            }
            else if (currentAction == 1)
            {
                //Bag
            }
            else if (currentAction == 2)
            {
                //Pet
                OpenPartyScreen();
            }
            else if (currentAction == 3)
            {
                //Run
            }
        }
        else if (state == NewBattleState.MoveSelection)
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PlayerMove());
        }
        else if (state == NewBattleState.PartyScreen)
        {
            var selectedMember = playerParty.Pets[currentMember];
            if (selectedMember.HP <= 0)
            {
                partyScreen.SetMessageText("You can't send out a fainted Pet");
                return;
            }
            if (selectedMember == playerUnit.Pet)
            {
                partyScreen.SetMessageText("You can't switch with the same Pet");
                return;
            }
            partyScreen.gameObject.SetActive(false);
            state = NewBattleState.Busy;
            StartCoroutine(SwitchPet(selectedMember));
        }
    }

    IEnumerator SwitchPet(Pet newPet)
    {
        if(playerUnit.Pet.HP > 0 ){
            yield return dialogBox.TypeDialog($"Come back{playerUnit.Pet.Base.Name}");
            playerUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
        }
        playerUnit.Setup(newPet);
        dialogBox.SetMoveNames(newPet.Moves);
        yield return dialogBox.TypeDialog($"Go {newPet.Base.Name}");

        StartCoroutine(enemyMove());

    }

    void HandleActionCancel()
    {
        if (state == NewBattleState.MoveSelection)
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            ActionSelection();
        }
        else if (state == NewBattleState.PartyScreen)
        {

            partyScreen.gameObject.SetActive(false);
            ActionSelection();

        }
    }

    void HandlePartyScreenSelection()
    {
        if (move.x > 0)
            ++currentMember;
        else if (move.x < 0)
            --currentMember;
        else if (move.y < 0)
            currentMember += 2;
        else if (move.y > 0)
            currentMember -= 2;

        currentMember = Mathf.Clamp(currentMember, 0, playerParty.Pets.Count - 1);
        partyScreen.UpdateMemberSelection(currentMember);
    }

}
