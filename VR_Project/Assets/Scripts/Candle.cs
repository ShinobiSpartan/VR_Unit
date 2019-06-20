using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public Light candle;
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        { 
            if (candle.enabled)
            {
                candle.enabled = false;
            }
            else
            {
                candle.enabled = true;
            }
        }
    }
}
