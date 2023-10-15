using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class anScript : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    public int speed = 70;
    public bool touchGround = true;

    public LayerMask Ground;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {


    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            touchGround = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            touchGround = false;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Move();

        RotateCharacter();

        Jump(3);
        if(!touchGround)
        {
            Jump(3);
        }
    }
    private void RotateCharacter()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.Self);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(Vector3.down, 90 * Time.deltaTime, Space.Self);
        }
    }
    private void Jump(int digitOfState)
    {
        if (Input.GetKey(KeyCode.Space) && touchGround)
        {
            if (touchGround == true)
            {
                anim.SetInteger("State", digitOfState);
                //transform.position += new Vector3(0, 30f, 0);

                //transform.Translate(Vector3.up *35* Time.deltaTime, Space.Self);
                transform.Translate(Vector3.up * 110 * Time.deltaTime, Space.Self);

                //rb.velocity = new Vector3(0, 30f, 0);
                //rb.velocity = rb.velocity + transform.forward * Time.deltaTime * 200;

                //transform.Translate(Vector3.forward * speed * 200 * Time.deltaTime);
                transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);
            }
        }
        anim.SetBool("touchGround", touchGround);
        if (touchGround == false)
        {
            anim.SetInteger("State", 0);
           // anim.SetBool("touchGround", touchGround);
        }
    }
    private void Move()
    {
        if (Input.GetAxis("Vertical") > 0 && touchGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("State", 1);
                //transform.Translate(Vector3.forward * speed * Time.deltaTime);
                //rb.AddForce(-Vector3.forward * Time.deltaTime * 100);
                //rb.velocity = rb.velocity+new Vector3(Time.deltaTime * speed, 0,0);
                rb.velocity = rb.velocity + transform.forward * Time.deltaTime * speed * 40;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetInteger("State", 2);
                    //transform.Translate(Vector3.forward * speed * 3 * Time.deltaTime);
                    //rb.AddForce(-Vector3.forward * Time.deltaTime * 2000);
                    //rb.velocity = Vector3.forward * Time.deltaTime * speed * 3;

                    rb.velocity = rb.velocity + transform.forward * Time.deltaTime * speed*100;

                    Jump(4);
                }
            }
        }
        else if (Input.GetAxis("Vertical") <= 0 && touchGround)
        {
            rb.velocity = new Vector3(0,0,0);
            anim.SetInteger("State", 0);
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetInteger("State", 1);
                //transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                rb.velocity = rb.velocity - transform.forward * Time.deltaTime * speed * 40;
            }
        }
    }
}
