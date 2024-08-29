using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    private bool isPaused = true; // Mulai dengan note dipause

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
        if (isPaused)
        {
            rb.velocity = Vector3.zero; // Hentikan note jika dipause
        }
        else
        {
            rb.velocity = new Vector3(0, -speed, 0); // Gerakkan note jika tidak dipause
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
}
