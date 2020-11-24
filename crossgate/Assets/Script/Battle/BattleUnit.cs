using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleUnit : MonoBehaviour
{
   [SerializeField] PetBase _base;
   [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;
    public Pet Pet {get;set;}

   public void Setup(){
       Pet = new Pet(_base,level);
       if(isPlayerUnit){
           GetComponent<Image>().sprite = Pet.Base.Sprite;
       }else{
           GetComponent<Image>().sprite = Pet.Base.Sprite;
       }
   }
}
