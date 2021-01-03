using UnityEngine;

namespace Assets.Scripts
{
    public class Towerblock
    {
        private GameObject block;
        private bool isLocked;

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

        public Towerblock(GameObject towerBlock, bool locked)
        {
            Block = towerBlock;
            IsLocked = locked;
        }
    }
}
