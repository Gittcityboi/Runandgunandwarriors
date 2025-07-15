using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class Player : MonoBehaviour
{
    //변수 설정 - 속도, 체력
    [SerializeField] private float speed = 1f;
    public float hp = 100f;
    public float damage;
    private Vector3 moveDirection;

    private Rigidbody playerRigidbody;
    public bool isDead = false;
    private Animator animator;
    [SerializeField] private Guns guns;

    void Start()
    {
        //Rigidbody 도입
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    { 
        
        if (hp <= 0)
        {
            Dead();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 트리거로 들어온 오브젝트의 태그가 "Bullet"인지 확인
        if (other.CompareTag("Bullet"))
        {

            // 여기에 Bullet 트리거와 닿았을 때 실행할 코드를 작성하세요.
            // 예: Bullet 오브젝트 비활성화, 특수 효과 발생 등
            damage = other.GetComponent<Bullets>().damage;
            Damaged();
            Destroy(other.gameObject);
        }
    }


    public void Damaged() //데미지 함수(체력바 damage만큼 조정 - damage 변수 지정 위치?)
    {
        animator.SetTrigger("Damaged");
        hp -= damage;
    }


    public void Dead() //사망 함수
    {
        isDead = true;
        animator.SetTrigger("Dead");
        gameObject.SetActive(false);
    }
    // 남은 필요 기능? - 이동 애니메이션(Asset 찾아서), 총기와 상호작용(발사, 재장전, 획득), 데미지 받는 경우(체력바 조정)

}

