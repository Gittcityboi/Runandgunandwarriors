using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    //���� ���� - �ӵ�, ü��
    [SerializeField] private float speed = 1f;
    [SerializeField] private float hp = 100f;
    [SerializeField] private Slider HpBar;

    private Rigidbody playerRigidbody;
    public bool isDead = false;

    void Start()
    {
        //Rigidbody ����
        playerRigidbody = GetComponent<Rigidbody>();
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
    }

    public void Damaged() //������ �Լ�(ü�¹� damage��ŭ ���� - damage ���� ���� ��ġ?)
    {
        
    }


    public void Dead() //��� �Լ�
    {
        gameObject.SetActive(false);
        isDead = true;
    }
    // ���� �ʿ� ���? - �̵� �ִϸ��̼�, �ѱ�� ��ȣ�ۿ�(�߻�, ������, ȹ��), ������ �޴� ���(ü�¹� ����)

}

