using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CLionLibWrapper : MonoBehaviour
{
    [DllImport("ML")]
    public extern static int my_add(int x, int y);

    [DllImport("ML")]
    public extern static int my_mult(int x, int y);
}
