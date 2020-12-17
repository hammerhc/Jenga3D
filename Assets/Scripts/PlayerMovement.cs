using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed;
    public float gravity;
    public float jumpHeight;
    public float thrust;
    public float maxThrust;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public LayerMask towerMask;

    Vector3 velocity;
    bool isGrounded;
    bool isHovering;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, towerMask);

        if (Input.GetMouseButtonDown(1) && isGrounded == false)
        {
            isHovering = !isHovering;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity.y += thrust * Time.deltaTime;
            if (velocity.y > maxThrust)
            {
                velocity.y = maxThrust;
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (isHovering)
        {
            velocity.y = 0;
        }
    }
}
