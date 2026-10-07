using UnityEngine;


public enum FacingDirection { Down, Up, Right, Left }

// Pokemon-style movement: pressing new direction turns character without moving, 
// holding moves them or pressing the direciton the player is already facing
// Left animations reuses Right sprites mirrored
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Idle Sprites (2 frames each: normal, slightly bigger)")]
    public Sprite[] idleDown = new Sprite[2];
    public Sprite[] idleUp = new Sprite[2];
    public Sprite[] idleRight = new Sprite[2]; //mirrored for Left

    [Header("Wale Sprites (4 frames each)")]
    public Sprite[] walkDown = new Sprite[4];
    public Sprite[] walkUp = new Sprite[4];
    public Sprite[] walkRight = new Sprite[4]; //mirrored for left

    [Header("Animation Speed")]
    public float idleFrameRate = 4f;
    public float walkFrameRate = 8f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private FacingDirection currentFacing = FacingDirection.Down;
    private bool isMoving = false;
    private Vector2 moveVector;

    private float frameTimer;
    private int frameIndex;
    private FacingDirection lastAnimDir = FacingDirection.Down;
    private bool lastAnimMoving = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleInput();
        HandleAnimation();
    }

    void HandleInput()
    {
        FacingDirection? desired = null;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) desired = FacingDirection.Right;
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) desired = FacingDirection.Left;
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) desired = FacingDirection.Up;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) desired = FacingDirection.Down;

        if (desired == null)
        {
            isMoving = false;
            moveVector = Vector2.zero;
            return;
        }

        if (desired.Value != currentFacing)
        {
            // First press in new direction just turns to face, no movement
            currentFacing = desired.Value;
            isMoving = false;
            moveVector = Vector2.zero;
            return;
        }

        // Already facing this way and key held, now move
        isMoving = true;
        moveVector = DirectionToVector(currentFacing);
    }

    void FixedUpdate()
    {
        if (isMoving)
            rb.MovePosition(rb.position + moveVector * moveSpeed * Time.fixedDeltaTime);
    }

    Vector2 DirectionToVector(FacingDirection dir)
    {
        switch (dir)
        {
            case FacingDirection.Up: return Vector2.up;
            case FacingDirection.Down: return Vector2.down;
            case FacingDirection.Right: return Vector2.right;
            case FacingDirection.Left: return Vector2.left;
            default: return Vector2.zero;
        }
    }

    void HandleAnimation()
    {
        sr.flipX = (currentFacing == FacingDirection.Left);

        // Reset fram cycle when direction or moving-state chages to prevent jumping mid animation
        if (currentFacing != lastAnimDir || isMoving != lastAnimMoving)
        {
            frameIndex = 0;
            frameTimer = 0f;
            lastAnimDir = currentFacing;
            lastAnimMoving = isMoving;
        }

        Sprite[] frames = GetCurrentFrameSet();
        if (frames == null || frames.Length == 0) return;

        float rate = isMoving ? walkFrameRate : idleFrameRate;
        frameTimer += Time.deltaTime;

        if (frameTimer >= 1f / rate)
        {
            frameTimer = 0f;
            frameIndex = (frameIndex + 1) % frames.Length;
        }

        sr.sprite = frames[frameIndex];
    }

    Sprite[] GetCurrentFrameSet()
    {
        switch (currentFacing)
        {
            case FacingDirection.Down: return isMoving ? walkDown : idleDown;
            case FacingDirection.Up: return isMoving ? walkUp : idleUp;
            case FacingDirection.Right:
            case FacingDirection.Left: return isMoving ? walkRight : idleRight;
            default: return idleDown;
        }
    }
}
