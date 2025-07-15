using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f; // 캐릭터 이동 속도 (초당 미터)
    public float rotationSpeed = 720.0f; // 캐릭터 회전 속도 (초당 각도)

    private Animator animator; // 캐릭터의 Animator 컴포넌트
    private Vector3 currentMoveDirection; // 현재 입력된 이동 방향

    void Start()
    {
        // 시작 시 Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // Animator 컴포넌트가 없는 경우 에러 메시지 출력
        if (animator == null)
        {
            Debug.LogError("PlayerMovementController: Animator 컴포넌트를 찾을 수 없습니다. 이 스크립트는 Animator 컴포넌트가 필요합니다.");
            enabled = false; // 스크립트 비활성화
            return;
        }

        // Root Motion 비활성화 확인 (매우 중요!)
        // 애니메이션 자체의 움직임이 아닌, 스크립트가 직접 이동을 제어합니다.
        if (animator.applyRootMotion)
        {
            Debug.LogWarning("PlayerMovementController: Animator의 'Apply Root Motion'이 활성화되어 있습니다. 비활성화하는 것이 좋습니다.");
            animator.applyRootMotion = false; // 스크립트에서 강제로 비활성화
        }
    }

    void Update()
    {
        // 1. WASD 입력 값 가져오기
        // GetAxisRaw는 -1, 0, 1의 정수 값을 반환하여 즉각적인 움직임에 적합합니다.
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // A(-1) / D(1)
        float verticalInput = Input.GetAxisRaw("Vertical");     // S(-1) / W(1)

        // 2. 이동 방향 벡터 계산 (XZ 평면)
        // Y축은 0으로 고정하여 2D 평면(X-Z) 이동을 구현합니다.
        currentMoveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 3. 캐릭터 위치 이동
        // moveSpeed와 Time.deltaTime을 곱하여 프레임 속도에 독립적으로 이동
        // Space.World를 사용하여 월드 좌표계 기준으로 이동
        transform.Translate(currentMoveDirection * moveSpeed * Time.deltaTime, Space.World);

        // 4. 캐릭터 회전 (이동 방향을 바라보도록)
        // 캐릭터가 움직이는 입력이 있을 때만 회전합니다.
        // magnitude가 0.1보다 큰지 확인하여 작은 오차를 무시하고 실제 움직임이 있을 때만 회전
        if (currentMoveDirection.magnitude > 0.1f)
        {
            // 이동 방향을 바라보는 목표 회전값 계산
            Quaternion targetRotation = Quaternion.LookRotation(currentMoveDirection);

            // 현재 회전에서 목표 회전으로 부드럽게 Slerp (구형 선형 보간)
            // rotationSpeed * Time.deltaTime으로 회전 속도 제어
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 5. 애니메이터 파라미터 업데이트
        // 'Speed' 파라미터에 이동 방향 벡터의 길이(속도)를 전달합니다.
        // 움직이지 않을 때는 0, 움직일 때는 0.1 ~ 1 사이의 값이 됩니다.
        animator.SetFloat("Speed", currentMoveDirection.magnitude);
    }
}
