using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour {

    public GameObject player;
    public LayerMask mask;

    [SerializeField]private float Speed=2f;
    private Rigidbody rb;
    private Vector3 offset;

    private void Start()
    {
        Debug.Log("This islocal player");
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }
        move();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestroyPlane"))
        {
            DestroyObject(this.gameObject);
        }
    }
}
