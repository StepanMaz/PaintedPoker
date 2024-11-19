public static class Nav
{
    public const string CreateGame = "/create-game";
    public const string Index = "/";
    public const string Games = "games";
    public static string Game(string gameName) => string.Concat(Games, "/", gameName);
}