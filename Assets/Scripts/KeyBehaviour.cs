using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBehaviour : MonoBehaviour
{
    public GameObject MyText;
    public int a;
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
            a++;
            MyText.GetComponent<Text>().text = "�����: " + a.ToString();
        }
    }
}
