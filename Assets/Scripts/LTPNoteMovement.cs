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
        // Mengontrol kecepatan berdasarkan status note
        if (isStopped || isPaused)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true; // Menghentikan physics saat dipause atau dihentikan
        }
        else
        {
            rb.isKinematic = false; // Aktifkan physics lagi saat dilanjutkan
            rb.velocity = new Vector3(0, -speed, 0);
        }
    }

    public void StopNote()
    {
        isStopped = true;
        rb.velocity = Vector3.zero; // Pastikan kecepatan langsung menjadi 0
    }

    public void ResumeNote()
    {
        isStopped = false;
        Resume(); // Pastikan note dilanjutkan
    }

    public void Pause()
    {
        isPaused = true;
        rb.velocity = Vector3.zero; // Pastikan kecepatan langsung menjadi 0 saat dipause
        rb.isKinematic = true; // Menonaktifkan physics selama pause
    }

    public void Resume()
    {
        isPaused = false;
        rb.isKinematic = false; // Mengaktifkan physics kembali saat resume
        rb.velocity = new Vector3(0, -speed, 0); // Kembali mengatur velocity
    }
}
