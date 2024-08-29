using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicListManager : MonoBehaviour
{
    public AudioSource audioSource; // Komponen AudioSource untuk memutar lagu
    public AudioClip[] musicClips; // Array dari lagu-lagu
    public GameObject listItemPrefab; // Prefab untuk item dalam daftar
    public Transform listParent; // Parent untuk item daftar
    public float previewDuration = 10f; // Durasi pratinjau dalam detik

    private Coroutine previewCoroutine; // Coroutine untuk pratinjau lagu

    void Start()
    {
        // Membuat item daftar untuk setiap lagu
        foreach (AudioClip clip in musicClips)
        {
            GameObject listItem = Instantiate(listItemPrefab, listParent);
            Text songNameText = listItem.transform.Find("SongName").GetComponent<Text>();
            Button listenButton = listItem.transform.Find("ListenButton").GetComponent<Button>();

            songNameText.text = clip.name;
            listenButton.onClick.AddListener(() => OnListenButtonClick(clip));
        }
    }

    // Fungsi untuk menangani klik pada tombol "Listen"
    public void OnListenButtonClick(AudioClip clip)
    {
        if (previewCoroutine != null)
        {
            StopCoroutine(previewCoroutine);
        }

        previewCoroutine = StartCoroutine(PlayPreview(clip));
    }

    // Coroutine untuk memutar pratinjau lagu
    IEnumerator PlayPreview(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitForSeconds(previewDuration);

        audioSource.Stop();
    }
}
