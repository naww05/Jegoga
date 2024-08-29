using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour
{
    float RM;
    GameObject Needle;
    GameObject Cylinder;

    void Start()
    {
        Needle = transform.Find("Needle").gameObject;
        Cylinder = transform.Find("Cylinder").gameObject;
    }

    void Update()
    {
        UpdateNeedle();
    }

    public void UpdateNeedle()
    {
        RM = PlayerPrefs.GetInt("RockMeter");
        float normalizedRM = (RM - 25) / 23.0f; // Menormalkan RM antara -1 dan 1

        // Misalnya, jarum bergerak sepanjang sumbu x silinder dengan rentang tertentu
        float needleRange = Cylinder.transform.localScale.x / 2; // Setengah lebar silinder
        Needle.transform.localPosition = new Vector3(normalizedRM * needleRange, Needle.transform.localPosition.y, Needle.transform.localPosition.z);
    }
}
