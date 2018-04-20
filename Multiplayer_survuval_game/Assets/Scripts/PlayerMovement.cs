using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour {

    public GameObject player;
    public LayerMask mask;

    [SerializeField]private float Speed=2f;
    private float cameraDistance=4f;
    private float cameraheight = 3f;
    private Rigidbody rb;
    private Transform maincamera;
    private Vector3 offset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        maincamera = Camera.main.transform;
        offset = new Vector3(0f,cameraheight,-cameraDistance);
        setCamera();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        move();
        moveCamera();
        Rotate();
    }

    //ro move player
    private void move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = Vector3.right * x;
        Vector3 moveVertical = Vector3.forward * z;

        rb.velocity = (moveHorizontal + moveVertical).normalized * Speed;
    }

    //to rotate player
    private void Rotate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out hit,Mathf.Infinity,mask))
        {
            Vector3 Rotation = hit.point - player.transform.position;
            Rotation.y = 0f;
            Quaternion newRotation =  Quaternion.LookRotation(Rotation);
            rb.MoveRotation(newRotation);
        }
    }
    //setInitial Camera
    private void setCamera()
    {
        maincamera.position = transform.position;
        maincamera.rotation = transform.rotation;
        maincamera.Translate(offset);
        maincamera.LookAt(transform.position);
    }
    //move Camera
    private void moveCamera()
    {
        offset = maincamera.transform.position - transform.position;
        maincamera.transform.position = transform.position + offset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestroyPlane"))
        {
            DestroyObject(this.gameObject);
        }
    }
}
