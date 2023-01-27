using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableEnemy : MonoBehaviour
{
    public WorldManager worldManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameVisible()
    {
        worldManager.visibleTargets.Add(gameObject);
    }
    private void OnBecameInvisible()
    {
        worldManager.visibleTargets.Remove(gameObject);
    }
}
