using TMPro;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    [Header("External References")]
    public TextMeshProUGUI timeText;
    public GameObject pauseMenu;

    [Header("General properties")]
    private float timeElapsed;
    private float updateInterval = 1f;
    private float elapsedTime;
    public bool isPaused;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UpdateClockText();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= updateInterval)
        {
            UpdateClockText();
            timeElapsed = 0;
        }
    }
    private void UpdateClockText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        string currentTime = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        timeText.text = currentTime;
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (!isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
