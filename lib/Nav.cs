using PaintedPoker.Game.Services;

public static class Nav
{
    public const string CreateGame = "createGame";
    public const string Games = "games";
    public static string Game(GameName gameName) => string.Concat(Games, "/", gameName.FormattedName);
}