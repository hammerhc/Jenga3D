using UnityEngine;

public class Towerblock
{
    private GameObject block;
    private bool isLocked;
    private Vector3 spawnPosition;

    public GameObject Block
    {
        get
        {
            return block;
        }
        set
        {
            block = value;
        }
    }

    public bool IsLocked
    {
        get
        {
            return isLocked;
        }
        set
        {
            isLocked = value;
        }
    }

    public Vector3 SpawnPosition
    {
        get
        {
            return spawnPosition;
        }
        set
        {
            spawnPosition = value;
        }
    }

    public Towerblock(GameObject towerBlock, bool locked, Vector3 pos)
    {
        Block = towerBlock;
        IsLocked = locked;
        SpawnPosition = pos;
    }
}
