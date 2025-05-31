using UnityEngine;
using System;
using System.Collections;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;

    public static CoroutineHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                // 确保在运行时动态创建实例
                GameObject obj = new GameObject("CoroutineHelper");
                _instance = obj.AddComponent<CoroutineHelper>();
                DontDestroyOnLoad(obj); // 跨场景不销毁
            }
            return _instance;
        }
    }

    public void ExecuteAfterDelay(float delay, Action action)
    {
        StartCoroutine(RunAfterDelay(delay, action));
    }

    private IEnumerator RunAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    void OnDestroy()
    {
        // 销毁时重置静态实例
        if (_instance == this)
        {
            _instance = null;
        }
    }
}