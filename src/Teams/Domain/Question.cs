namespace Teams.Domain
{
    public class Question : Entity
    {
        public string Text { get; protected set; }

        public Question(string text)
        {
            Text = text;
        }
    }
}
