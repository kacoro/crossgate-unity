using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
   [SerializeField] GameObject health;

 

   public void SetHP(float hpNoramlized){
       health.transform.localScale = new Vector3(hpNoramlized,1f);
   }
}
