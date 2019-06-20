using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public Light candle;
    void Update()
    {
        Vector2 touchValue = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        if(OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad))
        {
            if (touchValue.y > 0.5 && touchValue.x > -0.25 && touchValue.x < 0.25)
            {
                if (!(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 1)))
                    transform.position += transform.forward;
            }
            else if (touchValue.x > 0.5 && touchValue.y > -0.25 && touchValue.y < 0.25)
                transform.Rotate(new Vector3(0, 90, 0));
            else if (touchValue.x < 0.5 && touchValue.y > -0.25 && touchValue.y < 0.25)
                transform.Rotate(new Vector3(0, -90, 0));
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
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
