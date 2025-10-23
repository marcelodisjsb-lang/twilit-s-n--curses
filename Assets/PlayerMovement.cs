using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Vari�veis p�blicas para ajuste no Inspector
    public float moveSpeed = 5f; // Velocidade de movimento
    public float jumpForce = 10f; // For�a do pulo

    private Rigidbody2D rb; // Refer�ncia ao Rigidbody2D
    private Animator animator; // Refer�ncia ao Animator
    private SpriteRenderer spriteRenderer; // Refer�ncia ao SpriteRenderer
    public bool isGrounded = true; // Verifica se o jogador est� no ch�o


    void Start()
    {
        // Obt�m o componente Rigidbody2D do GameObject
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateAnimator();
        Movement();
        Jump();
        Attack();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", !isGrounded);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void Jump()
    {
        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void Movement()
    {
        // Movimento horizontal
        float moveInput = Input.GetAxis("Horizontal"); // Captura entrada do teclado (A/D ou setas)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Inverte a dire��o do sprite do personagem
        MirrorSprite(moveInput);
    }

    private void MirrorSprite(float moveInput)
    {
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        MirrorChildren();
    }

    private void MirrorChildren()
    {
        foreach (var child in transform.GetComponentsInChildren<Transform>())
        {
            if (child == transform) continue;

            Quaternion newRotation = Quaternion.identity;

            if (spriteRenderer.flipX)
            {
                newRotation = Quaternion.Euler(180f * Vector3.up);
            }

            child.rotation = newRotation;
        }

    }

    // Verifica se o jogador est� no ch�o (n�o � a melhor forma de fazer isso)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
