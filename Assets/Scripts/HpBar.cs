using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private float maxHP;
    private float curHP;
    [SerializeField] private Player player;
    [SerializeField] private Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        maxHP = player.hp;
        //�� ��� - ü�� ��ġ ǥ��
    }

    // Update is called once per frame
    void Update()
    {
        curHP = player.hp;
        hpBar.value = curHP / maxHP;
    }
    
}
