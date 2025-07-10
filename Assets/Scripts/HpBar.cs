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
        //들어갈 기능 - 체력 수치 표기
    }

    // Update is called once per frame
    void Update()
    {
        curHP = player.hp;
        hpBar.value = curHP / maxHP;
    }
    
}
