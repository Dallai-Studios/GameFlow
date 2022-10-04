using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFlow.Application.Utilities
{
    public class SerializableDictionary { }
    
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : SerializableDictionary, IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<SerializableKeyValuePair> list = new List<SerializableKeyValuePair>();
     
        [Serializable]
        public struct SerializableKeyValuePair
        {
            public TKey key;
            public TValue value;
     
            public SerializableKeyValuePair(TKey Key, TValue Value)
            {
                this.key = Key;
                this.value = Value;
            }
     
            public void SetValue(TValue Value)
            {
                this.value = Value;
            }
        }
     
        private Dictionary<TKey, uint> KeyPositions => this._keyPositions.Value;
        private Lazy<Dictionary<TKey, uint>> _keyPositions;
     
        public SerializableDictionary()
        {
            this._keyPositions = new Lazy<Dictionary<TKey, uint>>(this.MakeKeyPositions);
        }
     
        public SerializableDictionary(IDictionary<TKey, TValue> Dictionary)
        {
            this._keyPositions = new Lazy<Dictionary<TKey, uint>>(this.MakeKeyPositions);
     
            if (Dictionary == null)
            {
                throw new ArgumentException("The passed dictionary is null.");
            }
     
            foreach (KeyValuePair<TKey, TValue> pair in Dictionary)
            {
                this.Add(pair.Key, pair.Value);
            }
        }
     
        private Dictionary<TKey, uint> MakeKeyPositions()
        {
            int numEntries = this.list.Count;
            
            Dictionary<TKey, uint> result = new Dictionary<TKey, uint>(numEntries);
            for (int index = 0; index < numEntries; ++index) 
                result[this.list[index].key] = (uint) index;

            return result;
        }
     
        public void OnBeforeSerialize() { }
     
        public void OnAfterDeserialize() => this._keyPositions = new Lazy<Dictionary<TKey, uint>>(this.MakeKeyPositions);

        #region IDictionary
        public TValue this[TKey Key]
        {
            get => this.list[(int)this.KeyPositions[Key]].value;
            set
            {
                if (this.KeyPositions.TryGetValue(Key, out uint index))
                {
                    this.list[(int) index].SetValue(value);
                }
                else
                {
                    this.KeyPositions[Key] = (uint)this.list.Count;

                    this.list.Add(new SerializableKeyValuePair(Key, value));
                }
            }
        }
     
        public ICollection<TKey> Keys => this.list.Select(Tuple => Tuple.key).ToArray();
        public ICollection<TValue> Values => this.list.Select(Tuple => Tuple.value).ToArray();
     
        public void Add(TKey Key, TValue Value)
        {
            if (this.KeyPositions.ContainsKey(Key))
            {
                throw new ArgumentException("An element with the same key already exists in the dictionary.");
            }
            else
            {
                this.KeyPositions[Key] = (uint)this.list.Count;

                this.list.Add(new SerializableKeyValuePair(Key, Value));
            }
        }
     
        public bool ContainsKey(TKey Key) => this.KeyPositions.ContainsKey(Key);
     
        public bool Remove(TKey Key)
        {
            if (this.KeyPositions.TryGetValue(Key, out uint index))
            {
                Dictionary<TKey, uint> kp = this.KeyPositions;
     
                kp.Remove(Key);

                this.list.RemoveAt((int) index);

                int numEntries = this.list.Count;
     
                for (uint i = index; i < numEntries; i++)
                {
                    kp[this.list[(int) i].key] = i;
                }
     
                return true;
            }
     
            return false;
        }
     
        public bool TryGetValue(TKey Key, out TValue Value)
        {
            if (this.KeyPositions.TryGetValue(Key, out uint index))
            {
                Value = this.list[(int) index].value;
     
                return true;
            }
     
            Value = default;
                
            return false;
        }
        #endregion
     
        #region ICollection
        public int Count => this.list.Count;
        public bool IsReadOnly => false;
     
        public void Add(KeyValuePair<TKey, TValue> Kvp) => this.Add(Kvp.Key, Kvp.Value);
     
        public void Clear()
        {
            this.list.Clear();
            this.KeyPositions.Clear();
        }
     
        public bool Contains(KeyValuePair<TKey, TValue> Kvp) => this.KeyPositions.ContainsKey(Kvp.Key);
     
        public void CopyTo(KeyValuePair<TKey, TValue>[] Array, int ArrayIndex)
        {
            int numKeys = this.list.Count;
     
            if (Array.Length - ArrayIndex < numKeys)
            {
                throw new ArgumentException("arrayIndex");
            }
     
            for (int i = 0; i < numKeys; ++i, ++ArrayIndex)
            {
                SerializableKeyValuePair entry = this.list[i];
     
                Array[ArrayIndex] = new KeyValuePair<TKey, TValue>(entry.key, entry.value);
            }
        }
     
        public bool Remove(KeyValuePair<TKey, TValue> Kvp) => this.Remove(Kvp.Key);
        #endregion
     
        #region IEnumerable
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.list.Select(ToKeyValuePair).GetEnumerator();
     
            KeyValuePair<TKey, TValue> ToKeyValuePair(SerializableKeyValuePair Skvp)
            {
                return new KeyValuePair<TKey, TValue>(Skvp.key, Skvp.value);
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        #endregion
    }
}