using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public Transform spawnPoint;
    public Transform removePosition;
    public float blockCount;
    public bool gameOver;

    private List<Towerblock> tower = new List<Towerblock>();
    private float score = 0f;
    private float highScore = 0f;

    void Start()
    {
        BuildTower();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyTower();
            score = 0;
            FindObjectOfType<UIControllerGame>().SetScore(score);
            BuildTower();
        }

        for (int i = tower.Count - 1; i >= 0; i--)
        {
            if (tower[i].block.transform.position.y < removePosition.position.y)
            {
                score += 10;
                FindObjectOfType<UIControllerGame>().SetScore(score);
                Destroy(tower[i].block);
                tower.RemoveAt(i);
            }
        }

        CheckTower();
    }

    public void BuildTower()
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

    public void DestroyTower()
    {
        foreach (Towerblock towerBlock in tower)
        {
            Destroy(towerBlock.block);
        }

        tower.Clear();
    }

    void CheckTower()
    {
        if (!gameOver)
        {
            foreach (Towerblock towerBlock in tower)
            {
                if (towerBlock.isLocked)
                {
                    if (towerBlock.block.transform.position.z >= towerBlock.spawnPosition.z + 5 ||
                        towerBlock.block.transform.position.z <= towerBlock.spawnPosition.z - 5 ||
                        towerBlock.block.transform.position.x >= towerBlock.spawnPosition.x + 5 ||
                       towerBlock.block.transform.position.x <= towerBlock.spawnPosition.x - 5)
                    {
                        gameOver = true;
                    }
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }

    public bool CheckBlock(GameObject block)
    {
        foreach (Towerblock towerBlock in tower)
        {
            if (block == towerBlock.block)
            {
                return !towerBlock.isLocked;
            }
        }
        return false;
    }

    public void ClearList()
    {
        tower.Clear();
    }

    public void SetHighScore(float highScoreValue)
    {
        if (highScoreValue > highScore)
        {
            highScore = highScoreValue;
        }
    }

    public void SetHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }

    public float GetHighScore()
    {
        return highScore;
    }

    public float GetScore()
    {
        return score;
    }

    public void Save()
    {
        SaveSystem.SaveData(this, FindObjectOfType<AudioManager>());
    }

    public void Load()
    {
        GameData data = SaveSystem.LoadData();

        if (data != null)
        {
            SetHighScore(data.highScore);
            FindObjectOfType<AudioManager>().ChangeVolume("laserShot", data.soundVolume / 100);
            FindObjectOfType<AudioManager>().ChangeVolume("backgroundMusic", data.musicVolume / 100);
        }
    }
}
