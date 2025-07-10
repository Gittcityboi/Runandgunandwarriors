using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Guns : MonoBehaviour
{
    private Bullets bullet;
    private int magazine;
    public int max_ammo;
    public int cur_ammo;
    public float damage;
    private float fire_rate = 0f;


    public Guns smg;
    public Guns ar;
    public Guns sr;
    public Guns sg;

    bool isSg = false;
    bool wGun = false;

    // Start is called before the first frame update
    void Start()
    {
        /*Gun의 경우, 3가지 총기를 구현해야 함 - 
           1. 낮은 장탄 수, 느린 연사속도, 높은 데미지 (SR)
           2. 많은 장탄 수, 빠른 연사속도, 낮은 데미지 (SMG)
           3. 중간 쯤하는 총기 (AR)
           4. 샷건(가능 시)
           또한 기능적으로 Bullet을 마우스 방향으로 발사, UI와 상호작용 해 현재 장착 총기 및 상태 표현
           플레이어 따라다니기 구현해야 함
         */
        smg.max_ammo = 45;
        ar.max_ammo = 30;
        sr.max_ammo = 10;
        sg.max_ammo = 7;

        smg.fire_rate = 0.1f;
        ar.fire_rate = 0.3f;
        sr.fire_rate = 1f;
        sg.fire_rate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        cur_ammo -= 1;
        //bullet 발사, 장탄 1 감소, fire_rate에 영향받아 연사 속도 조절
    }

    public void Smg()
    {
        cur_ammo = smg.max_ammo;
        fire_rate = smg.fire_rate;
        isSg = false;
        wGun = true;
    }
    public void Ar()
    {
        cur_ammo = ar.max_ammo;
        fire_rate = ar.fire_rate;
        isSg = false;
        wGun = true;
    }
    public void Sr()
    {
        cur_ammo = sr.max_ammo;
        fire_rate = sr.fire_rate;
        isSg = false;
        wGun = true;
    }
    public void Sg()
    {
        cur_ammo = sg.max_ammo;
        fire_rate = sg.fire_rate;
        isSg = true;
        wGun = true;
    }

    public void Reload()
    {
        /*if문 사용 - 
         sg 아니면 3초 정도 걸리고 총알 최대로
         sg면 1초 마다 1발씩 장전
         */
        if (isSg)
        {
            cur_ammo += 1;
        }
        else if (wGun)
        {
            cur_ammo = max_ammo;
        }
        else
        {
            //nothing happens....
        }
    }
}
