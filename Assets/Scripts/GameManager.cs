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
        GetComponent<Player>(); //IsDead 값 받기 위해 GetComp - Player 실행
    }

   
    void Update()
    {
        ammo.text = gun.cur_ammo + " / " + gun.max_ammo;
        
        
        if(player.isDead)
        {
            
            //score 계산 후 엔딩 조건 충족 판단 -> 충족 시 엔딩, 미충족 시 Game Over Scene (함수로 대체 O)
        }
    }
    /*필요 기능
     * 1. 총기 랜덤 스폰 (구체적으로 보자면, 총기를 스폰 시키고 그 주위에 구체를 형성하여 isTrigger 활성 시키고 2초간 위에 있으면
     그 총기를 획득하게 되는 방식)
     * 2. 엔딩 씬 조건 판단 후 엔딩 씬 호출
     * 3. 플레이어 따라가는 카메라 구현 - 완
     * 4. UI 조정 GameManager가 담당.


    */
}
