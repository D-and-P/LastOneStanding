using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehav : MonoBehaviour {

    private float hitforce = 10f;
    private Rigidbody rbRocket;

    private void Start()
    {
        rbRocket = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!this.gameObject)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rbPlayer = collision.gameObject.GetComponent<Rigidbody>();
            rbPlayer.velocity = this.gameObject.transform.forward * hitforce;
        }
    }
}
