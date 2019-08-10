using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [System.Serializable]
    public struct Item
    {
        public string name;
        public Sprite sprite;
        public string[] isseKatega;
    }

    public Item[] items;
}
