using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    private Animator animator;
    [SerializeField] Guns gun;
    [SerializeField] Canvas canvas;
    [SerializeField] Text ammo;

    void Start()
    {
        GetComponent<Player>(); //IsDead �� �ޱ� ���� GetComp - Player ����
    }

   
    void Update()
    {
        ammo.text = gun.cur_ammo + " / " + gun.max_ammo;
        
        
        if(player.isDead)
        {
            
            //score ��� �� ���� ���� ���� �Ǵ� -> ���� �� ����, ������ �� Game Over Scene (�Լ��� ��ü O)
        }
    }
    /*�ʿ� ���
     * 1. �ѱ� ���� ���� (��ü������ ���ڸ�, �ѱ⸦ ���� ��Ű�� �� ������ ��ü�� �����Ͽ� isTrigger Ȱ�� ��Ű�� 2�ʰ� ���� ������
     �� �ѱ⸦ ȹ���ϰ� �Ǵ� ���)
     * 2. ���� �� ���� �Ǵ� �� ���� �� ȣ��
     * 3. �÷��̾� ���󰡴� ī�޶� ���� - ��
     * 4. UI ���� GameManager�� ���.


    */
}
