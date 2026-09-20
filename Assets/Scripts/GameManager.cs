using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text messageText;

    private bool gameOver;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        if (messageText != null) messageText.gameObject.SetActive(false);
    }

    public void Win()
    {
        EndGame("¡Ganaste! Atrapaste al payaso\nPresioná R para reiniciar");
    }

    public void Lose()
    {
        EndGame("¡Perdiste! El ninja te atrapó\nPresioná R para reiniciar");
    }

    void EndGame(string message)
    {
        if (gameOver) return;
        gameOver = true;

        if (messageText != null)
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    void Update()
    {
        if (gameOver && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}