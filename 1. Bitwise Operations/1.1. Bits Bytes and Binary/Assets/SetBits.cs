using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBits : MonoBehaviour
{
    int bSequence = 11;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Binary: " + Convert.ToString(bSequence, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
