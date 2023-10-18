using UnityEngine;

public class Test : MonoBehaviour
{
    private float timer = 0f;
    private float interval = 3f;
    private Rigidbody rb;
    private Vector3 direction;
    public float speed = 4f;

    private void FixedUpdate()
    {
        var position = rb.position;

        position += direction * speed * Time.deltaTime;
        rb.MovePosition(position);

        Debug.DrawRay(transform.position, transform.forward, Color.green);

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {


        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");


        direction = new Vector3(h, 0, v);
        var directionMag = direction.magnitude;

        if (directionMag > 1)
        {
            direction.Normalize();
        }

    }
}