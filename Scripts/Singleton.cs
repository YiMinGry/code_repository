using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static readonly object lockObj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        GameObject obj = GameObject.Find(typeof(T).Name);
                        if (obj == null)
                        {
                            obj = new GameObject(typeof(T).Name);
                            instance = obj.AddComponent<T>();
                        }
                        else
                        {
                            instance = obj.GetComponent<T>();
                        }
                    }
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}