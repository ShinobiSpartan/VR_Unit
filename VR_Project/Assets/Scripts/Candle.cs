using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public Player player;
    public MazeExit exit;
    public ParticleSystem flame;
    private void Update()
    {
        //flame = GetComponent<ParticleSystem>();
        //var vel = flame.velocityOverLifetime;
        //vel.y = Vector3.Distance(player.transform.position, exit.transform.position);
    }
}
