using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pet
{

    [SerializeField] PetBase _base;
    [SerializeField] int level;
    public PetBase Base { get{
        return _base;
    } }
    public int Level { get{
        return level;
    } }

    public int HP { get; set; }  //short form 
    public List<Move> Moves { get; set; }

    public Dictionary<Stat,int> Stats {get; private set;}  
    public Dictionary<Stat,int> StatBoosts {get; private set;} 

    public Condition Status {get;private set;}
    public int StatusTime{get;set;}

    public Condition VolatileStatus{get;private set;}

     public int VolatileStatusTime{get; set;}
    public Queue<string> StatusChanges {get;private set;} = new Queue<string>();
    public bool HpChanged {get;set;}

    public event System.Action OnStatusChanged;
    
    public void Init()
    { 
       
        Moves = new List<Move>();

        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }
        CalculateStats();
        HP = MaxHp;

        ResetStatBoost();
        Status = null;
        VolatileStatus = null;
    }

    void CalculateStats(){
        Stats = new Dictionary<Stat, int>();
        Stats.Add(Stat.Attack,Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5);
        Stats.Add(Stat.Defense,Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5);
        Stats.Add(Stat.SpAttack,Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5);
        Stats.Add(Stat.SpDefense,Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5);
        Stats.Add(Stat.Speed,Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5);

        MaxHp = Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10 +Level;
    }

    void ResetStatBoost(){
        StatBoosts = new Dictionary<Stat, int>(){
            {Stat.Attack,0},
            {Stat.Defense,0},
            {Stat.SpAttack,0},
            {Stat.SpDefense,0},
            {Stat.Speed,0},
            {Stat.Accuracy,0},
            {Stat.Evasion,0}
        };
    }
    
    int GetStat(Stat stat){
        int statVal = Stats[stat];

        // TODO: Apply stat boost
        int boost = StatBoosts[stat];
        var boostValues = new float[] {1f,1.5f,2f,2.5f,3f,3.5f,4f};

        if(boost >= 0){
            statVal = Mathf.FloorToInt(statVal * boostValues[boost]);
        }else{
            statVal = Mathf.FloorToInt(statVal / boostValues[-boost]);
        }

        return statVal;
    }

    public void AppllyBoost(List<StatBoost> statBoosts){
        foreach (var statBoost in statBoosts){
            var stat = statBoost.stat;
            var boost = statBoost.boost;
            StatBoosts[stat] = Mathf.Clamp( StatBoosts[stat] + boost,-6,6);

            if(boost > 0){
                StatusChanges.Enqueue($"{Base.Name}'s {stat} rose!");
            }else{
                StatusChanges.Enqueue($"{Base.Name}'s {stat} fell!");
            }

            Debug.Log($"{stat} has been bossted to {StatBoosts[stat]}");
        }
    }

    public int Attack
    {
        get { return GetStat(Stat.Attack); }
    }

    public int Defense
    {
        get { return GetStat(Stat.Defense); }
    }
    public int SpAttack
    {
        get { return  GetStat(Stat.SpAttack); }
    }
    public int SpDefense
    {
        get { return  GetStat(Stat.SpDefense); }
    }
    public int Speed
    {
        get { return GetStat(Stat.Speed); }
    }

    public int MaxHp
    {
        get; private set;
    }
    

    public DamageDetails TakeDamage(Move move, Pet attacker)
    {
        float critical = 1f;
        if (Random.value * 100f <= 6.25f)
            critical = 2f;
        //float type = TypeChart.GetEffectiveness(move.Base.Type,this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type,this.Base.Type2);
        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type) * critical;

        var damageDetails = new DamageDetails()
        {
            TypeEffectiveness = type,
            Critical = critical,
            Fainted = false
        };

        float attack = (move.Base.Category == MoveCategory.Special) ? attacker.SpAttack : attacker.Attack;
        float defance = (move.Base.Category == MoveCategory.Special) ? SpDefense : Defense;

        float modifiers = Random.Range(0.85f, 1f) * type;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attack / defance) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);
        // HP -= damage;
        // if (HP <= 0)
        // {
        //     HP = 0;
        //     damageDetails.Fainted = true;
        // }
        UpdateHP(damage);
        return damageDetails;
    }

    public void UpdateHP(int damage){
        HP = Mathf.Clamp(HP - damage,0,MaxHp);
        HpChanged = true;
    }

    public void SetStatus(ConditionID conditionId){
        if(Status != null) return;

        Status = ConditionsDB.Conditions[conditionId];
        Status?.OnStart?.Invoke(this);
        StatusChanges.Enqueue($"{Base.Name} {Status.StartMessage}");
        OnStatusChanged?.Invoke();
    }

    public void CureStatus(){
        Status = null;
        OnStatusChanged?.Invoke();
    }

     public void SetVolatileStatus(ConditionID conditionId){ //only battle is over 
        if(VolatileStatus != null) return;

        VolatileStatus = ConditionsDB.Conditions[conditionId];
        VolatileStatus?.OnStart?.Invoke(this);
        StatusChanges.Enqueue($"{Base.Name} {VolatileStatus.StartMessage}");
    }

    public void CureVolatilesStatus(){
        VolatileStatus = null;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }



    public bool OnBeoreMove(){
        bool canPerformMove = true;
        if(Status?.OnBeforeMove!=null){
            if(!Status.OnBeforeMove(this)){
                canPerformMove = false;
            }
        }

        if(VolatileStatus?.OnBeforeMove!=null){
            if(!VolatileStatus.OnBeforeMove(this)){
                canPerformMove = false;
            }
        }
        return canPerformMove;
    }

    public void OnAfterTurn(){
        Status?.OnAfterTurn?.Invoke(this);
        VolatileStatus?.OnAfterTurn?.Invoke(this);
    }

    public void OnBattleOver(){
        VolatileStatus = null;
        ResetStatBoost();
    }
}



public class DamageDetails
{
    public bool Fainted { get; set; } //晕倒
    public float Critical { get; set; } //临界值
    public float TypeEffectiveness { get; set; }

}
