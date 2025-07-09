using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    //변수 설정 - 속도, 체력
    [SerializeField] private float speed = 1f;
    [SerializeField] private float hp = 100f;
    [SerializeField] private Slider HpBar;

    private Rigidbody playerRigidbody;
    public bool isDead = false;

    void Start()
    {
        //Rigidbody 도입
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    { 
        //이동 기능
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        playerRigidbody.velocity = newVelocity;
    }

    public void Damaged() //데미지 함수(체력바 damage만큼 조정 - damage 변수 지정 위치?)
    {
        
    }


    public void Dead() //사망 함수
    {
        gameObject.SetActive(false);
        isDead = true;
    }
    // 남은 필요 기능? - 이동 애니메이션, 총기와 상호작용(발사, 재장전, 획득), 데미지 받는 경우(체력바 조정)

}

