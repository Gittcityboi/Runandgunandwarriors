using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    public Slider targetSlider; // 유니티 에디터에서 여기에 슬라이더 UI 오브젝트를 드래그하여 할당합니다.
    public Text valueText;     // 유니티 에디터에서 여기에 값을 표시할 Text UI 오브젝트를 드래그하여 할당합니다.

    // Start is called before the first frame update
    void Start()
    {
        // 초기 값을 텍스트에 표시
        UpdateSliderValueText(targetSlider.value);

        // 슬라이더 값이 변경될 때마다 UpdateSliderValueText 함수를 호출하도록 리스너 추가
        // AddListener는 'float' 타입의 매개변수를 받는 함수를 연결합니다.
        targetSlider.onValueChanged.AddListener(UpdateSliderValueText);
    }

    void UpdateSliderValueText(float newValue)
    {
        if (valueText != null)
        {
            // 슬라이더의 값을 텍스트에 표시
            // 필요하다면 정수형으로 변환하여 표시할 수 있습니다.
            valueText.text = newValue.ToString(); // 실수로 표시
            //valueText.text = Mathf.RoundToInt(newValue).ToString(); // 정수로 반올림하여 표시
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
