using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.Tilemaps;

public class LeftColController : MonoBehaviour
{
    public PlayerController playerController;

    public bool moveLeft;

    [Header("Block Info")]
    public GameObject block;
    public bool blockDetected;

    public float timeToMove = 0.2f;
    public bool isMoving;
    public Vector3 origPos, targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Leftdate is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            moveLeft = true;
        }

        if (playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("BlockPass"))
        {
            moveLeft = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = true;
            moveLeft = false;

            block = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            moveLeft = false;
        }

        if (playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("BlockPass"))
        {
            moveLeft = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = false;
            moveLeft = true;

            block = null;
        }
    }
}
