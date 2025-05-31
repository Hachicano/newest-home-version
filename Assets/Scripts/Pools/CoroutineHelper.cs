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
                // ȷ��������ʱ��̬����ʵ��
                GameObject obj = new GameObject("CoroutineHelper");
                _instance = obj.AddComponent<CoroutineHelper>();
                DontDestroyOnLoad(obj); // �糡��������
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
        // ����ʱ���þ�̬ʵ��
        if (_instance == this)
        {
            _instance = null;
        }
    }
}