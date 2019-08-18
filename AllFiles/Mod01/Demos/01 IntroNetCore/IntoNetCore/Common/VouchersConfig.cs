namespace IntroNetCore
{
    //http://json2csharp.com/ -> json to c# generieren
    public class AppConfig
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string ApplicationName { get; set; }
        public Authentication Authentication { get; set; }
    }

    public class ConnectionStrings
    {
        public string LocalDBConnection { get; set; }
        public string SQLServerDBConnection { get; set; }
    }

    public class Facebook
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }

    public class Authentication
    {
        public Facebook Facebook { get; set; }
    }
}