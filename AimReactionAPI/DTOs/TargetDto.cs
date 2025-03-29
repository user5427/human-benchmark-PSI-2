using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class TargetDto(int size, int x, int y)
{
    public int Size { get; set; } = size;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}

