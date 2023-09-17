namespace ISteak.Commons.Results
{
    public class ResultService : IResultService
    {
        public IList<ResultMessage> AllMessages { get; set; }
        public IList<ResultMessage> Errors { get; set; }
        public IList<ResultMessage> Successes { get; set; }
        public IList<ResultMessage> Warnings { get; set; }

        public ResultService()
        {
            this.ClearMessages();
        }

        public bool AddMessages(IEnumerable<ResultMessage> messages)
        {
            var result = false;
            foreach (var item in messages)
            {
                if (this.AddMessage(item))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool AddMessages(params ResultMessage[] messages)
        {
            var result = false;
            foreach (var item in messages)
            {
                if (this.AddMessage(item))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool AddMessage(ResultMessage message)
        {
            if (message == null)
                return false;

            this.AllMessages.Add(message);

            switch (message.Type)
            {
                case ResultMessageTypes.Error:
                    this.Errors.Add(message);
                    break;
                case ResultMessageTypes.Success:
                    this.Successes.Add(message);
                    break;
                case ResultMessageTypes.Warning:
                    this.Warnings.Add(message);
                    break;
                default:
                    this.Warnings.Add(message);
                    break;
            }

            return true;
        }

        public bool HasMessages()
        {
            return !this.AllMessages.Count().Equals(0);
        }

        public bool HasMessages(ResultMessage message)
        {
            if (this.AllMessages == null)
                return false;

            return this.AllMessages.Any(item => item.Code == message.Code);
        }

        public bool HasErrors()
        {
            return !this.Errors.Count().Equals(0);
        }

        public bool HasSuccesses()
        {
            return !this.Successes.Count().Equals(0);
        }

        public bool HasWarnings()
        {
            return !this.Warnings.Count().Equals(0);
        }

        public void ClearMessages()
        {
            this.AllMessages = new List<ResultMessage>();
            this.Errors = new List<ResultMessage>();
            this.Successes = new List<ResultMessage>();
            this.Warnings = new List<ResultMessage>();
        }

        public bool IsValid()
        {
            return !this.HasErrors();
        }

    }
}
