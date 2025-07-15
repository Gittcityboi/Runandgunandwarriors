using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float damage;
    public float bulletSpeed = 50f; // 총알의 이동 속도
    public float lifeTime = 7f;     // 총알이 사라지기까지의 시간

    void Start()
    {

    }

    // 총알이 발사될 때 외부에서 호출하여 방향과 속도를 설정합니다.
    public void Initialize(Vector3 direction)
    {
        // Rigidbody를 사용하여 총알에 힘을 가해 발사합니다.
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Bullet 오브젝트에 Rigidbody가 없습니다. 총알이 움직이지 않을 수 있습니다.");
        }
    }

    // (선택 사항) 충돌 처리 예시
    void OnCollisionEnter(Collision collision)
    {
        // 예시: 벽이나 다른 오브젝트에 닿으면 총알 파괴
        if (collision.gameObject.CompareTag("Opfor"))
        {
            // 적에게 데미지를 주는 로직
            Destroy(gameObject); // 총알 파괴
        }
        else if (collision.gameObject.CompareTag("Player")) 
        { 
            Destroy(gameObject); // 총알 파괴
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
