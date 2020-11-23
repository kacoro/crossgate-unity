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

    
    private PlayerInputActions controls;
    private Vector2 move;
    private Animator enemyAnmi;
    private Animator playerAnim;
    private void Awake() {
        controls = new PlayerInputActions();
        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled  += ctx => move = Vector2.zero;
        controls.GamePlay.Ok.started +=ctx => OnAttackButton();

        // animatedObject.SetActive(true);
        // animatedObject.myAnimator.SetBool("MyVariable", true);
       
         
         
    }
    void Start()
    {
        state = BattleState.START;
      
       StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {

        
       
        if(state == BattleState.PLAYERTURN){
           
        }
    }
    
     private void OnEnable() {
        controls.GamePlay.Enable();
    }

     private void OnDisable() {
         controls.GamePlay.Disable();
     }

    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();
        playerAnim = playerGo.GetComponentInChildren<Animator>();
        playerAnim.SetFloat("MoveX",-1);
         playerAnim.SetFloat("MoveY",1);
        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        
         enemyAnmi = enemyGo.GetComponent<Animator>();
         enemyAnmi.SetFloat("MoveX",1);
         enemyAnmi.SetFloat("MoveY",-1);
        //  anmi.SetBool("isRuning",true);
        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    
    IEnumerator PlayerAttack(){
        playerAnim.SetBool("isAttack",true);
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
         enemyHUD.SetHP(enemyUnit.currentHp);
         dialogueText.text = "The attack is successfull";
        yield return new WaitForSeconds(1f); //攻击有8帧
         playerAnim.SetBool("isAttack",false);
        if(isDead){
            //End
            state = BattleState.WON;
            playerAnim.SetBool("isVictory",true);
            enemyAnmi.SetBool("isDeath",true);
            EndBattle();
        }else{
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
           
            // Enemy turn
        }
    }

    IEnumerator PlayerHeal(){
       
        playerAnim.SetBool("isMagic",true);
        yield return new WaitForSeconds(1.5f);
        playerAnim.SetBool("isMagic",false);
         playerUnit.Heal(5);
        playerHUD.SetHP(playerUnit.currentHp);
        dialogueText.text = "You feel renewed strength！";
        yield return new WaitForSeconds(1f);
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
        // anmi.SetBool("isRuning",false);
        enemyAnmi.SetBool("isAttack",true);
    //     AnimatorStateInfo stateinfo = anmi.GetCurrentAnimatorStateInfo(0);
    //     if (stateinfo.IsName("Attack")&& (stateinfo.normalizedTime > 1.0f))
    //    {
    //         anmi.SetBool("isAttack",false);
    //    }
       
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHp);

         yield return new WaitForSeconds(1f);
        enemyAnmi.SetBool("isAttack",false);
        if(isDead){
            enemyAnmi.SetBool("isVictory",true);
            playerAnim.SetBool("isDeath",true);
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
