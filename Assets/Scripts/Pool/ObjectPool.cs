using System.Collections.Generic;
using System.Linq;
using GameController;
using UnityEngine;
using Util;

namespace Pool
{
    public class ObjectPool : GameObjectSingleton<ObjectPool>
    {

        private readonly Dictionary<PooledObject, Queue<PooledObject>> _pooledObjects = new Dictionary<PooledObject, Queue<PooledObject>>();

        protected override void Init()
        {
            base.Init();
            GameManager.GameEnded += GameManager_GameEnded;
            PreparePool();
        }
        
        protected override void DeInit()
        {
            base.DeInit();
            GameManager.GameEnded -= GameManager_GameEnded;
        }

        private void GameManager_GameEnded()
        {
            FreePool();
        }
        
        private void PreparePool()
        {
            Prepare(AssetManager.Instance.Ball,1);
        }

        private PooledObject PrepareObject(PooledObject pooledBehaviour)
        {
            PooledObject obj = Instantiate(pooledBehaviour, gameObject.transform);
            obj.Init();
            obj.Free();

            return obj;
        }

        private void Prepare(PooledObject pooledObject, int count)
        {
            if (_pooledObjects.TryGetValue(pooledObject, out Queue<PooledObject> objectPool))
            {
                for (int i = 0; i < count - objectPool.Count; i++)
                {
                    var obj = PrepareObject(pooledObject);
                    objectPool.Enqueue(obj);
                }
            }
            else
            {
                objectPool = new Queue<PooledObject>();
                
                for (int i = 0; i < count; i++)
                {
                    var obj = PrepareObject(pooledObject);
                    objectPool.Enqueue(obj);
                }
                _pooledObjects.Add(pooledObject, objectPool);
            }
        }

        public T Get<T>(PooledObject obj, Vector3 position, Quaternion rotation = default, Transform parent = null) where T : PooledObject
        {
            if (!_pooledObjects.ContainsKey(obj))
            {
                Prepare(obj, 1);
                Debug.LogError("PoolObjects with Tag " + obj + " doesn't exist ..");
            }

            var pooledObject = _pooledObjects[obj].FirstOrDefault(item => item.IsFree);

            if (pooledObject == null)
            {
                pooledObject = PrepareObject(obj);

                _pooledObjects[obj].Enqueue(pooledObject);

#if UNITY_EDITOR || FORCE_DEBUG
                Debug.LogError($"prepare object: {obj}");
#endif           
            }

            if (parent)
            {
                pooledObject.transform.SetParent(parent);
            }

            pooledObject.transform.position = position;
            pooledObject.transform.rotation = rotation;

            pooledObject.SpawnFromPool();

            return (T)pooledObject;
        }

        public void FreeObject(PooledObject obj)
        {
            obj.Free();
        }

        private void FreePool()
        {
            foreach (var pair in _pooledObjects)
            {
                foreach (var obj in pair.Value)
                {
                    FreeObject(obj);
                }
            }
        }
        
        private void DestroyAll(PooledObject prefab)
        {
            if (_pooledObjects.TryGetValue(prefab, out Queue<PooledObject> queue))
            {
                foreach (var entry in queue)
                {
                    Destroy(entry.gameObject);
                }

                _pooledObjects.Remove(prefab);
            }
        }
    }
}