using AimReactionAPI.Services;

namespace AimReactionAPI.Models;

public enum RoomStatus
{
    WAITING,
    PLAYING
}

public class Room(Guid Id, int creatorId, string roomName)
{
    public Guid Id { get; set; } = Id;
    public string Name { get; set; } = roomName;
    public int CreatorId { get; set; } = creatorId;
    public HashSet<int> Players { get; set; } = [creatorId];
    public Dictionary<int, double> PlayerTimes { get; set; } = [];
    public RoomStatus RoomStatus { get; set; } = RoomStatus.WAITING;
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