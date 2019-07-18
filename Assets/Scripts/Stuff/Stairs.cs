using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public int currentStair;
    public List<Transform> stairs { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        currentStair = -1;
        stairs = new List<Transform>();
        foreach (Transform tr in transform) {
            stairs.Add(tr);
        }
        stairs.Remove(stairs[stairs.Count-1]);
        stairs.Remove(stairs[stairs.Count-1]);
    }
}
