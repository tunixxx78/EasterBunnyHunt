using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody ammoRb;
    [SerializeField] float ammoSpeed;

    private void Update()
    {
        //ammoRb.AddForce(ammoRb.transform.forward * ammoSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * ammoSpeed);

        Destroy(this.gameObject, 2f);
    }
}
