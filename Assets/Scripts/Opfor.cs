using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opfor : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Opfor의 이동 속도
    private Transform playerTransform; // Player 오브젝트의 Transform 참조
    public float hp = 75f;
    private float damage;

    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        /* 구현할 적은 플레이어를 따라옴, 낮은 확률로 총기를 들고 스폰하는 적이 있음, 이동 속도는 플레이어의 0.7~0.8배 정도
         */

        animator = GetComponent<Animator>();

        // "Player" 태그를 가진 오브젝트를 찾습니다.
        // 게임 시작 시 한 번만 찾으므로 효율적입니다.
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. Player 오브젝트에 'Player' 태그가 지정되었는지 확인해주세요.");
            // 플레이어를 찾지 못하면 스크립트를 비활성화하거나 오브젝트를 파괴할 수 있습니다.
            enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // 1. Player를 향하는 방향 벡터 계산
            // Opfor의 위치에서 Player의 위치를 빼면 Player를 향하는 방향 벡터가 나옵니다.
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // 2. Opfor를 Player 방향으로 이동
            // Time.deltaTime을 곱하여 프레임 속도에 독립적으로 이동하게 합니다.
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

            // (선택 사항) Player를 바라보도록 회전
            // 2D 게임이라면 Z축 회전만 필요할 수 있습니다.
            // 3D 게임이라면 Quaternion.LookRotation 사용
            if (directionToPlayer != Vector3.zero) // 0 벡터가 아닐 때만 회전 (오류 방지)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed * 5f); // 5f는 회전 속도 조절 계수
            }
        }

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
        animator.SetTrigger("Dead");
        Destroy(gameObject, 0.7f);
    }



}
