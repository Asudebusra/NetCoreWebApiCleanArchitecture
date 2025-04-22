namespace App.Repositories
{
    public class ConnectionStringOption
    {
        //SqlServer,Redis

        public const string Key = "ConnectionStrings";
        public string SqlServer { get; set; } = default!; // nullable olamaz  
    }
}
