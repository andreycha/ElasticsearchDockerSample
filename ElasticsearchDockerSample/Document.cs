namespace ElasticsearchDockerSample
{
    public class Document
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return $"[Text: '{Text}']";
        }
    }
}
