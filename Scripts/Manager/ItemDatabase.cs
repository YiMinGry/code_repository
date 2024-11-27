using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    private Dictionary<string, GameObject> itemObjectCache = new Dictionary<string, GameObject>();
    private Dictionary<string, GameItem> gameItemCache = new Dictionary<string, GameItem>();


    public GameObject LoadFxByName(string itemName)
    {
        if (itemObjectCache.TryGetValue(itemName, out GameObject cachedObject))
        {
            return cachedObject;
        }

        GameObject itemObject = Resources.Load<GameObject>($"Prefabs/Fx/{itemName}");
        if (itemObject == null)
        {
            Debug.LogWarning($"GameObject '{itemName}' not found in Resources.");
            return null;
        }

        itemObjectCache[itemName] = itemObject;
        return itemObject;
    }

    public GameObject LoadItemByObject(string itemName)
    {
        if (itemObjectCache.TryGetValue(itemName, out GameObject cachedObject))
        {
            return cachedObject;
        }

        GameObject itemObject = Resources.Load<GameObject>($"Prefabs/Items/{itemName}");
        if (itemObject == null)
        {
            Debug.LogWarning($"GameObject '{itemName}' not found in Resources.");
            return null;
        }

        itemObjectCache[itemName] = itemObject;
        return itemObject;
    }

    public GameItem LoadItemByData(string itemName)
    {
        if (gameItemCache.TryGetValue(itemName, out GameItem cachedData))
        {
            return cachedData;
        }

        GameObject itemObject = LoadItemByObject(itemName);
        if (itemObject == null)
        {
            return null;
        }

        GameItem gameItem = itemObject.GetComponent<GameItem>();
        if (gameItem == null)
        {
            Debug.LogWarning($"GameItem component not found on '{itemName}'.");
            return null;
        }

        gameItemCache[itemName] = gameItem;
        return gameItem;
    }

    public GameItem InstantiateItem(string itemName, Transform parent)
    {
        GameObject item = GameObject.Instantiate(LoadItemByObject(itemName), parent);
        return item.GetComponent<GameItem>();
    }
}
