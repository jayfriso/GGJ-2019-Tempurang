using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DebugValueController : MonoBehaviour
{
    public Action<bool> onToggled;

    [SerializeField]
    private VerticalLayoutGroup _debugValueItemGroup;

    [SerializeField]
    private GameObject _debugValueItemRef;

    [SerializeField]
    private Button _closeButton;

    private void Start()
    {
        _closeButton.onClick.AddListener(() => { Toggle(false); });
    }

    public void Toggle(bool toggle) { gameObject.SetActive(toggle); onToggled.Invoke(toggle); }

    public void AddNewDebugValue(string title, float initValue, Action<float> onValueChanged)
    {
        DebugValueItem debugValueItem = Instantiate(_debugValueItemRef, _debugValueItemGroup.transform).GetComponent<DebugValueItem>();
        debugValueItem.onValueChanged += onValueChanged;
        debugValueItem.Init(title, initValue);
    }

}
