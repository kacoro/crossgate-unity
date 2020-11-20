using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;
    public BattleState state;

    public BattleHub playerHUD;
    public BattleHub enemyHUD;

    void Start()
    {
        state = BattleState.START;
       StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {

    }
    


    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();
        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    
    IEnumerator PlayerAttack(){
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
         enemyHUD.SetHP(enemyUnit.currentHp);
         dialogueText.text = "The attack is successfull";
        yield return new WaitForSeconds(2f);

        if(isDead){
            //End
            state = BattleState.WON;
            EndBattle();
        }else{
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            // Enemy turn
        }
    }

    IEnumerator PlayerHeal(){
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHp);
        dialogueText.text = "You feel renewed strength！";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    void PlayerTurn(){
        dialogueText.text = "Choose an action：";
    }

    void EndBattle(){
        if(state == BattleState.WON){
            dialogueText.text = "You won the battle!";
        }else if(state == BattleState.LOST){
            dialogueText.text = "You were defeated.";
            //exit the battle
        }
    }

    IEnumerator EnemyTurn(){
        //AI
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHp);

         yield return new WaitForSeconds(1f);

        if(isDead){
            state = BattleState.LOST;
            EndBattle();
        }else{
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    public void OnAttackButton(){
        if(state != BattleState.PLAYERTURN){
            return;
        }
        StartCoroutine(PlayerAttack());
    }

    public void OHealButton(){
        if(state != BattleState.PLAYERTURN){
            return;
        }
        StartCoroutine(PlayerHeal());
    }

}
