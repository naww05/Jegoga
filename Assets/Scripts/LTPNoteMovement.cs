using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTPNoteMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    private bool isStopped = false;
    private bool isPaused = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = new Vector3(0, -speed, 0);
        Pause(); // Note dipause pada awal permainan
    }

    void Update()
    {
        if (isStopped || isPaused) // Note berhenti jika dihentikan atau dipause
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = new Vector3(0, -speed, 0);
        }
    }

    public void StopNote()
    {
        isStopped = true;
    }

    public void ResumeNote()
    {
        isStopped = false;
        Resume(); // Pastikan note dilanjutkan
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
}
