using UnityEngine;
using DG.Tweening;

public class WallLogic
{
    PartWall[] partWalls;
    Vector3[] startPosition;
    Sequence sequence;
        
    public WallLogic(IPointsWall points)
    {
        partWalls = points.partWalls;
        startPosition = SetStartPosition(points.partWalls);
    }

    /// <summary>
    /// Получаем начальные положения для преграды
    /// </summary>
    /// <param name="cubes">Части преграды</param>
    /// <returns>Начальные положения</returns>
    public Vector3[] SetStartPosition(params PartWall[] cubes)
    {
        Vector3[] startPosition = new Vector3[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            startPosition[i] = cubes[i].transform.position;
        }
        return startPosition;
    }

    /// <summary>
    /// Отключение физики
    /// </summary>
    void isPhysics(params PartWall[] cubes)
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].isActivePhysics();
        }
    }

    /// <summary>
    /// Создание анимации восстановления
    /// </summary>
    /// <param name="cubes">Массив частей преграды</param>
    /// <returns>очередь из анимаций</returns>
    public Sequence TweenAnim()
    {
        sequence = DOTween.Sequence();
        sequence.AppendCallback(() => isPhysics(partWalls));
        for (int i = 0; i < partWalls.Length; i++)
        {
            sequence.Join(partWalls[i].transform.DOJump(startPosition[i], .5f, 1, 1));
            sequence.Join(partWalls[i].transform.DORotate(Vector3.zero, 0.5f, RotateMode.Fast));
        }
        sequence.AppendCallback(() => isPhysics(partWalls));
        
        return sequence;
    }
}
