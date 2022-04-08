using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public bool switchActive;

    // Start is called before the first frame update
    void Start()
    {
        switchActive = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameObject.CompareTag("FreeBlock"))
        {
            switchActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.CompareTag("FreeBlock"))
        {
            switchActive = false;
        }
    }
}
