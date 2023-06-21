using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    // int , float, bool 
    // public / private
    public float speed = 1f;
    public float jumpForce = 20f;
    private Rigidbody rg;
    public bool OnGround;

    private void OnEnable()
    {
        gameObject.transform.position = (new Vector3(0f, 2f, 0f));
    }
    private void Awake()
    {
        Debug.Log("Awake");
    }
    void Start()
    {
        Debug.Log("Start");
        rg = gameObject.GetComponent<Rigidbody>();
        gameObject.transform.position = (new Vector3(0f, 2f, 0f));
    }

    // Update is called once per frame
    void Update()
    {

        move();

    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Z) && OnGround)
        {
            rg.AddForce(Vector3.up * jumpForce);
        }
    }
    private void move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0f, 0f));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0f, 0f, -1 * speed * Time.deltaTime));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Plane")
        {
            OnGround = true;
            Debug.Log("On");       
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Plane")
        {
            OnGround = false;
            Debug.Log("off");
        }
    }
}
