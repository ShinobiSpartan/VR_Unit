using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAccepter : MonoBehaviour
{
    private void Awake()
    {
        PlayerInput.onTriggerDown += TriggerDown;   
    }

    private void TriggerDown()
    {

    }

    private void TriggerUp()
    {

    }
}
