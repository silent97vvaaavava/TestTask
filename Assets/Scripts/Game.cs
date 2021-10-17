using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Game : MonoBehaviour
{
    [SerializeField] GameLogic gameLogic;
    public Wall WallObj => gameLogic.wall;


    private void Awake()
    {
        gameLogic.Init(gameObject);
    }

    private void Update()
    {
        gameLogic.InputPlayer();
    }
}
