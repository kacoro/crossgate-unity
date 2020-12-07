namespace QuestSystem{
    public interface IQuestIdentifier 
    {
       int ID { get; }
       int ChainQuestID { get; }
       int SourceID { get; }
    }
}
