using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private List<GameObject> notesInTrigger = new List<GameObject>();
    GameObject GameManager;
    private bool isPaused = false;
    private bool firstTouchDetected = false;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        if (GameManager == null)
        {
            Debug.LogError("GameManager tidak ditemukan!");
        }
    }

    void Update()
    {
        if (isPaused)
            return;

        // Deteksi sentuhan pada layar
        if (Input.touchCount > 0)
        {
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
                                GameManager?.GetComponent<GameManager>()?.AddStreak();
                                AddScore();
                                firstTouchDetected = true; // Tanda bahwa sentuhan pertama telah terdeteksi
                            }
                            else
                            {
                                GameManager?.GetComponent<GameManager>()?.ResetStreak();
                                firstTouchDetected = false; // Reset tanda jika tidak ada note yang terdeteksi
                            }
                        }
                    }
                }
            }
        }
        else if (Input.touchCount == 0 && firstTouchDetected)
        {
            // Deteksi sentuhan bergantian jika tidak ada sentuhan yang terdeteksi dan sentuhan pertama telah terdeteksi sebelumnya
            for (int j = 0; j < Input.touchCount; j++)
            {
                Touch touch = Input.GetTouch(j);
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
                                GameManager?.GetComponent<GameManager>()?.AddStreak();
                                AddScore();
                            }
                            else
                            {
                                GameManager?.GetComponent<GameManager>()?.ResetStreak();
                            }
                        }
                    }
                }
            }
            firstTouchDetected = false; // Reset tanda setelah mendeteksi sentuhan bergantian
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WinNote"))
        {
            GameManager?.GetComponent<GameManager>()?.Win();
        }

        if (other.CompareTag("Note"))
        {
            notesInTrigger.Add(other.gameObject);
            HighlightNoteTile(other.gameObject, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            notesInTrigger.Remove(other.gameObject);
            HighlightNoteTile(other.gameObject, false);
            if (notesInTrigger.Count == 0) // Reset streak if no notes are in the trigger
            {
                GameManager?.GetComponent<GameManager>()?.ResetStreak();
            }
        }
    }

    void AddScore()
    {
        if (GameManager != null)
        {
            int currentScore = PlayerPrefs.GetInt("Score");
            currentScore += GameManager.GetComponent<GameManager>().GetScore();
            PlayerPrefs.SetInt("Score", currentScore);
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
}
