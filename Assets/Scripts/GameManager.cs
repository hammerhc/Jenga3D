using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject towerRow;

    void Start()
    {
        float height = 1f;
        for (int i = 0; i < 26; i++)
        {
            if (i % 2 == 1)
            {
                Instantiate(towerRow, new Vector3(0, height, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(towerRow, new Vector3(0, height, 0), Quaternion.Euler(new Vector3(0f, 90f, 0f)));
            }
            height += 1.5f;
        }
    }
}
