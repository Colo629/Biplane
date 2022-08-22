using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        aS = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        aS.Play();
    }
}
