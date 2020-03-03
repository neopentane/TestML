using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float x, z=0;
    public float speed = 1000f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(x * speed * Time.deltaTime, 0, z * speed * Time.deltaTime), ForceMode.Impulse);
    }
}
