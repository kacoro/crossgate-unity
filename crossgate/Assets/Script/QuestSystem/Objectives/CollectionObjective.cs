using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem{
    public class CollectionObjective : IQuestObjective
    {
        public string title;
        private string description;
        private bool isComplete;
        private bool isBonus;
        private string verb;
        private int colectionAmount;    //total amount of whatever we need
        private int currentAmount {get;set;} // starts at 0
        private GameObject itemToCollect;

        //collect 10 meat
        /// <summary>
        /// This construncttor builds a collection objective.
        /// </summary>
        /// <param name="titleVerb">Describes the type of collection.</param>
        /// <param name="totalAmount">Amount required to complete objective.</param>
        /// <param name="item">Item to be collected.</param>
        /// <param name="descript">Describe what will be collected.</param>
        /// <param name="bonus">Is this a bonus objecitve?</param>
        public CollectionObjective(string titleVerb,int totalAmount,GameObject item, string descript, bool bonus){
            title = $"{titleVerb} {totalAmount} {item.name}";
            verb = titleVerb;
            description = descript;
            itemToCollect = item;
            colectionAmount = totalAmount;
            currentAmount = 0;
            isBonus = bonus;
            CheckProgress();
        }

        public string Title { get { return title;} }

        public string Description  { get { return description;} }
      
        public bool IsComplete  { get { return isComplete;} }

        public bool IsBonus  { get { return isBonus;} }

        public int ColectionAmount  { get { return colectionAmount;} }

        public int CurrentAmount  { 
            get { return currentAmount;}  
         }

         public void UpdateCurrentAmount(int amount){
             currentAmount = amount;
             CheckProgress();
         }

        public GameObject ItemToCollect  { get { return itemToCollect;} }

        public void CheckProgress()
        {
           if(currentAmount >= colectionAmount)
                isComplete = true;
            else
                isComplete = false;
        }

        // public override bool Equals(object obj)
        // {
        //     return base.Equals(obj);
        // }

        // public override int GetHashCode()
        // {
        //     return base.GetHashCode();
        // }

        // 0/10 meat gathered 
        public override string ToString()
        {
            return currentAmount + "/" + colectionAmount + " " + itemToCollect.name + " " + verb + "ed!";
        }

        public void UpdateProgress()
        {
           
        }
    }
}   

