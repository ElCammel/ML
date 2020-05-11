using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCLionLibWrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("With CLion Dll : ");
        Debug.Log(CLionLibWrapper.my_add(42, 51));
        Debug.Log(CLionLibWrapper.my_mult(2, 3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
