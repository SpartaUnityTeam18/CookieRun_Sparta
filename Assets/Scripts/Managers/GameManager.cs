using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float timePassed;

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    public void StartGame()
    {
        timePassed = 0;
    }
}
