using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    //add checkpoints to define where the camera has to stop following the charactor
    [SerializeField] Transform[] checkPoints = null;

    public Transform[] GetCheckPoints() {
        return checkPoints;
    }
}
