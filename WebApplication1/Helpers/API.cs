namespace PostingManagement.UI.Helpers
{
    public class API
    {
        public static class Home
        {
            public static string GetDemoService(string baseUri) => $"{baseUri}/DemoService";
        }
    }
}
