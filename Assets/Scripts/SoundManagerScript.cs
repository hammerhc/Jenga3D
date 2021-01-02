using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip laserShotSound;

    private static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        laserShotSound = Resources.Load<AudioClip>("laser_shot");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "laserShot":
                audioSource.PlayOneShot(laserShotSound);
                break;
        }
    }
}
