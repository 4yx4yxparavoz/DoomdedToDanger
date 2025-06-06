using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int collected = 0;
    public int total = 10;

    public PlayerController playerController;
    public CameraController cameraController;
    public Text counterText;
    public GameObject winPanel;

    void Awake()
    {
        instance = this;
        winPanel.SetActive(false);
        UpdateUI();
    }

    public void CollectItem()
    {
        collected++;
        UpdateUI();
    }

    void UpdateUI()
    {
        counterText.text = $"Собрано: {collected}/{total}";
    }


    void ShowWinScreen()
    {
        winPanel.SetActive(true);

        if (playerController != null)
            playerController.enabled = false;

        if (cameraController != null)
            cameraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MusicManager.instance.StopMusic();
    }

}
