using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class GameLogic 
{
    public delegate void inputPlayer();
    public inputPlayer InputPlayer;

    public Button resetBtn;
    public RectTransform aimObject;
    public Rigidbody bullet;
    public Wall wall;

    bool shot = false;
    Animator animator;
    Camera mainCamera;
    Vector3 positionAim;

    private static Vector3 cursorWorldPosOnNCP
    {
        get
        {
#if UNITY_EDITOR
            return Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.nearClipPlane));
#elif UNITY_ANDROID
            Vector3 position = Vector3.zero;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                position = Camera.main.ScreenToWorldPoint(
                new Vector3(touch.position.x,
                touch.position.y,
                Camera.main.nearClipPlane));
            }
            return position;
#endif
        }
    }

    private static Vector3 cameraToCursor
    {
        get
        {
            return cursorWorldPosOnNCP - Camera.main.transform.position;
        }
    }

    private Vector3 cursorOnTransform
    {
        get
        {
            Vector3 camToTrans = aimObject.transform.position - Camera.main.transform.position;
            return Camera.main.transform.position +
                cameraToCursor *
                (Vector3.Dot(Camera.main.transform.forward, camToTrans) / Vector3.Dot(Camera.main.transform.forward, cameraToCursor));
        }
    }

    public void ShowBtn()
    {
        resetBtn.gameObject.SetActive(true);
        shot = false;
    }

    public void Init(GameObject GO)
    {
#if UNITY_EDITOR
        InputPlayer = InputPlayerEd;
#elif UNITY_ANDROID
        InputPlayer = InputPlayerAn;
#endif

        wall.show += ShowBtn;
        mainCamera = Camera.main;
        animator = GO.GetComponent<Animator>();

        resetBtn.onClick.AddListener(() =>
        {
            if (!wall.Check)
            {
                wall.WallLogic.TweenAnim().Play();
                wall.Check = true;
            }
        });
    }

    public void DirectionShot()
    {
        shot = true;
        wall.isMoving();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = mainCamera.transform.position;
        bullet.velocity = -cursorOnTransform.normalized * 25;
    }

#if UNITY_EDITOR
    void InputPlayerEd()
    {
        if (!resetBtn.gameObject.activeSelf && !shot)
        {
            if (Input.GetMouseButton(0))
            {
                positionAim = Input.mousePosition;
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetTrigger("Show");
                }
                aimObject.position = positionAim;

            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetTrigger("Hiden");
                if (EventSystem.current.currentSelectedGameObject == null)
                    DirectionShot();
            }
        }
    }
#elif UNITY_ANDROID

    void InputPlayerAn()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                animator.SetTrigger("Show");
            if (touch.phase == TouchPhase.Moved && !shot)
            {
                aimObject.position = touch.position;
            }
            if (touch.phase == TouchPhase.Ended && !resetBtn.gameObject.activeSelf && !shot)
            {
                animator.SetTrigger("Hiden");
                if (EventSystem.current.currentSelectedGameObject == null)
                    DirectionShot();
            }
        }
    }
#endif
}
