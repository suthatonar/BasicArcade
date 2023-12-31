using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] float vision_radius;
    [SerializeField] LayerMask TargetMask;

    // Start is called before the first frame update
    void Start() => agent = GetComponent<NavMeshAgent>();

    // Update is called once per frame
    void Update() => Patroll();

    void Patroll()
    {
        if (!GameManager.instance.GameStart) return;

        if (agent != null)
            agent.SetDestination(FindClosetObjectPos());
    }

    Vector3 FindClosetObjectPos()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, vision_radius, TargetMask);

        List<ObjectInVisionList> objectInVisionList = new List<ObjectInVisionList>();

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<item>().itemStatus == item.ItemStatus.None)
                    objectInVisionList.Add(new ObjectInVisionList(Vector3.Distance(transform.position, colliders[i].transform.position), colliders[i].gameObject));
            }
        }

        return SortObject(objectInVisionList);
    }

    Vector3 SortObject(List<ObjectInVisionList> objectInVisionList)
    {
        for (int i = 0; i < objectInVisionList.Count; i++)
        {
            ObjectInVisionList objectInVision = new ObjectInVisionList(objectInVisionList[i].distance, objectInVisionList[i].Object);

            int j;

            for (j = i - 1; j >= 0 && objectInVisionList[i].distance > objectInVision.distance; j--)
            {
                objectInVisionList[j + 1] = objectInVisionList[j];
            }

            objectInVisionList[j + 1] = objectInVision;
        }

        Vector3 pos = (objectInVisionList.Count > 0) ? objectInVisionList[0].Object.transform.position : transform.position;

        return pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.GetComponent<item>().type == item.ObjectType.Item)
        {
            ItemSpawner.Instance.RemoveItem(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}

[System.Serializable]
public class ObjectInVisionList
{
    public float distance;
    public GameObject Object;
    public ObjectInVisionList(float distance, GameObject gameObject)
    {
        this.distance = distance;
        this.Object = gameObject;
    }
}
