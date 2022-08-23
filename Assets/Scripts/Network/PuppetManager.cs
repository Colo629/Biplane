using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetManager : MonoBehaviour
{
    public class PuppetPositionData
    {
        public string puppetID { get; set; }
        public List<float> Position { get; set; }
        public List<float> Rotation { get; set; }
    }
    public static void PuppetPositionUpdate(string puppetID, Vector3 position, Quaternion rotation)
    {

    }

    public static void PuppetPositionDataHandler(PuppetPositionData puppetData)
    {

    }
}
