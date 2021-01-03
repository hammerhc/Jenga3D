using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public Transform spawnPoint;
    public Transform removePosition;
    public float blockCount;
    
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
                tower.Add(new Towerblock(Instantiate(block, new Vector3(spawnPoint.position.x - block.transform.localScale.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity), isLocked));
                tower.Add(new Towerblock(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity), isLocked));
                tower.Add(new Towerblock(Instantiate(block, new Vector3(spawnPoint.position.x + block.transform.localScale.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.identity), isLocked));
            }
            else
            {
                tower.Add(new Towerblock(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z - block.transform.localScale.x), Quaternion.Euler(new Vector3(0f, 90f, 0f))), isLocked));
                tower.Add(new Towerblock(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z), Quaternion.Euler(new Vector3(0f, 90f, 0f))), isLocked));
                tower.Add(new Towerblock(Instantiate(block, new Vector3(spawnPoint.position.x, spawnPoint.position.y + height, spawnPoint.position.z + block.transform.localScale.x), Quaternion.Euler(new Vector3(0f, 90f, 0f))), isLocked));
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
