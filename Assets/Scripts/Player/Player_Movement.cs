using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform player;
    public Transform cam;

    public float speed = 6;
    public float walkspeed = 5;
    public float runspeed = 10;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    public float jumpDistanceFactor = 2.5f;
    Vector3 velocity;
    public bool isGrounded;
    bool isJumpWall;
    
    public bool isClimbing;
    public float ClimbForce = 5.0f;
    public float ClimbSpeed = 2.0f;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Collider ColliderFront;
    public Transform sideCheck;
    public LayerMask wallMask;
    public float sideDistance = 1.0f;
    public bool sideJump;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    // Update is called once per frame

    private void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isJumpWall = Physics.CheckSphere(sideCheck.position, sideDistance, wallMask);


        

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }    
        
        if (Input.GetButtonDown("Jump") && !isGrounded && isJumpWall)
        {
            Debug.Log("SideJump");
            sideJump = true;

            RaycastHit hit;

            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 10f))
            {
                
                Debug.Log(Vector3.Angle(transform.forward, hit.normal));

                if (Vector3.Angle(transform.forward, hit.normal) < 160 && Vector3.Angle(transform.forward, hit.normal) > 0)
                {
                    //Bounce
                    Vector3 reflectV = Vector3.Reflect(transform.forward, hit.normal);


                    transform.position = hit.point;
                    transform.rotation = Quaternion.LookRotation(reflectV);

                    Vector3 moveDir = Quaternion.Euler(0f, transform.rotation.y, 0f) * Vector3.forward * 1.1f;
                    velocity.y = Mathf.Sqrt(jumpHeight * 0.5f * -2 * gravity);
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }
                else
                {
                    //climb mode
                    if (isClimbing)
                    {                        
                        transform.rotation = Quaternion.LookRotation(-transform.forward);
                        speed = walkspeed;
                        Vector3 moveDir = Quaternion.Euler(0f, transform.rotation.y, 0f) * Vector3.forward * 1.1f;
                        velocity.y = Mathf.Sqrt(jumpHeight * 0.5f * -2 * gravity);
                        controller.Move(moveDir.normalized * speed * Time.deltaTime);

                        isClimbing = false;
                    }
                    else
                    {
                        isClimbing = true;
                    }
                }
            }

        }


    }

    void FixedUpdate()
    {
        //gravity
        if (!isClimbing)
        {
            velocity.y += gravity * Time.fixedDeltaTime;
            controller.Move(velocity * Time.fixedDeltaTime);
            velocity.x = 0;
        }
        else
        {
            velocity.y += gravity * Time.fixedDeltaTime;
            velocity.x = ClimbForce;

        }


        if (isGrounded && !isClimbing)
        {
            //run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runspeed;
            }
            else
            {
                speed = walkspeed;
            }

            //walk
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }


        }
        else if(isClimbing)
        {
            //Climb
            speed = walkspeed * 0.5f;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(0f, vertical, 0f).normalized;

            if (direction.magnitude >= 0.1f)
            {
                controller.Move(direction.normalized * speed * Time.deltaTime);
            }

            if(vertical < 0 && isGrounded)
            {
                isClimbing = false;
            }
        }
        else
        {
            float vertical = 0;

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                vertical = Input.GetAxisRaw("Vertical") * jumpDistanceFactor/2;
            }
            else if(Input.GetAxisRaw("Vertical") < 0)
            {
                vertical = Input.GetAxisRaw("Vertical") * jumpDistanceFactor;
            }
            //jumping forward speed
            float character = player.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, character, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed + vertical) * Time.deltaTime);
        }
    }
}
