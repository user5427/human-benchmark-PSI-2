using Fleck;

namespace AimReactionAPI.Models;

public class Player(string username, IWebSocketConnection connection)
{
    public string Username { get; set; } = username;
    public IWebSocketConnection Connection { get; set; } = connection;
}