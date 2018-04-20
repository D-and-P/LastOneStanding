using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooting : MonoBehaviour {

    public LayerMask mask;
    public GameObject player;
    public GameObject rocketToSpawn;
    public Transform spawnPos;
    public PlayerMovement movement;

    private GameObject rocket;
    private float recoilForce=7f;
    private float speed=12f;
    private float fireRate=1f;
    private float nextFire;
    private Rigidbody rbRocket;
    private Rigidbody rbPlayer;

    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time>nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(shoot());
        }
    }

    private IEnumerator shoot()
    {
        rocket = Instantiate(rocketToSpawn, spawnPos.position, Quaternion.identity, spawnPos.transform) as GameObject;
        rbRocket = rocket.GetComponent<Rigidbody>();
        rbRocket.velocity = spawnPos.transform.forward * speed;
        movement.enabled = false;
        rbPlayer.velocity = spawnPos.transform.forward * -recoilForce;
        yield return new WaitForSeconds(1f);
        movement.enabled = true;
        Destroy(rocket.gameObject);
    }
}
