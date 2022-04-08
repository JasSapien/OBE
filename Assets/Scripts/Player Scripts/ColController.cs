using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.Tilemaps;

public class ColController : MonoBehaviour
{
    public PlayerController playerController;

    public bool canMove;
    public bool playerMove;
    public bool blockMove;

    public bool obstacleDetected;

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
        if (canMove)
        {
            if (blockDetected || obstacleDetected)
            {
                playerMove = false;
                blockMove = false;
            }
            else
            {
                playerMove = true;
                blockMove = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CanPass"))
        {
            canMove = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            blockMove = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            obstacleDetected = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = true;
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

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            obstacleDetected = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pushables"))
        {
            blockDetected = false;
            block = null;
        }
    }
}
