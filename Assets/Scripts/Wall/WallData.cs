using DG.Tweening;
using UnityEngine;

[System.Serializable]
public struct WallData: IPointsWall
{
    public PartWall[] cubes;
    [HideInInspector] public bool check;
    public static bool shot = false;
    [HideInInspector] public Vector3[] startPosition;

    public PartWall[] partWalls => cubes;
}

public interface IPointsWall
{
    PartWall[] partWalls { get; }
}

