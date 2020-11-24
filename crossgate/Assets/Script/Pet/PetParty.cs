using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PetParty : MonoBehaviour
{
    [SerializeField] List<Pet> pets;

    private void Start() {
        foreach(var pet in pets){
            pet.Init();
        }
    }

    public Pet GetHealthyPet(){
        return pets.Where(x => x.HP >0).FirstOrDefault();
    }
}
