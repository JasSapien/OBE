using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.Tilemaps;

public class UpColController : MonoBehaviour
{
    public PlayerController playerController;

    public bool moveUp;

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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            moveUp = true;
        }

        if (playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("BlockPass"))
        {
            moveUp = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = true;
            moveUp = false;

            block = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            moveUp = false;
        }

        if (playerController.controlBlock && other.gameObject.layer == LayerMask.NameToLayer("BlockPass"))
        {
            moveUp = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = false;
            moveUp = true;

            block = null;
        }
    }
}