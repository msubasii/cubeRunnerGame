using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// This is a reference to the Rigidbody component called "rb"
	public Rigidbody rb;

	public float forwardForce = 2000f;	// Variable that determines the forward force
	public float sidewaysForce = 500f;  // Variable that determines the sideways force
	public float rotationSpeed = 2000f;
    public float sidewaysSpeed = 30f; // Increased this for noticeable 
	public float rightEdge;
	[Range(0, 2)]

    

    public float max_accel;
    [SerializeField] private float current_accel;
    public float max_accel_reach_time;

    void Start()
    {
        // Initialize Rigidbody
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        Vector3 mevcut_pozisyon = transform.position;

        if (Input.GetKey("d"))
        {
            current_accel += max_accel * Time.deltaTime / max_accel_reach_time;
            mevcut_pozisyon.x += Mathf.Min(current_accel, sidewaysSpeed) * Time.deltaTime;
        }
        else if (Input.GetKey("a"))
        {
            current_accel += max_accel * Time.deltaTime / max_accel_reach_time;
            mevcut_pozisyon.x -= Mathf.Min(current_accel, sidewaysSpeed) * Time.deltaTime;
        }
        else if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
        else
        {
            current_accel = 0; // Reset acceleration if no keys are pressed
        }

        // Clamp the position
        mevcut_pozisyon.x = Mathf.Clamp(mevcut_pozisyon.x, -7.5f, rightEdge - transform.localScale.x / 2);
        transform.position = mevcut_pozisyon;

	
    }
}
