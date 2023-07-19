using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<item>().type == item.ObjectType.Item)
        {
            ItemSpawner.Instance.RemoveItem(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
