using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem{
    public interface IQuestObjective 
    {
        string Title {get;}
        string Description { get; }
        bool IsComplete { get; }
        bool IsBonus { get; }
        void UpdateProgress();
        void CheckProgress();
    
    }
}

