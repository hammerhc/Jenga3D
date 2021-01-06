using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float bulletSpeed;
    public float fireRate;
    public ParticleSystem laserBullet;
    public ParticleSystem laserShot;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && Time.timeScale == 1)
        {
            SoundManagerScript.PlaySound("laserShot");
            laserShot.Play();

            nextTimeToFire = Time.time + 1f / fireRate;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100))
            {
                var dir = hit.point - laserBullet.transform.position;
                var rot = Quaternion.LookRotation(dir, Vector3.up);
                laserBullet.transform.rotation = rot;
                laserBullet.Play();
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
