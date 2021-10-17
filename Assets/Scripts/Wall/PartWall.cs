using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PartWall : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    BoxCollider bCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bCollider = GetComponent<BoxCollider>();
    }

    /// <summary>
    /// ���������� ���������� ������ ��� ����� ��������
    /// </summary>
    public void isActivePhysics()
    {
        rb.useGravity = !rb.useGravity;
        rb.isKinematic = !rb.isKinematic;
        bCollider.isTrigger = !bCollider.isTrigger;
    }
}
