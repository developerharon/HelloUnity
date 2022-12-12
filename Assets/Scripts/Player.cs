using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private int superJumpsRemaining;

    private Rigidbody rigidBodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every physics update
    private void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);
        
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
            return;

        if (jumpKeyWasPressed)
        {
            float jumpPower = 7;

            if (superJumpsRemaining > 0)
            {
                jumpPower += 3;
                superJumpsRemaining--;
            }

            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }
}