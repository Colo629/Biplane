using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker tracker;
    void Start()
    {
        tracker = this;
    }

}
