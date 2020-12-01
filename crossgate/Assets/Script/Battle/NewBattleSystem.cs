using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum NewBattleState { Start, ActionSelection, MoveSelection, RunningTurn, Busy, PartyScreen, BattleOver }

public enum BattleAction {Move,SwitchPet,UseItem,Run}

public class NewBattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] PartyScreen partyScreen;

    public event Action<bool> OnBattleOver;

    NewBattleState state;
    NewBattleState? prevState;
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

    void BattleOver(bool won)
    {
        state = NewBattleState.BattleOver;
        playerParty.Pets.ForEach(p => p.OnBattleOver());
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

    IEnumerator RunTurns(BattleAction playerAction){
        state = NewBattleState.RunningTurn;
        if(playerAction == BattleAction.Move){
            playerUnit.Pet.CurrentMove = playerUnit.Pet.Moves[currentMove];
            enemyUnit.Pet.CurrentMove = enemyUnit.Pet.GetRandomMove();

            //Check who goes first
            bool playerGoesFirst = playerUnit.Pet.Speed >= enemyUnit.Pet.Speed;

            var firstUnit = (playerGoesFirst)? playerUnit:enemyUnit;
            var secondUnit = (playerGoesFirst)? enemyUnit:playerUnit;

            var secondPet = secondUnit.Pet;


            //First Turn
            yield return RunMove(firstUnit,secondUnit,firstUnit.Pet.CurrentMove);
            yield return RunAfterTurn(firstUnit);
            if(state == NewBattleState.BattleOver) yield break;
            
            if(secondPet.HP > 0){

           
            //Second Turn
             yield return RunMove(secondUnit,firstUnit,secondUnit.Pet.CurrentMove);
             yield return RunAfterTurn(secondUnit);
            if(state == NewBattleState.BattleOver) yield break;
             }


        }else{
            if(playerAction == BattleAction.SwitchPet){
                var selectedPet = playerParty.Pets[currentMember];
                state= NewBattleState.Busy;
                yield return SwitchPet(selectedPet);
            }

            //Enemy Turn
            var enemyMove = enemyUnit.Pet.GetRandomMove();
            yield return RunMove(enemyUnit,playerUnit,enemyMove);
             yield return RunAfterTurn(enemyUnit);
            if(state == NewBattleState.BattleOver) yield break;
        }
        if(state != NewBattleState.BattleOver){
            ActionSelection();
        }
    }


    IEnumerator ShowStatusChanges(Pet pet)
    {
        while (pet.StatusChanges.Count > 0)
        {
            var message = pet.StatusChanges.Dequeue();
            yield return dialogBox.TypeDialog(message);
        }

    }

 

    void CheckForBattleOver(BattleUnit faintedUnit)
    {
        if (faintedUnit.IsPlayerUnit)
        {
            var nextPet = playerParty.GetHealthyPet();
            if (nextPet != null)
            {
                OpenPartyScreen();
            }
            else
            {
                BattleOver(false);
            }
        }
        else
        {
            BattleOver(true);
        }
    }

    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit, Move sourceMove)
    {

        bool canRunMove = sourceUnit.Pet.OnBeoreMove();
        if(!canRunMove){
            yield return ShowStatusChanges(sourceUnit.Pet);
            yield return sourceUnit.Hud.UpdateHP();
            yield break;
        }
        yield return ShowStatusChanges(sourceUnit.Pet);

        sourceMove.PP--;
        yield return dialogBox.TypeDialog($"{sourceUnit.Pet.Base.Name} use {sourceMove.Base.Name}");

        if(CheckIfMoveHits(sourceMove,sourceUnit.Pet,targetUnit.Pet)){

            sourceUnit.PlayAttackAnimation();
            yield return new WaitForSeconds(1f);

            targetUnit.PlayHitAnimation();

            if (sourceMove.Base.Category == MoveCategory.Status)
            {
                yield return RunMoveEffects(sourceMove.Base.Effects, sourceUnit.Pet, targetUnit.Pet,sourceMove.Base.Target);

            }
            else
            {
                var damageDetails = targetUnit.Pet.TakeDamage(sourceMove, sourceUnit.Pet);
                yield return targetUnit.Hud.UpdateHP();
                yield return ShowDamageDetails(damageDetails);
            }

            if(sourceMove.Base.Secondaries != null && sourceMove.Base.Secondaries.Count >0 && targetUnit.Pet.HP > 0 ){
                foreach (var secondary in sourceMove.Base.Secondaries){
                    var rnd = UnityEngine.Random.Range(1,101);
                    if(rnd <= secondary.Chance)
                        yield return RunMoveEffects(secondary, sourceUnit.Pet, targetUnit.Pet,secondary.Target);
                }
            }

            if (targetUnit.Pet.HP <= 0)
            {
                yield return dialogBox.TypeDialog($"{targetUnit.Pet.Base.Name} Fainted");
                targetUnit.PlayFaintAnimation();
                yield return new WaitForSeconds(2f);

                CheckForBattleOver(targetUnit);
            }
        }
        else{
            yield return dialogBox.TypeDialog($"{sourceUnit.Pet.Base.Name}'s attack missed");
        }
       
    }

    IEnumerator RunAfterTurn(BattleUnit sourceUnit){
        if(state == NewBattleState.BattleOver) yield break;
        yield return new WaitUntil(()=> state == NewBattleState.RunningTurn);

        sourceUnit.Pet.OnAfterTurn();
        yield return ShowStatusChanges(sourceUnit.Pet);
        yield return sourceUnit.Hud.UpdateHP();
         if (sourceUnit.Pet.HP <= 0)
        {
            yield return dialogBox.TypeDialog($"{sourceUnit.Pet.Base.Name} Fainted");
            sourceUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);

            CheckForBattleOver(sourceUnit);
        }

    }

    IEnumerator RunMoveEffects(MoveEffects effects, Pet source, Pet target,MoveTarget moveTarget)
    {
       
        // Stat Boosting
        if (effects.Boosts != null)
        {
            if (moveTarget == MoveTarget.Self)
            {
                source.AppllyBoost(effects.Boosts);
            }
            else
            {
                target.AppllyBoost(effects.Boosts);
            }
        }

        //Status Condition
        if(effects.Status != ConditionID.none){
            target.SetStatus(effects.Status);
        }

        //Volatile Status Condition
        if(effects.VolatileStatus != ConditionID.none){
            target.SetVolatileStatus(effects.VolatileStatus);
        }

        yield return ShowStatusChanges(source);
        yield return ShowStatusChanges(target);
    }

    bool CheckIfMoveHits(Move move,Pet source,Pet target){

        if(move.Base.AlwayssHits) return true;
        float moveAccuracy = move.Base.Accuracy;

        int accuracy = source.StatBoosts[Stat.Accuracy];
        int evasion = target.StatBoosts[Stat.Evasion];

         var boostValues = new float[] {1f,4f /3f,5f /3f,2f,7f/3f,8f/3f,3f};

         if(accuracy >0){
             moveAccuracy *= boostValues[accuracy];
         }else{
             moveAccuracy /= boostValues[-accuracy];
         }

        if(evasion >0){
             moveAccuracy /= boostValues[evasion];
         }else{
             moveAccuracy *= boostValues[-evasion];
         }

        return UnityEngine.Random.Range(1,101) <= moveAccuracy;
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
                prevState = state;
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
            StartCoroutine(RunTurns(BattleAction.Move));
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
            if(prevState == NewBattleState.ActionSelection){
                prevState = null;
                StartCoroutine(RunTurns(BattleAction.SwitchPet));
            }else{
                 state = NewBattleState.Busy;
                StartCoroutine(SwitchPet(selectedMember));
            }
           
        }
    }

    IEnumerator SwitchPet(Pet newPet)
    {
        if (playerUnit.Pet.HP > 0)
        {
            yield return dialogBox.TypeDialog($"Come back{playerUnit.Pet.Base.Name}");
            playerUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
        }
        playerUnit.Setup(newPet);
        dialogBox.SetMoveNames(newPet.Moves);
        yield return dialogBox.TypeDialog($"Go {newPet.Base.Name}");

       state = NewBattleState.RunningTurn;

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
