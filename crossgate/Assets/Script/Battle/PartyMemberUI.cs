using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PartyMemberUI : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    [SerializeField] Color highlightedColor;

    Pet _pet;
    public void setData(Pet pet)
    {
        _pet = pet;
        nameText.text = pet.Base.Name;
        levelText.text = "Lv" + pet.Level;
        hpBar.SetHP((float)pet.HP / pet.MaxHp);
    }

    public IEnumerator UpdateHP()
    {
        //    hpBar.SetHP((float)_pet.HP / _pet.MaxHp);
        yield return hpBar.SetHPSmooth((float)_pet.HP / _pet.MaxHp);
    }

    public void SetSelected(bool selected){
        if(selected){
            nameText.color = highlightedColor;
        }else{
            nameText.color = Color.white;
        }
    }
}
