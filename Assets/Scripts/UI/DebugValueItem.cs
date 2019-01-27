using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class DebugValueItem : MonoBehaviour
{
    public Action<float> onValueChanged;

    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private TMP_InputField _valueInput;

    public void Init(string title, float initValue)
    {
        _titleText.text = title;
        _valueInput.text = initValue.ToString();
        _valueInput.onValueChanged.AddListener(HandleValueChanged);
        gameObject.SetActive(true);
    }

    private void HandleValueChanged(string newValue) { onValueChanged.Invoke(float.Parse(newValue)); }
}
