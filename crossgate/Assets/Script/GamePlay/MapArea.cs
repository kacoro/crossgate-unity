using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
  [SerializeField]List<Pet> wildPets;

  public Pet GetRandomWildPet(){
      var wildPet = wildPets[Random.Range(0,wildPets.Count)];
      wildPet.Init();
      return wildPet;
  }
}
