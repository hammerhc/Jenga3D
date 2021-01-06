using UnityEngine;

[System.Serializable]
public class Towerblock
{
    public GameObject block;

    public bool isLocked;

    public Vector3 spawnPosition;

    public Towerblock(GameObject towerBlock, bool locked, Vector3 pos)
    {
        block = towerBlock;
        isLocked = locked;
        spawnPosition = pos;
    }
}
