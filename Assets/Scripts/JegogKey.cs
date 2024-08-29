using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JegogKey : MonoBehaviour
{
    private Animator animator;
    public float touchSensitivity = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                    {
                        TriggerAnimation();
                    }
                }
            }
        }
    }

    void TriggerAnimation()
    {
        animator.SetFloat("Sensitivity", touchSensitivity);
        animator.SetTrigger("Hit");
    }
}

