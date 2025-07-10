using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class Player : MonoBehaviour
{
    //���� ���� - �ӵ�, ü��
    [SerializeField] private float speed = 1f;
    public float hp = 100f;
    [SerializeField] private Slider HpBar;

    private Rigidbody playerRigidbody;
    public bool isDead = false;
    private Animator animator;
    int damage;

    void Start()
    {
        //Rigidbody ����
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    { 
        //�̵� ���
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        playerRigidbody.velocity = newVelocity;

        if (hp <= 0)
        {
            Dead();
        }
    }

    public void Damaged() //������ �Լ�(ü�¹� damage��ŭ ���� - damage ���� ���� ��ġ?)
    {
        animator.SetTrigger("Damaged");
        hp -= damage;
    }


    public void Dead() //��� �Լ�
    {
        gameObject.SetActive(false);
        isDead = true;
        animator.SetTrigger("Dead");
    }
    // ���� �ʿ� ���? - �̵� �ִϸ��̼�(Asset ã�Ƽ�), �ѱ�� ��ȣ�ۿ�(�߻�, ������, ȹ��), ������ �޴� ���(ü�¹� ����)

}

