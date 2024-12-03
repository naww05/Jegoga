using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public Text countdownText; // Referensi ke UI Text untuk menampilkan countdown
    public Text staticText; // Referensi ke UI Text untuk teks statis
    public GameManager gameManager; // Referensi ke GameManager
    public Image backgroundPanel; // Referensi ke Image Panel sebagai background

    private LTPNoteMovement[] allLTPNotes; // Array dari semua note dengan LTPNoteMovement
    private NoteMovement[] allNotes; // Array dari semua note dengan NoteMovement

    private void Start()
    {
        allLTPNotes = FindObjectsOfType<LTPNoteMovement>(); // Temukan semua LTPNote di scene
        allNotes = FindObjectsOfType<NoteMovement>(); // Temukan semua NoteMovement di scene
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        staticText.gameObject.SetActive(true); // Aktifkan teks statis
        backgroundPanel.gameObject.SetActive(true); // Aktifkan background panel

        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
        }

        countdownText.text = "MULAI";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
        staticText.gameObject.SetActive(false); // Nonaktifkan teks statis
        backgroundPanel.gameObject.SetActive(false); // Nonaktifkan background panel

        gameManager.StartGame();
        ResumeAllNotes();
    }

    private void ResumeAllNotes()
    {
        foreach (LTPNoteMovement note in allLTPNotes)
        {
            note.ResumeNote(); // Lanjutkan semua note dengan LTPNoteMovement yang dipause
        }

        foreach (NoteMovement note in allNotes)
        {
            note.Resume(); // Lanjutkan semua note dengan NoteMovement yang dipause
        }
    }
}
