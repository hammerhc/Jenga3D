using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public Transform spawnPoint;
    public Transform removePosition;
    public float blockCount;

    public static bool gameOver = false;

    private static List<Towerblock> tower = new List<Towerblock>();
    private float Score = 0f;

    void Start()
    {
        buildTower();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            deleteTower();
            Score = 0;
            UIControllerGame.SetScore(Score);
            buildTower();
        }

        for (int i = tower.Count - 1; i >= 0; i--)
        {
            if (tower[i].Block.transform.position.y < removePosition.position.y)
            {
                Score += 10;
                UIControllerGame.SetScore(Score);
                Destroy(tower[i].Block);
                tower.RemoveAt(i);
            }
        }

        checkTower();
    }

    void buildTower()
    {
        float height = block.transform.localScale.y;
        for (int i = 0; i < blockCount; i++)
        {
            bool isLocked = false;
            if (i == 0 || i > blockCount - 4)
            {
                isLocked = true;
            }
            else
            {
                isLocked = false;
            }
            if (i % 2 == 1)
            {
                Vector3 blockLeft = new Vector3(spawnPoint.position.x - block.transform.localScale.x, spawnPoint.position.y + height, spawnPoint.position.z);
                Vector3 blockMid = new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z);
                Vector3 blockRight = new Vector3(spawnPoint.position.x + block.transform.localScale.x, spawnPoint.position.y + height, spawnPoint.position.z);

                tower.Add(new Towerblock(Instantiate(block, blockLeft, Quaternion.identity), isLocked, blockLeft));
                tower.Add(new Towerblock(Instantiate(block, blockMid, Quaternion.identity), isLocked, blockMid));
                tower.Add(new Towerblock(Instantiate(block, blockRight, Quaternion.identity), isLocked, blockRight));
            }
            else
            {
                Vector3 blockLeft = new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z - block.transform.localScale.x);
                Vector3 blockMid = new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z);
                Vector3 blockRight = new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z + block.transform.localScale.x);


                tower.Add(new Towerblock(Instantiate(block, blockLeft, Quaternion.Euler(new Vector3(0f, 90f, 0f))), isLocked, blockLeft));
                tower.Add(new Towerblock(Instantiate(block, blockMid, Quaternion.Euler(new Vector3(0f, 90f, 0f))), isLocked, blockMid));
                tower.Add(new Towerblock(Instantiate(block, blockRight, Quaternion.Euler(new Vector3(0f, 90f, 0f))), isLocked, blockRight));
            }
            height += block.transform.localScale.y;
        }
    }

    void deleteTower()
    {
        foreach (Towerblock towerBlock in tower)
        {
            Destroy(towerBlock.Block);
        }

        tower.Clear();
    }

    void checkTower()
    {
        if (!gameOver)
        {
            foreach (Towerblock towerBlock in tower)
            {
                if (towerBlock.IsLocked)
                {
                    if (towerBlock.Block.transform.position.z >= towerBlock.SpawnPosition.z + 2 ||
                        towerBlock.Block.transform.position.z <= towerBlock.SpawnPosition.z - 2 ||
                        towerBlock.Block.transform.position.x >= towerBlock.SpawnPosition.x + 2 ||
                       towerBlock.Block.transform.position.x <= towerBlock.SpawnPosition.x - 2)
                    {
                        gameOver = true;
                    }
                }
            }
        }
    }

    public static bool CheckBlock(GameObject block)
    {
        foreach (Towerblock towerBlock in tower)
        {
            if (block == towerBlock.Block)
            {
                return !towerBlock.IsLocked;
            }
        }
        return false;
    }

    public static void ClearList()
    {
        tower.Clear();
    }
}
