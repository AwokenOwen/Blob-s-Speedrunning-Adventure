using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MoveStates
{
    normal,
    dashing
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    MoveStates moveState = MoveStates.normal;

    [SerializeField]
    Vector2 input;

    [SerializeField]
    Transform footSphereTransform;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float dashForce;

    public Rigidbody2D rb { get; private set; }

    [SerializeField]
    bool hasDash;

    [SerializeField]
    float normalDrag;

    [SerializeField]
    float dashDrag;

    [SerializeField]
    float dashDuration;

    [SerializeField]
    float gravityScale;

    [SerializeField]
    float deathOffset;

    float maxSpeed;

    float dashDir;

    int lastDir;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        rb = GetComponent<Rigidbody2D>();
        hasDash = true;
    }

    private void Start()
    {
        rb.drag = normalDrag;
    }

    private void Update()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(input.x, 0);
        switch (moveState)
        {
            case MoveStates.normal:
                rb.drag = normalDrag;
                if (GameManager.Instance.alive)
                {
                    rb.gravityScale = gravityScale;
                }
                maxSpeed = moveSpeed;
                rb.AddForce(moveDirection * moveSpeed);
                if (moveDirection.x > 0.5f)
                {
                    GetComponent<Animator>().Play("Right");
                }
                else if (moveDirection.x < -0.5f)
                {
                    GetComponent<Animator>().Play("Left");
                }
                else if (input.y < -0.25f)
                {
                    GetComponent<Animator>().Play("Crouch");
                }
                else
                {
                    GetComponent<Animator>().Play("Idle");
                }
                break;
            case MoveStates.dashing:
                rb.drag = dashDrag;
                rb.gravityScale = 0f;
                rb.AddForce(new Vector2(dashDir * dashForce, 0), ForceMode2D.Impulse);
                break;
            default:
                break;
        }

        if (Grounded() && !hasDash)
        {
            hasDash = true;
        }
    }

    bool Grounded()
    {
        if (Physics2D.CircleCast(footSphereTransform.position, GetComponent<CapsuleCollider2D>().size.x / 2f, Vector3.down, 0.2f, groundMask) && GameManager.Instance.phaseMode)
            return true;
        return false;
    }

    public void OnMove(Vector2 input)
    {
        this.input = input;
    }

    public void OnJump()
    {
        if (Grounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnDash()
    {
        if (!Grounded() && hasDash)
        {
            dashDir = Mathf.Sign(input.x);
            hasDash = false;
            rb.velocity = new Vector2();
            StartCoroutine(DashDuration());
            if (dashDir > 0)
            {
                GetComponent<Animator>().Play("DashRight");
            }
            else if (dashDir < 0)
            {
                GetComponent<Animator>().Play("DashLeft");
            }
        }
    }

    IEnumerator DashDuration()
    {
        moveState = MoveStates.dashing;
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = new Vector2();
        moveState = MoveStates.normal;
    }

    public bool checkIfInside()
    {
        return Physics2D.OverlapCapsule(transform.position, new Vector2(GetComponent<CapsuleCollider2D>().size.x - deathOffset, GetComponent<CapsuleCollider2D>().size.y - deathOffset), GetComponent<CapsuleCollider2D>().direction, 0f, groundMask) && GameManager.Instance.alive;
    }

    public void LoadMainMenu()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
