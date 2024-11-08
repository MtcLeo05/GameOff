using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> keys = new ();
    [SerializeField] private List<Tvalue> values = new ();
    
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        
        foreach (var (key, value) in this)
        {
            keys.Add(key);
            values.Add(value);
        }
    }

    public void OnAfterDeserialize()
    {
        Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogWarning("OnAfterDeserialize: keys.Count != values.Count");
            return;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            Add(keys[i], values[i]);
        }
    }
}
