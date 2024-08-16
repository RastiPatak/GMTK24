using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    bool inAir = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale = 1;
            rb.AddForce(transform.up * 200);
            inAir = true;
        }
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            rb.gravityScale *= 1.05f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        inAir = false;
        rb.gravityScale = 1;
    }
}
