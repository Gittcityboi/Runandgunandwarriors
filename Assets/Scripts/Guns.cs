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
        /*Gun�� ���, 3���� �ѱ⸦ �����ؾ� �� - 
           1. ���� ��ź ��, ���� ����ӵ�, ���� ������ (SR)
           2. ���� ��ź ��, ���� ����ӵ�, ���� ������ (SMG)
           3. �߰� ���ϴ� �ѱ� (AR)
           4. ����(���� ��)
           ���� ��������� Bullet�� ���콺 �������� �߻�, UI�� ��ȣ�ۿ� �� ���� ���� �ѱ� �� ���� ǥ��
           �÷��̾� ����ٴϱ� �����ؾ� ��
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
        //bullet �߻�, ��ź 1 ����, fire_rate�� ����޾� ���� �ӵ� ����
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
        /*if�� ��� - 
         sg �ƴϸ� 3�� ���� �ɸ��� �Ѿ� �ִ��
         sg�� 1�� ���� 1�߾� ����
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
