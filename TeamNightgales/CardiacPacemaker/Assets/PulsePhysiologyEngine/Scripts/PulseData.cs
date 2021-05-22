/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using System.Collections.Generic;
using UnityEngine;

// Serializable List<float> wrapper
// https://answers.unity.com/questions/289692/serialize-nested-lists.html
[System.Serializable]
public class FloatList
{
    public List<float> list;
    public FloatList()
    {
        list = new List<float>();
    }
    public FloatList(int capacity)
    {
        list = new List<float>(capacity);
    }
    public void Clear()
    {
        list.Clear();
    }
    public void Add(float value)
    {
        list.Add(value);
    }
    public void Set(int index, float value)
    {
        list[index] = value;
    }
    public float Get(int index)
    {
        return list[index];
    }
    public int Count
    {
        get
        {
            return list.Count;
        }
    }
    public bool IsEmpty()
    {
        return Count == 0;
    }
}

// Data container for Pulse vitals
public class PulseData: ScriptableObject
{
    public string[] fields;             // name of the data fields
    public FloatList timeStampList;     // list of time points
    public List<FloatList> valuesTable; // table holding a value for each time point for each data field
}
