using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.Tilemaps;

public class ColController : MonoBehaviour
{
    public PlayerController playerController;

    public bool playerMove;
    public bool blockMove;

    [Header("Block Info")]
    public GameObject block;
    public bool blockDetected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            playerMove = true;
            blockMove = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            blockMove = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = true;
            playerMove = false;
            blockMove = false;

            block = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            playerMove = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            blockMove = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = false;
            playerMove = true;
            blockMove = true;

            block = null;
        }
    }
}
