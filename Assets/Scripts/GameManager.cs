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
        GetComponent<Player>(); //IsDead �� �ޱ� ���� GetComp - Player ����
    }

   
    void Update()
    {
        if(player.isDead == true)
        {
            //score ��� �� ���� ���� ���� �Ǵ� -> ���� �� ����, ������ �� Game Over Scene (�Լ��� ��ü O)
        }
    }




}
