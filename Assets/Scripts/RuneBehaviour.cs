using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneBehaviour : MonoBehaviour
{
    // ��������� �������� ��� ��������� � ���� �������
    void OnTriggerEnter2D(Collider2D collision)
    {
        /**
        * ���������, ��� �������, ��������������� �������
        * ���� ��� ��� "Player", �� ������� ���������
        **/
        if (collision.gameObject.tag == "Player")
        {
            // ���������� ��� ������
            Destroy(gameObject);


        }
    }
}
