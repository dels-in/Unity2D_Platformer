using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int score;
    public Text scoreText;
    public AudioClip audioClipJump;

    private bool isGrounded;
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSourceJump;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scoreText.text = score.ToString();
        audioSourceJump = GetComponent<AudioSource>();
    }

    private void Update()
    {
        scoreText.text = score.ToString();

        animator.SetInteger("State", 0);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            animator.SetTrigger("Jump");
        }

        var position = transform.position;

        position.x += Input.GetAxis("Horizontal") * speed;

        transform.position = position;

        if (isGrounded)
            animator.SetBool("IsFalling", false);
        else if (rigidbody2d.velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("IsFalling", true);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
                spriteRenderer.flipX = true;
            else if (Input.GetAxis("Horizontal") > 0)
                spriteRenderer.flipX = false;

            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }

    private void Jump()
    {
        audioSourceJump.PlayOneShot(audioClipJump);
        rigidbody2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        isGrounded = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        isGrounded = false;
    }

    public void AddCoin(int count)
    {
        score += count;
    }
}