using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTPActivator : MonoBehaviour
{
    private List<GameObject> notesInTrigger = new List<GameObject>();
    private GameObject GameManager;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("JegogSong").GetComponent<AudioSource>();
    }

    void Update()
    {
        // Deteksi sentuhan pada layar
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                // Cek apakah sentuhan terjadi pada objek bambu
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        if (notesInTrigger.Count > 0) // Cek apakah ada note di dalam trigger
                        {
                            DestroyNotesInTrigger();
                            ResumeAllNotes();
                            ResumeAudio();
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            notesInTrigger.Add(other.gameObject);
            HighlightNoteTile(other.gameObject, true);

            // Menghentikan pergerakan note saat berada dalam trigger
            LTPNoteMovement noteMovement = other.GetComponent<LTPNoteMovement>();
            if (noteMovement != null)
            {
                noteMovement.StopNote();
                PauseAllNotesExcept(noteMovement);
                PauseAudio();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            notesInTrigger.Remove(other.gameObject);
            HighlightNoteTile(other.gameObject, false);

            // Melanjutkan pergerakan note saat keluar dari trigger
            LTPNoteMovement noteMovement = other.GetComponent<LTPNoteMovement>();
            if (noteMovement != null)
            {
                noteMovement.ResumeNote();
            }
        }
    }

    void DestroyNotesInTrigger()
    {
        foreach (GameObject note in notesInTrigger)
        {
            Destroy(note);
        }
        notesInTrigger.Clear();
    }

    void HighlightNoteTile(GameObject note, bool highlight)
    {
        Renderer renderer = note.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (highlight)
            {
                renderer.material.color = Color.red;
            }
            else
            {
                renderer.material.color = Color.white;
            }
        }
    }

    void PauseAllNotesExcept(LTPNoteMovement activeNote)
    {
        LTPNoteMovement[] allNotes = FindObjectsOfType<LTPNoteMovement>();
        foreach (LTPNoteMovement note in allNotes)
        {
            if (note != activeNote)
            {
                note.StopNote();
            }
        }
    }

    void ResumeAllNotes()
    {
        LTPNoteMovement[] allNotes = FindObjectsOfType<LTPNoteMovement>();
        foreach (LTPNoteMovement note in allNotes)
        {
            note.ResumeNote();
        }
    }

    void PauseAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    void ResumeAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}
