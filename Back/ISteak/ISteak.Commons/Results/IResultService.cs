namespace ISteak.Commons.Results
{
    public interface IResultService
    {
        IList<ResultMessage> AllMessages { get; set; }
        IList<ResultMessage> Errors { get; set; }
        IList<ResultMessage> Successes { get; set; }
        IList<ResultMessage> Warnings { get; set; }
        bool AddMessages(IEnumerable<ResultMessage> messages);
        bool AddMessages(params ResultMessage[] messages);
        bool AddMessage(ResultMessage message);
        bool HasMessages();
        bool HasMessages(ResultMessage message);
        bool HasErrors();
        bool HasSuccesses();
        bool HasWarnings();
        void ClearMessages();
        bool IsValid();
    }
}