using System.Collections.Generic;
using UnityEngine;

public class LTPActivator : MonoBehaviour
{
    private List<GameObject> notesInTrigger = new List<GameObject>();
    private AudioSource audioSource;  // Tambahkan referensi AudioSource untuk setiap activator

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Deteksi sentuhan pada layar
        for (int i = 0; i < Input.touchCount; i++) // Loop untuk deteksi multi-touch
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                // Cek apakah sentuhan terjadi pada objek activator
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        PlaySound(); // Mainkan suara untuk activator yang disentuh

                        if (notesInTrigger.Count > 0)
                        {
                            DestroyNotesInTrigger();
                            ResumeAllNotes(); // Resume semua note secara bersamaan
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

            LTPNoteMovement noteMovement = other.GetComponent<LTPNoteMovement>();
            if (noteMovement != null)
            {
                noteMovement.StopNote();
                PauseAllNotesExcept(noteMovement); // Pause semua note lainnya
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            notesInTrigger.Remove(other.gameObject);
            HighlightNoteTile(other.gameObject, false);

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

    void HighlightNoteTile(GameObject note, bool highlight)
    {
        Renderer renderer = note.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = highlight ? Color.red : Color.white;
        }
    }

    // Fungsi untuk memainkan suara pada activator
    void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
