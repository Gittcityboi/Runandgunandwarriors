using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollower : MonoBehaviour
{
    [Header("팔로우 설정")]
    public Transform playerTransform; // 플레이어 오브젝트의 Transform (에디터에서 할당)
    public Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f); // 플레이어로부터의 총의 상대적 위치 (오프셋)
    public bool smoothFollow = false; // 부드러운 따라가기 여부
    public float smoothSpeed = 10f; // 부드러운 따라가기 속도 (smoothFollow가 true일 때만 적용)

    void Start()
    {
        // 플레이어 트랜스폼이 할당되지 않았다면 "Player" 태그를 가진 오브젝트를 찾습니다.
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform;
            }
            else
            {
                Debug.LogError("GunFollowPlayer: Player 오브젝트를 찾을 수 없습니다. Player 오브젝트에 'Player' 태그가 지정되었는지 확인하거나 Inspector에서 직접 할당해주세요.");
                enabled = false; // 스크립트 비활성화
            }
        }
    }

    void LateUpdate() // LateUpdate에서 처리하여 플레이어 이동 후 위치를 업데이트
    {
        if (playerTransform == null)
        {
            return; // 플레이어가 없으면 아무것도 하지 않음
        }

        // 목표 위치 계산: 플레이어 위치 + 오프셋
        Vector3 targetPosition = playerTransform.position + offset;

        if (smoothFollow)
        {
            // Lerp를 사용하여 현재 위치에서 목표 위치로 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            // 직접 목표 위치로 설정 (즉각적인 따라가기)
            transform.position = targetPosition;
        }

        // (선택 사항) 총이 플레이어를 따라 회전하게 하려면
        // transform.rotation = playerTransform.rotation;
        // 또는 특정 방향으로 총이 고정되게 하려면 Quaternion.Euler(X, Y, Z) 사용
    }
}
