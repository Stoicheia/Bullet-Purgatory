using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    public Equippable[] items;
    public Player[] players;
    public Dictionary<string, Equippable> getItem = new Dictionary<string, Equippable>();
    public Dictionary<string, Player> getPlayer = new Dictionary<string, Player>();
    public void UpdateReferences()
    {
        try
        {
            Dictionary<string, Equippable> newItems = new Dictionary<string, Equippable>();
            foreach (var item in items)
            {
                if (item == null) { continue; }
                newItems.Add(item.ID, item);
            }

            Dictionary<string, Player> newPlayers = new Dictionary<string, Player>();
            foreach (var player in players)
            {
                if (player == null) { continue; }
                newPlayers.Add(player.id, player);
            }
            getItem = newItems;
            getPlayer = newPlayers;
        }
        catch
        {
            Debug.LogError("null exception or some shit, database not updated");
        }
        foreach(KeyValuePair<string, Equippable> e in getItem)
        {
            Debug.Log(e.Key + ": " + e.Value.name);
        }
        Debug.Log("Updated");
    }

    public List<Equippable> GetItems(List<string> itemIDs)
    {
        List<Equippable> myItems = new List<Equippable>();
        foreach(var itemID in itemIDs)
        {
            if (itemID == "" || itemID == null) continue;
            Equippable item = getItem[itemID];
            if(item!=null)
                myItems.Add(item);
        }
        return myItems;
    }

    public Equippable GetItem(string itemID)
    {
        if (!getItem.ContainsKey(itemID)) return null;
        else return getItem[itemID];
    }

    public Player GetPlayer(string playerID)
    {
        if (!getPlayer.ContainsKey(playerID)) return null;
        else return getPlayer[playerID];
    }
}
