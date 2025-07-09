using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private Player player;
    
    
    void Start()
    {
        GetComponent<Player>(); //IsDead 값 받기 위해 GetComp - Player 실행
    }

   
    void Update()
    {
        if(player.isDead == true)
        {
            //score 계산 후 엔딩 조건 충족 판단 -> 충족 시 엔딩, 미충족 시 Game Over Scene (함수로 대체 O)
        }
    }




}
