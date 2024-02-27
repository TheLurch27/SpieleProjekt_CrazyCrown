using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem inputSystem;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isSaluting = false;
    private bool isSneaking = false;
    private bool isInteracting = false;

    public float moveSpeed = 5f;
    public float sneakSpeed = 2f;

    private Vector2 moveInput;

    private void Awake()
    {
        inputSystem = new InputSystem();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.CharacterInput.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputSystem.CharacterInput.Move.canceled += ctx => moveInput = Vector2.zero;
        inputSystem.CharacterInput.Interact.started += ctx => StartInteract();
        inputSystem.CharacterInput.Interact.canceled += ctx => EndInteract();
        inputSystem.CharacterInput.Salute.started += ctx => StartSalute();
        inputSystem.CharacterInput.Salute.canceled += ctx => EndSalute();
        inputSystem.CharacterInput.Crouch.started += ctx => StartCrouch();
        inputSystem.CharacterInput.Crouch.canceled += ctx => EndCrouch();
        inputSystem.CharacterInput.PickUpItem.started += ctx => StartPickUpItem();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.CharacterInput.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputSystem.CharacterInput.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputSystem.CharacterInput.Interact.started -= ctx => StartInteract();
        inputSystem.CharacterInput.Interact.canceled -= ctx => EndInteract();
        inputSystem.CharacterInput.Salute.started -= ctx => StartSalute();
        inputSystem.CharacterInput.Salute.canceled -= ctx => EndSalute();
        inputSystem.CharacterInput.Crouch.started -= ctx => StartCrouch();
        inputSystem.CharacterInput.Crouch.canceled -= ctx => EndCrouch();
        inputSystem.CharacterInput.PickUpItem.started -= ctx => StartPickUpItem();
    }

    private void FixedUpdate()
    {
        if (!isSaluting)
        {
            MoveCharacter(moveInput);
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        float speed = isSneaking ? sneakSpeed : moveSpeed;
        rb.velocity = direction.normalized * speed; // Richtung beibehalten und die Geschwindigkeit entsprechend setzen
        UpdateAnimation(direction);
    }

    private void StartInteract()
    {
        isInteracting = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isInteracting", true);
        animator.SetBool("isWalking", false);
    }

    private void EndInteract()
    {
        isInteracting = false;
        rb.velocity = Vector2.zero;
        animator.SetBool("isInteracting", false);
        animator.SetBool("isWalking", true);
    }

    public void StartSalute()
    {
        isSaluting = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isSaluting", true);
        animator.SetBool("isWalking", false);
    }

    private void EndSalute()
    {
        isSaluting = false;
        animator.SetBool("isSaluting", false);
    }

    public void StartCrouch()
    {
        isSneaking = true;
        animator.SetBool("isSneaking", true);
    }

    private void EndCrouch()
    {
        isSneaking = false;
        animator.SetBool("isSneaking", false);
    }

    internal void StartPickUpItem()
    {
        Debug.Log("PickUpItem button pressed.");
    }

    private void UpdateAnimation(Vector2 direction)
    {
        if (direction.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public bool IsSneaking // Öffentliche Eigenschaft, um den Schleichzustand abzurufen
    {
        get { return isSneaking; }
    }

    // Methode zum Setzen des Schleichzustands
    public void SetSneaking(bool sneaking)
    {
        isSneaking = sneaking;
    }
}
