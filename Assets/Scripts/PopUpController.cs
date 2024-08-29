using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpController : MonoBehaviour
{
    public GameObject PopUpPanel; // Panel pop-up
    public GameObject QuitPanel; // Panel konfirmasi keluar
    public AudioSource jegogMusic; // AudioSource untuk musik Jegog
    private bool isPaused = false;

    // Array untuk menyimpan semua script yang perlu dijeda
    private JegogController[] jegogBars;
    private NoteMovement[] noteMovement;
    private Activator[] jegogs;

    void Start()
    {
        jegogBars = FindObjectsOfType<JegogController>(); // Temukan semua objek JegogBar di scene
        noteMovement = FindObjectsOfType<NoteMovement>(); // Temukan semua objek NoteController di scene
        jegogs = FindObjectsOfType<Activator>();

        if (jegogMusic == null)
        {
            // Temukan AudioSource jika belum diatur melalui Inspector
            GameObject jegogSongObject = GameObject.Find("JegogSong");
            if (jegogSongObject != null)
            {
                jegogMusic = jegogSongObject.GetComponent<AudioSource>();
            }
        }
    }

    public void OpenPopUpPanel()
    {
        // Menampilkan pop up panel
        PopUpPanel.SetActive(true);
        Time.timeScale = 0; // Menjeda waktu di dalam game
        JegogController.isPaused = true; // Set static variable isPaused to true
        isPaused = true;
        

        // Menjeda semua note
        foreach (NoteMovement note in noteMovement)
        {
            note.Pause(); // Pastikan Anda memiliki fungsi Pause() di NoteController
        }

        foreach (Activator jegog in jegogs)
        {
            jegog.Pause(); // Pastikan Anda memiliki fungsi Pause() di NoteController
        }

        // Menjeda musik
        if (jegogMusic != null && jegogMusic.isPlaying)
        {
            jegogMusic.Pause();
        }
    }

    public void ClosePopUpPanel()
    {
        PopUpPanel.SetActive(false);
        JegogController.isPaused = false; // Set static variable isPaused to false
        isPaused = false;
        Time.timeScale = 1; // Melanjutkan waktu di dalam game

        foreach (Activator jegog in jegogs)
        {
            jegog.Resume();
        }

        foreach (NoteMovement note in noteMovement)
        {
            note.Resume();
        }

        // Melanjutkan musik
        if (jegogMusic != null && !jegogMusic.isPlaying)
        {
            jegogMusic.UnPause();
        }

    }

    public void ShowQuitConfirmation()
    {
        PopUpPanel.SetActive(false);
        QuitPanel.SetActive(true);
    }

    public void ConfirmQuit()
    {
        QuitPanel.SetActive(false);
        Time.timeScale = 1; // Ensure time scale is reset to 1
        JegogController.isPaused = false; // Set static variable isPaused to false
        SceneManager.LoadScene("MainMenu"); // Ganti "MainMenu" dengan nama scene menu utama Anda
    }

    public void CancelQuit()
    {
        QuitPanel.SetActive(false);
        JegogController.isPaused = false; // Set static variable isPaused to false
        isPaused = false;
        Time.timeScale = 1; // Melanjutkan waktu di dalam game

        foreach (Activator jegog in jegogs)
        {
            jegog.Resume();
        }

        foreach (NoteMovement note in noteMovement)
        {
            note.Resume();
        }

        // Melanjutkan musik
        if (jegogMusic != null && !jegogMusic.isPlaying)
        {
            jegogMusic.UnPause();
        }
    }
}
