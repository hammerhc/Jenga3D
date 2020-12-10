using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject towerRow;

    private List<GameObject> tower = new List<GameObject>();

    void Start()
    {
        buildTower();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            deleteTower();
            buildTower();
        }
    }

    void buildTower()
    {
        float height = 1f;
        for (int i = 0; i < 18; i++)
        {
            if (i % 2 == 1)
            {
                tower.Add(Instantiate(towerRow, new Vector3(0, height, 0), Quaternion.identity));
            }
            else
            {
                tower.Add(Instantiate(towerRow, new Vector3(0, height, 0), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
            }
            height += 1.5f;
        }
    }

    void deleteTower()
    {
        foreach(GameObject towerRow in tower)
        {
            Destroy(towerRow);
        }
    }
}
