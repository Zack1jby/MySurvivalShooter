using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float dashSpeed = 2f;
    public float dashTime = .5f;
    public float dashCooldownTime = 3f;
    public bool isDashing;
    public GameObject dashShield;
    bool canDash = true;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && (!isDashing && canDash))
        {
            StartCoroutine(nameof(Dash));
        }

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;
        // Boost player character's movement speed by a multiplier while dashing
        if (isDashing)
        {
            movement *= dashSpeed;
        }

        playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    // Temporarily boost player character's movement speed, make them invincible to enemy attack, and display the dash shield object
    IEnumerator Dash()
    {
        StartCoroutine(nameof(DashCooldown));
        isDashing = true;
        dashShield.SetActive(true);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        dashShield.SetActive(false);

    }

    // Track dash cooldown
    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldownTime);
        canDash = true;
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
