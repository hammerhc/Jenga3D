using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public Transform spawnPoint;

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

        for (int i = tower.Count - 1; i >= 0; i--)
        {
            if (tower[i].transform.position.y < 0)
            {
                Destroy(tower[i]);
                tower.RemoveAt(i);
            }
        }
    }

    void buildTower()
    {
        float height = 1f;
        for (int i = 0; i < 18; i++)
        {
            if (i % 2 == 1)
            {
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x - 2.5f, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity));
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity));
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x + 2.5f, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity));
            }
            else
            {
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z - 2.5f), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z + 2.5f), Quaternion.Euler(new Vector3(0f, 90f, 0f))));

                tower.Add(Instantiate(block, new Vector3(0, height, -2.5f), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
                tower.Add(Instantiate(block, new Vector3(0, height, 0), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
                tower.Add(Instantiate(block, new Vector3(0, height, 2.5f), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
            }
            height += 1.5f;
        }
    }

    void deleteTower()
    {
        foreach(GameObject towerBlock in tower)
        {
            Destroy(towerBlock);
        }

        tower.Clear();
    }
}
