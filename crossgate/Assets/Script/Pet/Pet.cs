using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet 
{
   public PetBase Base {get;set;}
   public int Level  {get;set;}

    public int HP {get;set;}  //short form 
    public List<Move> Moves {get;set;}

   public Pet(PetBase pBase, int pLevel){ //constructor
       Base = pBase;
       Level = pLevel;
        HP = MaxHp;
       Moves = new List<Move>();

       foreach (var move in Base.LearnableMoves ){
           if(move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if(Moves.Count >=4)
                break;
       }
   }

  
   public int Attack{
       get { return Mathf.FloorToInt((Base.Attack * Level)/100f) + 5;}
   }

   public int Defense{
       get { return Mathf.FloorToInt((Base.Defense * Level)/100f) + 5;}
   }
   public int SpAttack{
       get { return Mathf.FloorToInt((Base.SpAttack * Level)/100f) + 5;}
   }
   public int SpDefense{
       get { return Mathf.FloorToInt((Base.SpDefense * Level)/100f) + 5;}
   }
   public int MaxHp{
       get { return Mathf.FloorToInt((Base.MaxHp * Level)/100f) + 10;}
   }
   public int Speed{
       get { return Mathf.FloorToInt((Base.Speed * Level)/100f) + 5;}
   }

   public bool TakeDamage(Move move, Pet attacker){
       float modifiers = Random.Range(0.85f,1f);
       float a = (2 * attacker.Level +10) /250f;
       float d = a * move.Base.Power * ((float)attacker.Attack /Defense) + 2 ;
       int damage = Mathf.FloorToInt(d * modifiers);
        HP -= damage;
        if(HP <= 0){
            HP = 0;
            return true;
        }

        return false;
   }

   public Move GetRandomMove(){
       int r = Random.Range(0,Moves.Count);
       return Moves[r];
   }
}
