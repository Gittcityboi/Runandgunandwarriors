using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead == true)
        {
            //score ��� �� ���� ���� ���� �Ǵ� -> ���� �� ����, ������ �� Game Over Scene (�Լ��� ��ü O)
        }
    }




}
