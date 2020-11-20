using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHp;
    public int currentHp;

    public bool TakeDamage(int dmg){
        currentHp -= dmg;

        if(currentHp <=0){
            currentHp = 0;
            return true;
        }else
            return false;
    }

    public void Heal(int amount){
        currentHp +=amount;
        if(currentHp > maxHp){
            currentHp = maxHp;
        }
    }
}
