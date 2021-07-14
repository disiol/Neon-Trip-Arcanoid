using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector2 ballInitialForce;

    // GameObject
    public GameObject playerObject;

    // используйте этот метод для инициализации
    void Start()
    {
        // создаем силу
        ballInitialForce = new Vector2(100.0f, 300.0f);

        // переводим в неактивное состояние
        ballIsActive = false;

        // запоминаем положение
        ballPosition = transform.position;
    }

    void Update()
    {
        // проверка нажатия на пробел
        if (Input.GetButtonDown("Jump") == true)
        {
            chexkStatus();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!PlayerScript.lose && !PlayerScript.win)
            {
                chexkStatus();
            }
            else
            {
                Application.LoadLevel("Level1");
            }
        }

        pozition();

        // проверка падения шара
        cheakBoll();
    }


    private void chexkStatus()
    {
        // проверка состояния
        if (!ballIsActive)
        {
            // сброс всех сил
            GetComponent<Rigidbody2D>().isKinematic = false;
            // применим силу
            GetComponent<Rigidbody2D>().AddForce(ballInitialForce);
            // зададим активное состояние
            ballIsActive = !ballIsActive;
        }
    }

    private void cheakBoll()
    {
        if (ballIsActive && transform.position.y < -6)
        {
            ballIsActive = !ballIsActive;
            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = -3.83f;
            transform.position = ballPosition;

            GetComponent<Rigidbody2D>().isKinematic = true;

            playerObject.SendMessage("TakeLife");
        }
    }

    private void pozition()
    {
        if (!ballIsActive && playerObject != null)
        {
            // задаем новую позицию шарика
            ballPosition.x = playerObject.transform.position.x;

            // устанавливаем позицию шара
            transform.position = ballPosition;
        }
    }
}