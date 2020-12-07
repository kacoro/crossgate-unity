
using System;
namespace QuestSystem{
    public class QuestText : IQuestText
    {
            private string title;
            private string descriptionSummary;
            private string hint;
            private string dialog;
         
            
            public string Title { get {return title;}}

            public string DescriptionSummary { get {return descriptionSummary;}}

            public string Hint{ get {return hint;}}

            public string Dialog { get {return dialog;}}
    }
}