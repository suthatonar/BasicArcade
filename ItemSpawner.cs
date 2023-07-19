using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] int itemSpawnTotal;
    [SerializeField] GameObject[] itemPrefabList;

    [SerializeField] Transform itemContentParent;

    List<GameObject> itemList = new List<GameObject>();

    [Range(-100, 100)]
    [SerializeField] float minX, maxX, minZ, maxZ;

    public static ItemSpawner Instance;

    private void Start() => Instance = this;

    // Update is called once per frame
    void Update() => Spawner();

    public void RemoveItem(GameObject item) => itemList.Remove(item);

    public void RemoveAll()
    {
        foreach (GameObject item in itemList)
            itemList.Remove(item);

        itemList.Clear();
    }

    void Spawner()
    {
        for (int i = itemList.Count; itemList.Count < itemSpawnTotal; i++)
        {
            int randItem = Random.Range(0, itemPrefabList.Length);
            GameObject item = Instantiate(itemPrefabList[randItem], itemSpawnPos(), Quaternion.identity);
            item.transform.SetParent(itemContentParent);
            itemList.Add(item);
        }
    }

    Vector3 itemSpawnPos()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        Vector3 pos = new Vector3(x, 0.5f, z);

        return pos;
    }
}
