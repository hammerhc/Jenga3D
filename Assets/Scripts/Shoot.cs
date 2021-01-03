using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float bulletSpeed;
    public float fireRate;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && Time.timeScale == 1)
        {
            SoundManagerScript.PlaySound("laserShot");

            nextTimeToFire = Time.time + 1f / fireRate;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100))
            {
                if (hit.rigidbody != null)
                {
                    if (GameManager.CheckBlock(hit.transform.gameObject))
                    {
                        hit.rigidbody.AddForce(-hit.normal * bulletSpeed);
                    }
                }
            }
        }
    }


}
