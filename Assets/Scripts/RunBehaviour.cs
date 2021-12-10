using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBehaviour : MonoBehaviour
{
    /**
    ** ��������� ������
    **/
    [Header("Player velocity")]
    // ��� Ox
    public int xVelocity = 5;
    // ��� Oy
    public int yVelocity = 8;

    [SerializeField] private LayerMask ground;

    // ���������� ��������� (����) �������
    private Rigidbody2D rigidBody;
    // ���������, �������� �� ������������
    private Collider2D coll;

    private void Start()
    {
        // �������� ������ � Rigidbody2D ������� Player
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
    }

    private void Update()
    {
        updatePlayerPosition();
    }

    // ��������� �������������� ������
    private void updatePlayerPosition()
    {
        // �������� �������� ����� ��������������� �����������
        float moveInput = Input.GetAxis("Horizontal");
        // �������� �������� ����� ������
        float jumpInput = Input.GetAxis("Jump");

        // �������� xVelocity, yVelocity ����� ������ ����� ���������

        if (moveInput < 0)
        { // ���� �����
            rigidBody.velocity = new Vector2(-xVelocity, rigidBody.velocity.y);
        }
        else if (moveInput > 0)
        { // ���� ������
            rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
        }
        else if (coll.IsTouchingLayers(ground))
        {
            rigidBody.velocity = Vector2.zero; // ����� ���� ���� ����� ������� ���� ��� ����������� ���������, ���������
        }

        if (jumpInput > 0 && coll.IsTouchingLayers(ground))
        { //��� �������, ���� ����� �� �����
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity);
        }
    }
}