using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float gameDuration = 900f;
    private float remainingTime;

    public CameraController cameraController;
    public Text timerText;
    public GameObject losePanel;

    private bool gameEnded = false;

    void Start()
    {
        remainingTime = gameDuration;
        losePanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        remainingTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (remainingTime <= 0f)
        {
            EndGame();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        gameEnded = true;
        losePanel.SetActive(true);
        Time.timeScale = 0f;
        if (cameraController != null)
            cameraController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
