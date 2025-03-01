using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int maxItemsToDrop;
    [SerializeField] private ItemData[] ItemPool;
    private List<ItemData> possibleDrop = new List<ItemData>();

    [SerializeField] private GameObject dropPrefabs;

    public virtual void GenerateDrop()
    {
        if (ItemPool.Length == 0)
        {
            Debug.Log("Item pool is empty. Enemy cannot drop items");
            return;
        }

        foreach (ItemData item in ItemPool)
        {
            if (item != null && Random.Range(0, 100) < item.dropChange)
            {
                possibleDrop.Add(item);
            }
        }

        for (int i = 0; i < maxItemsToDrop; i++)
        {
            if(possibleDrop.Count > 0)
            {
                int randomIndex = Random.Range(0, possibleDrop.Count);
                ItemData itemToDrop = possibleDrop[randomIndex];

                DropItem(itemToDrop);
                possibleDrop.Remove(itemToDrop);
            }
        }

    }


    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefabs, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        newDrop.GetComponent<itemObject>().SetupItem(_itemData, randomVelocity);
    }
}
