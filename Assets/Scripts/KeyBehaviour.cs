using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBehaviour : MonoBehaviour
{
    public GameObject MyText;
    public int a;
    // Активация триггера при попадании в него объекта
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        /**
        * Проверяем, тэг объекта, активировавшего триггер
        * Если его тэг "Player", то условия выполнено
        **/
        if (collision.gameObject.tag == "Player")
        {
            // Уничтожаем наш объект
            Destroy(gameObject);
            a++;
            MyText.GetComponent<Text>().text = "Ключи: " + a.ToString();
        }
    }
}
