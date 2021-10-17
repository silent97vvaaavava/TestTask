using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(1, 5)] float timeLife = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Invoke("Hiden", 0.1f);
        }
    }

    void Hiden()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Invoke("Hiden", timeLife);
        }
    }
}
