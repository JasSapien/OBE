using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Player player;

    public int playerDir;

    public GameObject moveColliders;

    public GameObject block;
    public BlockScript blockScript;

    public bool canMove;
    public bool isMoving;
    private Vector3 lastPos, origPos, targetPos, origBlockPos, targetBlockPos;
    public float recallTime = 0.2f;
    private float timeToMove = 0.2f;
    private float timeToMoveBlock = 0.2f;

    public PlayerCollider playerCol;

    public ColController leftCollider, rightCollider, upCollider, downCollider;

    public bool readyShift;
    public bool isShifted;
    public bool greenShift = false;
    public bool redShift = false;


    private SpriteRenderer playerSprite;

    public Sprite faulterFront;
    public Sprite faulterBack;
    public Sprite faulterSide;

    public Sprite greenFront;
    public Sprite greenBack;
    public Sprite greenSide;
    public Sprite greenBlock;

    public GameObject coatSleep;

    public bool controlBlock;

    public int bodyFloor;
    public Vector3 bodyPos;
    public Vector3 nextTile;
    public Vector3 lastTile;
    public Vector3 saveNextTile;
    public Vector3 saveLastTile;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(0);
        playerSprite = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        canMove = true;
        lastTile = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (player.GetAxisRaw("Move Horizontal") == 1f && !isMoving)
            {
                playerDir = 0;

                if (!controlBlock)
                {
                    if (isShifted)
                    {
                        playerSprite.sprite = greenSide;
                    }
                    else
                    {
                        playerSprite.sprite = faulterSide;
                    }

                    playerSprite.flipX = false;

                    if (rightCollider.playerMove)
                    {
                        StartCoroutine(MovePlayer(Vector3.right));

                        lastTile = nextTile;
                        nextTile = rightCollider.transform.position;
                    }
                }
                else
                {
                    if (rightCollider.blockMove && rightCollider.playerMove)
                    {
                        StartCoroutine(MovePlayer(Vector3.right));

                        lastTile = nextTile;
                        nextTile = rightCollider.transform.position;
                    }
                }
                
            }

            if (player.GetAxisRaw("Move Horizontal") == -1f && !isMoving)
            {
                playerDir = 1;

                if (!controlBlock)
                {
                    if (isShifted)
                    {
                        playerSprite.sprite = greenSide;
                    }
                    else
                    {
                        playerSprite.sprite = faulterSide;
                    }

                    playerSprite.flipX = true;

                    if (leftCollider.playerMove)
                    {
                        StartCoroutine(MovePlayer(Vector3.left));

                        lastTile = nextTile;
                        nextTile = leftCollider.transform.position;
                    }
                }
                else
                {
                    if (leftCollider.blockMove && leftCollider.playerMove)
                    {
                        StartCoroutine(MovePlayer(Vector3.left));

                        lastTile = nextTile;
                        nextTile = leftCollider.transform.position;
                    }
                }
            }

            if (player.GetAxisRaw("Move Vertical") == 1f && !isMoving)
            {
                playerDir = 2;

                if (!controlBlock)
                {
                    if (isShifted)
                    {
                        playerSprite.sprite = greenBack;
                    }
                    else
                    {
                        playerSprite.sprite = faulterBack;
                    }

                    playerSprite.flipX = false;

                    if (upCollider.playerMove)
                    {
                        StartCoroutine(MovePlayer(Vector3.up));

                        lastTile = nextTile;
                        nextTile = upCollider.transform.position;
                    }
                }
                else
                {
                    if (upCollider.blockMove && upCollider.playerMove)
                    {
                        StartCoroutine(MovePlayer(Vector3.up));

                        lastTile = nextTile;
                        nextTile = upCollider.transform.position;
                    }
                }
            }

            if (player.GetAxisRaw("Move Vertical") == -1f && !isMoving)
            {
                playerDir = 3;

                if (!controlBlock)
                {
                    if (isShifted)
                    {
                        playerSprite.sprite = greenFront;
                    }
                    else
                    {
                        playerSprite.sprite = faulterFront;
                    }

                    playerSprite.flipX = false;

                    if (downCollider.playerMove)
                    {
                        StartCoroutine(MovePlayer(Vector3.down));

                        lastTile = nextTile;
                        nextTile = downCollider.transform.position;
                    }
                }
                else
                {
                    if (downCollider.blockMove && downCollider.playerMove)
                    {
                        StartCoroutine(MovePlayer(Vector3.down));

                        lastTile = nextTile;
                        nextTile = downCollider.transform.position;
                    }
                }
                
            }

            //Block Detection 0 = right, left = 1, up = 2, down = 3

            if (leftCollider.blockDetected)
            {
                if (isShifted && playerDir == 1)
                {
                    if (player.GetButton("Interact"))
                    {
                        block = leftCollider.block;
                        blockScript = leftCollider.block.GetComponent<BlockScript>();

                        transform.position = leftCollider.transform.position;

                        StartCoroutine(PossessBlock());
                    }
                }

                if (!isShifted && player.GetAxisRaw("Move Horizontal") == -1f && !isMoving)
                {
                    block = leftCollider.block;
                    blockScript = leftCollider.block.GetComponent<BlockScript>();

                    if (blockScript.leftCheck.canMove)
                    {
                        StartCoroutine(MoveBlock(Vector3.left));
                    }
                }
            }

            if (upCollider.blockDetected)
            {
                if (isShifted && playerDir == 2)
                {
                    if (player.GetButton("Interact"))
                    {
                        block = upCollider.block;
                        blockScript = upCollider.block.GetComponent<BlockScript>();

                        transform.position = upCollider.transform.position;

                        StartCoroutine(PossessBlock());
                    }
                }

                if (!isShifted && player.GetAxisRaw("Move Vertical") == 1f && !isMoving)
                {
                    block = upCollider.block;
                    blockScript = upCollider.block.GetComponent<BlockScript>();

                    if (blockScript.upCheck.canMove)
                    {
                        StartCoroutine(MoveBlock(Vector3.up));
                    }
                }
            }

            if (rightCollider.blockDetected)
            {
                if (isShifted && playerDir == 0)
                {
                    if (player.GetButton("Interact"))
                    {
                        block = rightCollider.block;
                        blockScript = rightCollider.block.GetComponent<BlockScript>();

                        transform.position = rightCollider.transform.position;

                        StartCoroutine(PossessBlock());
                    }
                }

                if (!isShifted && player.GetAxisRaw("Move Horizontal") == 1f && !isMoving)
                {
                    block = rightCollider.block;
                    blockScript = rightCollider.block.GetComponent<BlockScript>();

                    if (blockScript.rightCheck.canMove)
                    {
                        StartCoroutine(MoveBlock(Vector3.right));
                    }
                }
            }

            if (downCollider.blockDetected)
            {
                if (isShifted && playerDir == 3)
                {
                    if (player.GetButton("Interact"))
                    {
                        block = downCollider.block;
                        blockScript = downCollider.block.GetComponent<BlockScript>();

                        transform.position = downCollider.transform.position;

                        StartCoroutine(PossessBlock());
                    }
                }

                if (!isShifted && player.GetAxisRaw("Move Vertical") == -1f && !isMoving)
                {
                    block = downCollider.block;
                    blockScript = downCollider.block.GetComponent<BlockScript>();

                    if (blockScript.downCheck.canMove)
                    {
                        StartCoroutine(MoveBlock(Vector3.down));
                    }
                }
            }
        }

        if (player.GetButtonDown("Cancel"))
        {
            if (isShifted && !controlBlock)
            {
                canMove = false;

                StartCoroutine(RecallSoul());
                StartCoroutine(UnShift());
            }

            if (controlBlock)
            {
                canMove = false;

                StartCoroutine(ExitBlock());
            }
        }

        if (player.GetButtonDown("Item1") && !isShifted)
        {
            readyShift = true;
        }

        if (readyShift)
        {
            saveNextTile = nextTile;
            saveLastTile = lastTile;

            transform.position = nextTile;
            
            bodyPos = nextTile;

            bodyFloor = playerCol.floorNumber;
            coatSleep.SetActive(true);
            coatSleep.transform.parent = null;

            playerSprite.sprite = greenFront;

            isShifted = true;
            readyShift = false;
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        //moveColliders.SetActive(false);

        isMoving = true;

        float elapsedTime = 0;

        lastPos = transform.position;
        origPos = transform.position;
        targetPos = origPos + direction;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        //moveColliders.SetActive(true);

        isMoving = false;
    }

    private IEnumerator MoveBlock(Vector3 direction)
    {
        canMove = false;

        float elapsedTime = 0;

        origBlockPos = block.transform.position;
        targetBlockPos = origBlockPos + direction;

        while (elapsedTime < timeToMoveBlock)
        {
            block.transform.position = Vector3.Lerp(origBlockPos, targetBlockPos, (elapsedTime / timeToMoveBlock));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        block.transform.position = targetBlockPos;

        canMove = true;
    }

    private IEnumerator RecallSoul()
    {
        float elapsedTime = 0;

        while (elapsedTime < recallTime)
        {
            transform.position = Vector3.Lerp(transform.position, bodyPos, (elapsedTime / recallTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator UnShift()
    {
        isShifted = false;

        yield return new WaitForSeconds(0.2f);

        playerCol.floorNumber = bodyFloor;

        transform.position = bodyPos;
        playerSprite.sprite = faulterFront;

        nextTile = saveNextTile;
        lastTile = saveLastTile;

        coatSleep.SetActive(false);
        coatSleep.transform.parent = transform;

        canMove = true;
    }

    private IEnumerator PossessBlock()
    {
        controlBlock = true;
        playerSprite.sprite = greenBlock;
        block.SetActive(false);
        block.transform.position = transform.position;
        block.transform.SetParent(transform);
        
        yield return new WaitForSeconds(0.1f);

        //leftCollider.playerMove = true;
        //rightCollider.moveRight = true;
        //upCollider.moveUp = true;
        //downCollider.moveDown = true;
    }

    private IEnumerator ExitBlock()
    {
        yield return new WaitForSeconds(0.2f);

        controlBlock = false;
        playerSprite.sprite = greenFront;
        block.SetActive(true);
        block.transform.SetParent(null);

        if (leftCollider.playerMove)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        else if (upCollider.playerMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        else if (rightCollider.playerMove)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        else if (downCollider.playerMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }

        canMove = true;
    }
}
