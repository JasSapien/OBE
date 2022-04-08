using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.Tilemaps;

public class RightColController : MonoBehaviour
{
    public PlayerController playerController;

    public bool moveRight;

    [Header("Block Info")]
    public GameObject block;
    public bool blockDetected;

    public float timeToMove = 0.2f;
    public bool isMoving;
    public Vector3 origPos, targetPos;

    // Start is called before the first frame Update
    void Start()
    {

    }

    // Rightdate is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            moveRight = true;
        }

        if (playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("BlockPass"))
        {
            moveRight = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = true;
            moveRight = false;

            block = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            moveRight = false;
        }

        if (playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("BlockPass"))
        {
            moveRight = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = false;
            moveRight = true;

            block = null;
        }
    }
}