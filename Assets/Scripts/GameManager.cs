using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public Transform spawnPoint;
    public Transform removePosition;
    public float blockCount;

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
            if (tower[i].transform.position.y < removePosition.position.y)
            {
                Destroy(tower[i]);
                tower.RemoveAt(i);
            }
        }
    }

    void buildTower()
    {
        float height = block.transform.localScale.y;
        for (int i = 0; i < blockCount; i++)
        {
            if (i % 2 == 1)
            {
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x - block.transform.localScale.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity));
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity));
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x + block.transform.localScale.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity));
            }
            else
            {
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z - block.transform.localScale.x), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
                tower.Add(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z + block.transform.localScale.x), Quaternion.Euler(new Vector3(0f, 90f, 0f))));
            }
            height += block.transform.localScale.y;
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
