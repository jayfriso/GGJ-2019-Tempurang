using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private DebugValueController _debugValueController;

    [SerializeField]
    private Button _debugValueButton;

    private void Start()
    {
        _debugValueButton.onClick.AddListener(() => { _debugValueController.Toggle(true); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
