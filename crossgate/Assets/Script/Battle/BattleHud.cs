using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleHud : MonoBehaviour
{
   [SerializeField] Text nameText;
   [SerializeField] Text levelText;
   [SerializeField] HPBar hpBar;

   public void setData(Pet pet){
       nameText.text = pet.Base.Name;
       levelText.text = "Lv" + pet.Level;
       hpBar.SetHP((float)pet.HP / pet.MaxHp);
   }
}
