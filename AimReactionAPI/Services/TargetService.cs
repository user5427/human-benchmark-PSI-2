using AimReactionAPI.Models;

namespace AimReactionAPI.Services;

public class TargetService
{
    public List<Target> GenerateTargets(int maxTargets = 10, int targetSpeed = 10)
    {
        var targets = new List<Target>();
        for (int i = 0; i < maxTargets; i++)
        {
            targets.Add(GenerateTarget(targetSpeed));
        }
        return targets;
    }

    public static Target GenerateTarget(int targetSpeed = 0)
    {
        return new Target
        {
            X = new Random().Next(0, 100),
            Y = new Random().Next(0, 100),
            Size = new Random().Next(1, 10),
            Speed = targetSpeed
        };
    }
}