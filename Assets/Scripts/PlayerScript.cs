using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public float playerVelocity;
    private Vector3 playerPosition;
    public float boundary;
    public float spead;

    private int playerLives;
    private int playerPoints;
    public static bool win;
    public static bool lose;
    private string lifesSurese;

    // используйте этот метод для инициализации
    void Start()
    {
        lifesSurese = "Live's: " + playerLives + "  Score: " + playerPoints;

        lose = false;
        win = false;
        // получим начальную позицию платформы
        playerPosition = gameObject.transform.position;

        playerLives = 3;
        playerPoints = 0;
    }

    // Update вызывается при отрисовке каждого кадра игры
    void Update()
    {
        WinLose();
        // горизонтальное движение

        // выход из игры
        Exit();

        if (!lose && !win)
        {
            playerPosition.x += Input.GetAxis("Horizontal") * playerVelocity;

            // обновим позицию платформы
            transform.position = playerPosition;

            // проверка выхода за границы
            if (playerPosition.x < -boundary)
            {
                transform.position = new Vector3(-boundary, playerPosition.y, playerPosition.z);
            }

            if (playerPosition.x > boundary)
            {
                transform.position = new Vector3(boundary, playerPosition.y, playerPosition.z);
            }
        }
    }

    private static void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.x = mousePos.x > 2.5f ? 2.5f : mousePos.x;
        mousePos.x = mousePos.x < -2.5f ? -2.5f : mousePos.x;
        if (!lose && !win)
        {
            playerPosition = Vector2.MoveTowards(playerPosition,
                new Vector2(mousePos.x, playerPosition.y),
                spead * Time.deltaTime);
        }
    }

    void addPoints(int points)
    {
        playerPoints += points;
    }

    void TakeLife()
    {
        playerLives--;
    }

    void WinLose()
    {
        Lose();

        Win();
    }

    private void Lose()
    {
        // перезапускаем уровень
        if (playerLives == 0)
        {
            lose = true;
            // Application.LoadLevel("Level1");
        }

        Debug.Log("lose = " + lose);
    }

    private void Win()
    {
        // все блоки уничтожены
        if ((GameObject.FindGameObjectsWithTag("Block")).Length == 0)
        {
            win = true;
            // проверяем текущий уровень
            // if (Application.loadedLevelName == "Level1")
            // {
            //     Application.LoadLevel("Level2");
            // }
            // else
            // {
            //    
            // }
            Debug.Log("win = " + win);
        }
    }

    void OnGUI()
    {
        int widthScreen = Screen.width;
        int xCenter = (widthScreen / 2);
        int heightScreen = Screen.height;
        int yCenter = (heightScreen / 2);

        int width = 800;
        int height = 800;

        GUI.depth = 1;
        GUIStyle font = new GUIStyle(GUI.skin.GetStyle("label"));
        font.fontSize = 150;
        font.normal.textColor = Color.green;
        font.alignment = TextAnchor.UpperRight;
        font.alignment = TextAnchor.UpperCenter;


        if (!lose && !win)
        {
            GUI.Label(new Rect(xCenter - height / 2, yCenter - 1000, width, height),
                "Live's: " + playerLives + "  Score: " + playerPoints, font);
        }
        else if (lose)
        {
            GUI.Label(new Rect(xCenter - height / 2  , yCenter -1000 , width , height+200),
                "Game over  Score: " + playerPoints + " tap to screen for restart", font);
        }
        else if (win)
        {
            GUI.Label(new Rect(xCenter - height / 2 , yCenter - 1000, width , height + 200),
                "You win  Score: " + playerPoints + " tap to screen for restart", font);
        }
    }
}