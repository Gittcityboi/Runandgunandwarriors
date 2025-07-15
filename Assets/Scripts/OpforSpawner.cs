using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpforSpawner : MonoBehaviour
{
    [Header("스폰 설정")]
    public GameObject opforPrefab; // 생성할 Opfor 프리팹 (에디터에서 할당)
    public float spawnInterval = 3f; // Opfor 생성 간격 (초)

    [Header("스폰 범위 설정 (Player 기준)")]
    public Transform playerTransform; // Player 오브젝트의 Transform (에디터에서 할당하거나 "Player" 태그 찾기)
    public float minSpawnDistance = 15f; // 플레이어로부터 최소 스폰 거리 (이 값보다 가까이 스폰되지 않음)
    public float maxSpawnDistance = 30f; // 플레이어로부터 최대 스폰 거리 (사용자 요청: 30)

    [Header("카메라 설정")]
    public Camera mainCamera; // 주 카메라 (할당하지 않으면 자동으로 MainCamera 태그를 찾음)

    private float timer; // 스폰 간격 타이머

    // Start is called before the first frame update
    void Start()
    {
        // 메인 카메라가 할당되지 않았다면 "MainCamera" 태그를 가진 카메라를 찾습니다.
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("메인 카메라를 찾을 수 없습니다. 'MainCamera' 태그가 지정되었는지 확인하거나 Inspector에서 직접 할당해주세요.");
                enabled = false; // 스크립트 비활성화
                return;
            }
        }

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
                Debug.LogError("Player 오브젝트를 찾을 수 없습니다. 'Player' 태그가 지정되었는지 확인하거나 Inspector에서 직접 할당해주세요.");
                enabled = false;
                return;
            }
        }

        timer = spawnInterval; // 첫 스폰을 위해 타이머 초기화
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnOpfor();
            timer = spawnInterval; // 다음 스폰을 위해 타이머 재설정
        }
    }

    void SpawnOpfor()
    {

        if (opforPrefab == null)
        {
            Debug.LogError("Opfor 프리팹이 할당되지 않았습니다!");
            return;
        }
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform이 할당되지 않았습니다! Opfor를 스폰할 수 없습니다.");
            return;
        }

        Vector3 spawnWorldPos = Vector3.zero;
        bool foundValidSpawnPoint = false;
        int maxAttempts = 50; // 유효한 스폰 지점을 찾기 위한 최대 시도 횟수 (무한 루프 방지)

        for (int i = 0; i < maxAttempts; i++)
        {
            // 1. 플레이어 주변에 랜덤한 방향과 거리로 스폰 위치 후보 계산
            // Random.insideUnitCircle은 반지름 1인 원 내부의 랜덤한 점을 반환 (Vector2)
            // .normalized를 통해 단위 벡터(방향)를 얻고, Random.Range로 거리를 조절
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

            // XZ 평면에서의 위치 계산 (플레이어 Y축은 유지)
            Vector3 potentialSpawnPos = playerTransform.position + new Vector3(randomDirection.x * randomDistance, 0f, randomDirection.y * randomDistance);

            // Y축은 플레이어의 Y 위치를 따르도록 설정 (또는 고정된 지면 높이로 설정할 수 있음)
            potentialSpawnPos.y = playerTransform.position.y;

            // 2. 이 위치가 카메라 뷰포트 밖에 있는지 확인
            // WorldToViewportPoint는 월드 좌표를 뷰포트 좌표 (0~1)로 변환합니다.
            // Z값은 카메라로부터의 거리를 나타냅니다.
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(potentialSpawnPos);

            // 뷰포트 좌표가 0~1 범위를 벗어나면 화면 밖에 있는 것
            // viewportPoint.z < 0 은 카메라 뒤에 있는 경우를 의미하므로 제외해야 합니다.
            if (viewportPoint.x < 0 || viewportPoint.x > 1 ||
                viewportPoint.y < 0 || viewportPoint.y > 1 ||
                viewportPoint.z < 0) // Z < 0 이면 카메라 뒤에 있음
            {
                spawnWorldPos = potentialSpawnPos;
                foundValidSpawnPoint = true;
                break; // 유효한 스폰 지점을 찾았으므로 루프 종료
            }
        }

        if (foundValidSpawnPoint)
        {
            // 유효한 위치에 Opfor를 생성합니다.
            Instantiate(opforPrefab, spawnWorldPos, Quaternion.identity); // 회전은 기본값으로
            Debug.Log($"Opfor가 월드 좌표 {spawnWorldPos}에 생성되었습니다. (플레이어로부터 {Vector3.Distance(playerTransform.position, spawnWorldPos):F2}m)");
        }
        else
        {
            Debug.LogWarning("유효한 Opfor 스폰 지점을 찾을 수 없습니다. 스폰 거리나 환경 설정을 확인해주세요.");
        }
    }
 }
