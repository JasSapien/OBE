using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public bool canMove;
    public bool onStairs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!onStairs && other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Stairs") || other.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            onStairs = true;
            canMove = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Stairs") || other.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            onStairs = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            canMove = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            canMove = true;
        }
    }
}
