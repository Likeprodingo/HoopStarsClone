using UnityEngine;

namespace Pool
{
    public class PooledObject: MonoBehaviour
    {
        
        public bool IsFree
        {
            get;
            private set;
        }
        
        public virtual void DeInit()
        {
            
        }

        public virtual void SpawnFromPool()
        {
            gameObject.SetActive(true);
            IsFree = false;
        }

        protected virtual void BeforeReturnToPool()
        {

        }

        protected virtual void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public virtual void Init()
        {

        }

        public void Free()
        {
            BeforeReturnToPool();
            ReturnToPool();
            IsFree = true;
        }
    }
}