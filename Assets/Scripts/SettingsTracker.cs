using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsTracker : MonoBehaviour
{
    public static SettingsTracker instance;

    public float musicVolume, sfxVolume;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
