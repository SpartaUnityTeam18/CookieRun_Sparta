using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //게임 흐른 시간
    public float timePassed;

    private void Update()
    {
        timePassed += Time.deltaTime; //시간 최신화
    }

    public void StartGame()//게임 시작
    {
        timePassed = 0;
    }
}
