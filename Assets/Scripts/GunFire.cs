using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [Header("총알 설정")]
    public GameObject bulletPrefab; // 발사할 총알 프리팹 (에디터에서 할당)
    public Transform muzzlePoint;   // 총알이 발사될 총구 위치 (에디터에서 할당)
    public float fireRate = 0.5f;   // 발사 속도 (초당 발사 횟수, 숫자가 낮을수록 빠르게 발사)
    public float raycastDistance = 100f; // 마우스 레이캐스트 최대 거리

    private float nextFireTime; // 다음 발사가 가능한 시간

    [Header("카메라 설정")]
    public Camera playerCamera; // 플레이어의 메인 카메라 (할당하지 않으면 자동으로 MainCamera 태그를 찾음)

    void Start()
    {
        // 카메라가 할당되지 않았다면 "MainCamera" 태그를 가진 카메라를 찾습니다.
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            if (playerCamera == null)
            {
                Debug.LogError("GunController: 메인 카메라를 찾을 수 없습니다. 'MainCamera' 태그가 지정되었는지 확인하거나 Inspector에서 직접 할당해주세요.");
                enabled = false;
                return;
            }
        }

        // 총구 위치가 할당되었는지 확인
        if (muzzlePoint == null)
        {
            Debug.LogError("GunController: Muzzle Point(총구 위치)가 할당되지 않았습니다! 총알이 총에서 발사되지 않을 수 있습니다.");
            muzzlePoint = this.transform;
        }

        nextFireTime = 0f; // 게임 시작 시 바로 발사 가능하도록 초기화
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시 발사
        if (Input.GetMouseButton(0))
        {
            // 발사 쿨타임 확인
            if (Time.time >= nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + fireRate; // 다음 발사 가능 시간 업데이트
            }
        }
    }

    void FireBullet()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("총알 프리팹이 할당되지 않았습니다!");
            return;
        }

        // 1. 마우스 위치에서 Ray 생성
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetWorldPos;

        // 2. Raycast를 사용하여 마우스가 가리키는 월드 지점 찾기
        // Y축 고정을 위해, 레이가 'Y=muzzlePoint.position.y' 평면과 만나는 지점을 찾습니다.
        // 이를 위해 가상의 평면을 생성하고 그 평면과의 교차점을 계산합니다.

        Plane groundPlane = new Plane(Vector3.up, muzzlePoint.position); // Y축 고정을 위한 평면 (법선 벡터: 위, 평면의 기준점: 총구 Y)
        float distance;

        if (groundPlane.Raycast(ray, out distance))
        {
            // 레이가 평면과 만나는 지점을 목표로 설정
            targetWorldPos = ray.GetPoint(distance);
        }
        else
        {
            // 레이가 평면과 만나지 않으면 (예: 카메라가 평면과 평행할 때),
            // 카메라로부터 raycastDistance 만큼 떨어진 지점을 목표로 설정하되 Y축을 총구 Y로 고정
            targetWorldPos = ray.GetPoint(raycastDistance);
            targetWorldPos.y = muzzlePoint.position.y; // Y축 강제 고정
        }

        // 3. 총구 위치에서 목표 지점까지의 방향 벡터 계산
        Vector3 fireDirection = (targetWorldPos - muzzlePoint.position).normalized;

        // (선택 사항) 총알의 Y축 방향 벡터가 0이 되도록 강제.
        // 이렇게 하면 총알이 XZ 평면에서만 움직입니다.
        fireDirection.y = 0f;
        fireDirection.Normalize(); // 다시 정규화하여 방향 벡터의 길이를 1로 만듭니다.

        // 4. 총알 생성 및 발사
        GameObject newBulletGO = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.identity);

        Bullets bulletScript = newBulletGO.GetComponent<Bullets>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(fireDirection);
        }
        else
        {
            Debug.LogWarning("생성된 총알 프리팹에 Bullet 스크립트가 없습니다!");
        }

        // (디버그용) 발사 방향 시각화
        // Debug.DrawRay(muzzlePoint.position, fireDirection * 100, Color.red, 1f);
    }
}
