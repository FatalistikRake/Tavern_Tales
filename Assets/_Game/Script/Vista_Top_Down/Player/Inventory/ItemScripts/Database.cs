using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/ITem Database")]
public class Database : ScriptableObject
{
    [SerializeField] private List<InventoryItemData> _itemDatabase;

    [ContextMenu(itemName:"Set IDs")]
    public void SetItemIDs()
    {
        _itemDatabase = new List<InventoryItemData>();

        var foundItems = Resources.LoadAll<InventoryItemData>(path:"itemData").OrderBy(i => i.ID).ToList();

        var hasIDInRange = foundItems.Where(i => i.ID!= -1 && i.ID < foundItems.Count).OrderBy(i => i.ID).ToList();

        var hasIDNotInRange = foundItems.Where(i => i.ID != -1 && i.ID >= foundItems.Count).OrderBy(i => i.ID).ToList();

        var noID = foundItems.Where(i => i.ID <= -1).ToList();

        var index = 0;
        for (int i = 0; i < foundItems.Count; i++)
        {
            InventoryItemData itemToAdd;
            itemToAdd = hasIDInRange.Find(match: d => d.ID == i);

            if (itemToAdd != null)
            {
                _itemDatabase.Add(itemToAdd);
            }
            else if (index < noID.Count)
            {
                noID[index].ID = i;
                itemToAdd = noID[index];
                index++;
                _itemDatabase.Add(itemToAdd);
            }
        }

        foreach (var item in hasIDNotInRange)
        {
            _itemDatabase.Add(item);
        }
    }

    public InventoryItemData GetItem (int id)
    {
        return _itemDatabase.Find(match: i => i.ID == id);
    }
}
