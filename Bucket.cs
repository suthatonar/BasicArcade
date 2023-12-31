using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    List<GameObject> BucketItem = new List<GameObject>();

    bool CompleteRecipe()
    {
        bool complete = true;

        int[] RecipeCurrent = new int[Enum.GetNames(typeof(item.itemType)).Length];
        int[] MainRecipe = new int[Enum.GetNames(typeof(item.itemType)).Length];

        foreach (GameObject item in BucketItem)
        {
            RecipeCurrent[(int)item.GetComponent<item>().item_Type]++;
        }

        foreach (item.itemType type in GameManager.instance.mainRecipe.type)
        {
            MainRecipe[(int)type]++;
        }

        for (int i = 0; i < MainRecipe.Length; i++)
        {
            if (RecipeCurrent[i] != MainRecipe[i])
            {
                complete = false;
                break;
            }
        }

        return complete;
    }

    public void RemoveAllItem()
    {
        foreach (GameObject item in BucketItem)
        {
            ItemSpawner.Instance.RemoveItem(item);
            Destroy(item);
        }

        BucketItem.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<item>()) return;

        if (other.GetComponent<item>().type == item.ObjectType.Item)
        {
            BucketItem.Add(other.gameObject);

            other.gameObject.GetComponent<item>().itemStatus = item.ItemStatus.InBucket;

            if (BucketItem.Count < GameManager.instance.mainRecipe.type.Count)
            {
                if (CompleteRecipe())
                {
                    GameManager.instance.RandomRecipe();
                    RemoveAllItem();
                    GameManager.instance.AddScore(1);
                }
            }
            else if (BucketItem.Count >= GameManager.instance.mainRecipe.type.Count)
            {
                if (CompleteRecipe())
                {
                    GameManager.instance.RandomRecipe();
                    RemoveAllItem();
                    GameManager.instance.AddScore(1);
                }
                else
                {
                    GameManager.instance.WrongRecipe();
                    RemoveAllItem();
                    GameManager.instance.RandomRecipe();
                }
            }
        }
    }
}
