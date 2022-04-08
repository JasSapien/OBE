using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    PlayerController playerController;

    public int floorNumber;

    public GameObject floor1;
    public GameObject floor2;
    public GameObject physicalPass;
    public GameObject astralPass;

    public GameObject playerShadow;

    public bool onStairs = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        floorNumber = 1;

        physicalPass.SetActive(true);
        astralPass.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isShifted && !playerController.controlBlock)
        {
            if (floorNumber == 1)
            {
                physicalPass.SetActive(true);
                astralPass.SetActive(false);
            }

            if (floorNumber == 2)
            {
                physicalPass.SetActive(false);
                astralPass.SetActive(true);
            }
        }
        else
        {
            physicalPass.SetActive(true);
            astralPass.SetActive(false);

            playerShadow.transform.localPosition = new Vector3(0, -0.1f, 0);
        }

        if (playerController.controlBlock)
        {
            playerShadow.SetActive(false);
            physicalPass.SetActive(true);
            astralPass.SetActive(false);
        }
        else
        {
            playerShadow.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (playerController.isShifted)
        {
            if (other.gameObject.name == "Floor 1" || other.gameObject.name == "Floor 2")
            {
                playerShadow.transform.localPosition = new Vector3(0, -0.1f, 0);
            }
        }

        if (other.gameObject.name == "Top")
        {
            floorNumber = 2;

            floor1.SetActive(false);
            floor2.SetActive(true);
        }

        if (other.gameObject.name == "Bottom")
        {
            floorNumber = 1;

            floor1.SetActive(true);
            floor2.SetActive(false);
        }

        if (other.gameObject.name == ("Stairs"))
        {
            onStairs = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == ("Stairs"))
        {
            onStairs = false;
        }

        if (playerController.isShifted)
        {
            if (other.gameObject.name == "Floor 2" && !onStairs)
            {
                playerShadow.transform.localPosition = new Vector3(0, -1.5f, 0);
            }
        }
    }
}
