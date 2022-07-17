using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public static class DirectionHelper
{
    static DirectionHelper()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    public static Direction? Roll() => (Direction)Random.Range(0, 3);

    public static IEnumerable<Direction> GetAllStartingWith(Direction direction)
        => new[] { direction }.Concat(System.Enum.GetValues(typeof(Direction)).Cast<Direction>().Except(new[] { direction }));
}
