using System;
using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour, IWall
{
    public Action show = delegate { };

    [SerializeField] WallData wallData;
    private WallLogic wallLogic;

    public bool Check
    {
        get
        {
            return wallData.check;
        }
        set
        {
            wallData.check = value;
        }
    }

    public WallLogic WallLogic { get => wallLogic; set => wallLogic = value; }
    private void Awake()
    {
        Check = true;
        wallLogic = new WallLogic(wallData);
    }

    public void isMoving()
    {
        StartCoroutine(CheckedMoving());
    }

    /// <summary>
    /// Проверка движутся ли части преграды
    /// </summary>
    /// <param name="cubes">Массив преград</param>
    /// <returns></returns>
    public IEnumerator CheckedMoving()
    {
        while (Check)
        {
            foreach (var item in wallData.cubes)
            {
                if (item.rb.IsSleeping())
                {
                    if (item.rb.IsSleeping())
                    {
                        Check = false;
                    }
                    else Check = true;
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        show();
    }
}