using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Equippable[] items;
    public Player[] players;
    public Dictionary<string, Equippable> GetItem = new Dictionary<string, Equippable>();
    public Dictionary<string, Player> GetPlayer = new Dictionary<string, Player>();
    public void OnAfterDeserialize()
    {
        GetItem = new Dictionary<string, Equippable>();
        foreach(var item in items)
        {
            if (item == null) continue;
            GetItem.Add(item.ID, item);
        }

        GetPlayer = new Dictionary<string, Player>();
        foreach(var player in players)
        {
            if (player == null) continue;
            GetPlayer.Add(player.id, player);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }

    public List<Equippable> GetItems(List<string> itemIDs)
    {
        List<Equippable> myItems = new List<Equippable>();
        foreach(var itemID in itemIDs)
        {
            Equippable item = GetItem[itemID];
            if(item!=null)
                myItems.Add(item);
        }
        return myItems;
    }
}
