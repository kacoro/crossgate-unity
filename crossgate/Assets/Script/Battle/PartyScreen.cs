using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PartyScreen : MonoBehaviour
{
    [SerializeField] Text messageText;
    PartyMemberUI[] memberSolts;
    List<Pet> pets;

    public void Init()
    {
        memberSolts = GetComponentsInChildren<PartyMemberUI>();
    }

    public void SetPartyData(List<Pet> pets)
    {
        this.pets = pets;
        for (int i = 0; i < memberSolts.Length; i++)
        {
            if (i < pets.Count)
            {
                memberSolts[i].setData(pets[i]);
            }
            else
            {
                memberSolts[i].gameObject.SetActive(false);

            }
        }

        messageText.text = "Choose a pet.";
    }

    public void UpdateMemberSelection(int selectedMember){
        for (int i = 0 ; i < pets.Count ; i++){
            if(i == selectedMember){
                memberSolts[i].SetSelected(true);
            }else{
                memberSolts[i].SetSelected(false);
            }
        }
    }

    public void SetMessageText(string message){
        messageText.text = message;
    }
}
