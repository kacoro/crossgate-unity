using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleHud : MonoBehaviour
{
   [SerializeField] Text nameText;
   [SerializeField] Text levelText;
   [SerializeField] Text statusText;
   [SerializeField] HPBar hpBar;

   [SerializeField] Color psnColor;
   [SerializeField] Color brnColor;
   [SerializeField] Color slpColor;
   [SerializeField] Color parColor;
   [SerializeField] Color frzColor;

    Pet _pet;
    Dictionary<ConditionID,Color> statusColors;
    
       public void setData(Pet pet){
       _pet = pet;
       nameText.text = pet.Base.Name;
       levelText.text = "Lv" + pet.Level;
       hpBar.SetHP((float)pet.HP / pet.MaxHp);

        statusColors = new Dictionary<ConditionID, Color>(){
            {ConditionID.psn,psnColor},
            {ConditionID.brn,brnColor},
            {ConditionID.slp,slpColor},
            {ConditionID.par,parColor},
            {ConditionID.frz,frzColor}
        };

       SetStatusText();
       _pet.OnStatusChanged  += SetStatusText;
   }
    void SetStatusText(){
        if(_pet.Status == null){
            statusText.text = "";
        }else{
            statusText.text = _pet.Status.Id.ToString().ToUpper();
            statusText.color = statusColors[_pet.Status.Id];
        }
    }
   public IEnumerator UpdateHP(){
    //    hpBar.SetHP((float)_pet.HP / _pet.MaxHp);
        if(_pet.HpChanged){
            yield return hpBar.SetHPSmooth((float)_pet.HP / _pet.MaxHp);
            _pet.HpChanged = false;
        }
       
   }
}
