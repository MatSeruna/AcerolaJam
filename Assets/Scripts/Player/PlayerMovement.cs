using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    [SerializeField] public float Speed { get; set; } = 5;

    public float groundDrag;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void Move()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        
        rb.AddForce(moveDirection.normalized * Speed * 10f, ForceMode.Force);
        

        Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > Speed)
        {
            Vector3 limitVel = flatVel.normalized * Speed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }


}
