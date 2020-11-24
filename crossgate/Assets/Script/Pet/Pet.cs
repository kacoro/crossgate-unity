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
}
