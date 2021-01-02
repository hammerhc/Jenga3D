using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float bulletSpeed;
    public float fireRate;

    private float nextTimeToFire = 0f; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * bulletSpeed);
                }
            }
        }
    }
}
