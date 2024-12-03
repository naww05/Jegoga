using UnityEngine;
using UnityEngine.SceneManagement;

public class GuidePopUpController : MonoBehaviour
{
    public GameObject PopUpPanel; // Panel pop-up

    void Start()
    {

    }

    public void OpenPopUpPanel()
    {
        // Menampilkan pop up panel
        PopUpPanel.SetActive(true);
    }

    public void ClosePopUpPanel()
    {
        PopUpPanel.SetActive(false);
    }
}
