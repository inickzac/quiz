namespace Teams.Domain
{
    public class Question : Entity
    {
        public string Text { get; private set; }

        public Question(string text)
        {
            Text = text;
        }
    }
}
