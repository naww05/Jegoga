using System.Collections;
using UnityEngine;

public class JegogController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isGameStarted = false;
    public static bool isPaused = false; // Static variable to track if the game is paused

    void Start()
    {
        // Ambil komponen AudioSource
        audioSource = GetComponent<AudioSource>();
        // Pastikan AudioSource tidak memutar suara otomatis saat dimulai
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }

        // Set game started setelah delay untuk memastikan semua inisialisasi selesai
        StartCoroutine(StartGameDelay());
    }

    IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(0.1f); // Delay 1 detik
        isGameStarted = true;
    }

    void Update()
    {
        // Jika game belum dimulai atau game di-pause, jangan proses input
        if (!isGameStarted || isPaused) return;

        // Deteksi input sentuhan
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                // Raycast dari posisi sentuhan
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Cek apakah objek yang disentuh adalah objek ini
                    if (hit.transform == transform)
                    {
                        Debug.Log("Bambu " + transform.name + " disentuh pada posisi: " + touch.position);
                        // Mainkan suara
                        PlaySound();
                    }
                }
            }
        }

#if UNITY_EDITOR
        // Deteksi klik mouse di editor
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Bambu " + transform.name + " disentuh (mouse) pada posisi: " + Input.mousePosition);
                    PlaySound();
                }
            }
        }
#endif
    }

    // Fungsi untuk memainkan suara
    void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
