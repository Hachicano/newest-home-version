using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;
    [SerializeField] public Dictionary<GameObject, List<GameObject>> objectPool = new Dictionary<GameObject, List<GameObject>>();
    [SerializeField] private int maxPoolSize = 10; // 对象池最大容量为 20(暂定)

    // 需要手动添加到对象池的对象数组
    public GameObject[] initialObjects;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

        // 将初始对象添加到对象池
        AddInitialObjectsToPool();
    }

    // 将初始对象添加到对象池的方法
    private void AddInitialObjectsToPool()
    {
        if (initialObjects != null)
        {
            foreach (GameObject obj in initialObjects)
            {
                if (obj != null)
                {
                    GameObject prefab = obj.GetComponent<Enemy>().EnemySelfPrefab;
                    if (prefab != null)
                    {
                        AddToPool(prefab, obj, true);
                    }
                }
            }
        }
    }

    public GameObject getPooledObject(GameObject _prefab, Vector3 _position, Quaternion _rotation)
    {
        if (!objectPool.ContainsKey(_prefab))
        {
            objectPool[_prefab] = new List<GameObject>();
            Debug.Log("创建List！" +  _prefab);
        }

        List<GameObject> pool = objectPool[_prefab];

        // 查找 popCount 最小或为 0 的对象
        GameObject objToReuse = null;
        int minPopCount = int.MaxValue;
        foreach (GameObject obj in pool)
        {
            ObjectSpawner spawner = obj.GetComponent<ObjectSpawner>();
            if (spawner.popCount < minPopCount)
            {
                minPopCount = spawner.popCount;
                objToReuse = obj;
            }
        }

        if (objToReuse != null && !objToReuse.activeInHierarchy)
        {
            Debug.Log("有欸");
            // 启用对象并初始化
            objToReuse.SetActive(true);
            objToReuse.transform.position = _position;
            objToReuse.transform.rotation = _rotation;
            objToReuse.GetComponent<ObjectSpawner>().initialize();// 预制体记得挂载一下这个脚本
            Debug.Log("启用了这个" + objToReuse.gameObject.name);
            return objToReuse;
        }
        /*
        if (pool.Count >= maxPoolSize)
        {
            // 对象池满，删除 popCount 最小的对象
            pool.Remove(objToReuse);
            Destroy(objToReuse);
            Debug.Log("删除了这个: " + objToReuse.gameObject.name);
        }*/

        // 创建新对象
        GameObject newObj = Instantiate(_prefab, _position, _rotation);
        newObj.GetComponent<ObjectSpawner>().selfPrefab = _prefab;
        Debug.Log("创建了这个" + newObj.gameObject.name);
        pool.Add(newObj);
        return newObj;
    }

    // 将对象加入对象池
    public void AddToPool(GameObject prefab, GameObject obj, bool isActive)
    {
        if (!objectPool.ContainsKey(prefab))
        {
            objectPool[prefab] = new List<GameObject>();
        }

        List<GameObject> pool = objectPool[prefab];

        if (pool.Count >= maxPoolSize)
        {
            // 对象池满，删除 popCount 最小的对象
            GameObject objToRemove = null;
            int minPopCount = int.MaxValue;
            foreach (GameObject pooledObj in pool)
            {

                if(!pooledObj.activeInHierarchy)
                {
                    ObjectSpawner spawner = pooledObj.GetComponent<ObjectSpawner>();
                    if (spawner.popCount < minPopCount)
                    {
                        minPopCount = spawner.popCount;
                        objToRemove = pooledObj;
                    }
                }
            }

            if (objToRemove != null)
            {
                pool.Remove(objToRemove);
                Destroy(objToRemove);
            }
        }

        // 添加对象到池
        obj.SetActive(isActive);
        pool.Add(obj);
    }

    public void returnToPool(GameObject obj)
    {
        ObjectSpawner spawner = obj.GetComponent<ObjectSpawner>();
        if (spawner != null)
        {
            if (!objectPool.ContainsKey(spawner.selfPrefab))
            {
                Debug.LogError($"对象池中不存在预制体键: {spawner.selfPrefab.name}");
                Destroy(obj);
                return;
            }

            List<GameObject> pool = objectPool[spawner.selfPrefab];
            Debug.Log(obj.gameObject.name + "对象池数量：" + pool.Count);

            if (pool.Count >= maxPoolSize)
            {
                // 对象池满，删除
                pool.Remove(obj);
                Destroy(obj);
                Debug.Log("删除了这个: " + obj.gameObject.name);
            }
            else
                obj.SetActive(false);
        }
    }
}
