using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject ingameCanvas;
    public GameObject pauseCanvas;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Debug.Log("Oyun duraklatıldı.");
        isPaused = true;
        Time.timeScale = 0f; // Oyun zamanını duraklat
        ingameCanvas.SetActive(false); // IngameCanvas'ı devre dışı bırak
        pauseCanvas.SetActive(true); // PauseCanvas'ı aktif hale getir
    }

    public void ResumeGame()
    {
        Debug.Log("Oyun devam ettirildi.");
        isPaused = false;
        Time.timeScale = 1f; // Oyun zamanını normale döndür
        pauseCanvas.SetActive(false); // PauseCanvas'ı devre dışı bırak
        ingameCanvas.SetActive(true); // IngameCanvas'ı aktif hale getir
    }
}
