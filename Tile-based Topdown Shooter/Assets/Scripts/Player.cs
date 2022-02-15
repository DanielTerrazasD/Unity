using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;

    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_rotationSpeed;

    // For debugging purposes:
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        // Calculate rotation diferential
        float rotationSpeed = -horizontalInput * m_rotationSpeed * Time.fixedDeltaTime;

        // Move rigid body by that diferential amount
        //rb.MoveRotation(rb.rotation + rotationSpeed);
        sr.transform.Rotate(0.0f, 0.0f, rotationSpeed);

        // Update angle
        Quaternion rotation = sr.transform.rotation;
        Vector3 angles = rotation.eulerAngles;
        angle = angles.z;
    }

    private void Move()
    {
        float xVelocity = Mathf.Cos(Mathf.Deg2Rad * angle) * verticalInput * m_moveSpeed * Time.fixedDeltaTime;
        float yVelocity = Mathf.Sin(Mathf.Deg2Rad * angle) * verticalInput * m_moveSpeed * Time.fixedDeltaTime;

        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D - " + collision.gameObject.name);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("OnCollisionStay2D - " + collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D - " + collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D - " + collision.gameObject.name);
    }
}
