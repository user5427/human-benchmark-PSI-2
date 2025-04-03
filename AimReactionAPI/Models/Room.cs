using AimReactionAPI.Services;

namespace AimReactionAPI.Models;

public enum RoomStatus
{
    WAITING,
    PLAYING
}

public class Room
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public int CreatorId { get; set; }
    public HashSet<int> Players { get; set; }
    public HashSet<int> AllowedPlayers { get; set;}
    public Dictionary<int, double> PlayerTimes { get; set; } = [];
    public RoomStatus RoomStatus { get; set; } 
    public GameVisibility RoomVisibility { get; set; } 

    public Room(Guid id, int creatorId, string roomName, GameVisibility visibility, HashSet<int> allowedPlayers)
    {
        Id = id;
        CreatorId = creatorId;
        Name = roomName;
        RoomVisibility = visibility;
        Players = [creatorId]; 
        AllowedPlayers = allowedPlayers ?? [];
        AllowedPlayers.Add(CreatorId); 
    }

    public bool AddToRoom(int userId)
    {
        if (RoomStatus == RoomStatus.WAITING)
        {
            return Players.Add(userId);
        }
        return false;
    }
    public bool RemoveFromRoom(int userId)
    {
        return Players.Remove(userId);
    }
    public void RegisterPlayerHit(int userId, double reactionTime)
    {
        PlayerTimes.Add(userId, reactionTime);
    }
    public void ResetPlayerTimes()
    {
        PlayerTimes.Clear();
    }
}