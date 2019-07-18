using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISense {
    void Initialize();
    void UpdateSense();

}

public abstract class Sense : MonoBehaviour,ISense
{
    public abstract void Initialize();
    public abstract void UpdateSense();
    protected Aspect aspect;

    
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdateSense();
    }
}
