using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsDB 
{
    public static void Init(){
        foreach (var kvp in Conditions){
            var conditionId = kvp.Key;
            var condition = kvp.Value;
            condition.Id =conditionId;
        }
    }
    public static Dictionary<ConditionID,Condition> Conditions {get; set; } = new Dictionary<ConditionID, Condition>{
        {
            ConditionID.psn,
            new Condition(){
                Name = "Poison",
                StartMessage = "has been poisoned",
                OnAfterTurn = (Pet pet) =>{
                    pet.UpdateHP(pet.MaxHp/8);
                    pet.StatusChanges.Enqueue($"{pet.Base.Name} hurt itself due to poison");
                }
            }    
        },
        {
            ConditionID.brn,
            new Condition(){
                Name = "Burn",
                StartMessage = "has been Burned",
                OnAfterTurn = (Pet pet) =>{
                    pet.UpdateHP(pet.MaxHp/16);
                    pet.StatusChanges.Enqueue($"{pet.Base.Name} hurt itself due to burn");
                }
            }    
        },
        {
            ConditionID.par,
            new Condition(){
                Name = "Paralyzed",
                StartMessage = "has been Paralyzed",
                OnBeforeMove = (Pet pet) => {
                    if(Random.Range(1,5) ==1){
                        pet.StatusChanges.Enqueue($"{pet.Base.Name}'s paralyzed and can't move");
                        return false;
                    }
                    return true;
                }
            }    
        },
        {
            ConditionID.frz,
            new Condition(){
                Name = "Freeze",
                StartMessage = "has been frozen",
                OnBeforeMove = (Pet pet) => {
                    if(Random.Range(1,5) ==1){
                        pet.CureStatus();
                        pet.StatusChanges.Enqueue($"{pet.Base.Name}'s is not frozen anymore");
                        return true;
                    }
                    return false;
                }
            }    
        },
        {
            ConditionID.slp,
            new Condition(){
                Name = "Sleep",
                StartMessage = "has been asleep",
                OnStart = (Pet pet) =>{
                    //sleep for 1-3 turns
                    pet.StatusTime = Random.Range(1,4);
                    Debug.Log($"Will be asleep for {pet.StatusTime} moves");

                },
                OnBeforeMove = (Pet pet) => {
                    if(pet.StatusTime <= 0){
                        pet.CureStatus();
                        pet.StatusChanges.Enqueue($"{pet.Base.Name} is woke up!");
                        return true;
                    }
                    pet.StatusTime --;
                    pet.StatusChanges.Enqueue($"{pet.Base.Name} is Sleeping");
                    return false;
                }
            }    
        },
        // volatile Status Conditions 
        {
            ConditionID.confusion,
            new Condition(){
                Name = "Confusion",
                StartMessage = "has been confused",
                OnStart = (Pet pet) =>{
                    //sleep for 1-4 turns
                    pet.VolatileStatusTime = Random.Range(1,5);
                    Debug.Log($"Will be confused for {pet.VolatileStatusTime} moves");

                },
                OnBeforeMove = (Pet pet) => {
                    if(pet.VolatileStatusTime <= 0){
                        pet.CureVolatilesStatus();
                        pet.StatusChanges.Enqueue($"{pet.Base.Name} kicked out of confusion!");
                        return true;
                    }

                    
                    pet.VolatileStatusTime --;

                    //50% chance to do a move
                    if(Random.Range(1,3) == 1)
                        return true;

                    pet.StatusChanges.Enqueue($"{pet.Base.Name} is Sleeping");
                    pet.UpdateHP(pet.MaxHp/8);
                    pet.StatusChanges.Enqueue($"It hurt itself due to confusion");
                    return false;
                }
            }    
        }
    };
}


public enum ConditionID{
    none,psn,brn,slp,par,frz, //毒伤，烧伤，睡眠，麻痹，冻结,混乱
    confusion
}