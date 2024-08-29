using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int multiplier = 1;
    int streak = 0;

    public RockMeter rockMeter;
    public AudioSource musicSource; // Referensi ke AudioSource untuk musik
    private bool gameStarted = false;

    public int loseSceneIndex = 8;
    public int winSceneIndex = 7;

    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("RockMeter", 20);
        musicSource.Pause(); // Musik berhenti saat game dimulai
    }

    void Update()
    {
        if (!gameStarted) return;

        // Tambahkan logika permainan di sini yang ingin dijalankan saat game dimulai
    }

    public void StartGame()
    {
        gameStarted = true;
        musicSource.Play(); // Mulai musik
    }

    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
    }

    void UpdateGUI()
    {
        PlayerPrefs.SetInt("Streak", streak);
        PlayerPrefs.SetInt("Multi", multiplier);
    }

    public void AddStreak()
    {
        int rockMeterValue = PlayerPrefs.GetInt("RockMeter");
        if (rockMeterValue + 1 < 50)
            PlayerPrefs.SetInt("RockMeter", rockMeterValue + 1);

        streak++;
        if (streak >= 24)
            multiplier = 4;
        else if (streak >= 16)
            multiplier = 3;
        else if (streak >= 8)
            multiplier = 2;
        else
            multiplier = 1;

        UpdateGUI();
        rockMeter.UpdateNeedle();
    }

    public void ResetStreak()
    {
        int rockMeterValue = PlayerPrefs.GetInt("RockMeter");
        PlayerPrefs.SetInt("RockMeter", rockMeterValue - 2);
        if (rockMeterValue - 2 < 0)
            Lose();

        streak = 0;
        multiplier = 1;
        UpdateGUI();
        rockMeter.UpdateNeedle();
    }

    void Lose()
    {
        SceneManager.LoadScene(loseSceneIndex);
    }

    public void Win()
    {
        if (PlayerPrefs.GetInt("HighScore") < PlayerPrefs.GetInt("Score"))
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        SceneManager.LoadScene(winSceneIndex);
    }

    public int GetScore()
    {
        return 10 * multiplier;
    }
}
