using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffSet = 0.05f;
    bool canMove = true;
    public SwordAttack swordAttack;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (canMove) { 
        if (movementInput != Vector2.zero)
        {
         bool success = TryMove(movementInput);
            if (!success && movementInput.x > 0)
            {
                success = TryMove(new Vector2(movementInput.x , 0));

             
            }
            if (!success && movementInput.y > 0)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }
            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (movementInput.x < 0) { 
        spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0) { 
        spriteRenderer.flipX = false;
        }
        }
    }
    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
             direction,
             movementFilter,
             castCollisions,
             moveSpeed * Time.deltaTime + collisionOffSet);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            return true;
        }
        else
        { 
        return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput= movementValue.Get<Vector2>();
    }
    void OnFire()
    {
        animator.SetTrigger("swordAttack");
        Debug.Log("Fire pressed.");
    }
    public void EndSwordAttack()
    {
        UnLockMovement();
        swordAttack.StopAttack(); 


    }
    public void SwordAttack()
    {
        LockMovement();
        if(spriteRenderer.flipX == true) { 
        swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight(); 
        }
   
    }
    
    public void LockMovement()
    {
        canMove = false;
    }
    public void UnLockMovement()
    {
        canMove= true;
    }

}
