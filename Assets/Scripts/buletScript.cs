using UnityEngine;

public class buletScript : MonoBehaviour
{

    public float lifetime;

    void Awake()
    {
        Destroy(gameObject, lifetime);    
    }
}
