using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class TargetResponse(int x, int y) : BaseDto
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}

