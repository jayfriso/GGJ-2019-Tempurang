using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private LandingCanvasController _landingCanvasController;

    [SerializeField]
    private DebugValueController _debugValueController;
    public DebugValueController debugValueController { get { return _debugValueController; } }

    [SerializeField]
    private Button _debugValueButton;

    [SerializeField]
    private TextMeshProUGUI _pointText;
    [SerializeField]
    private TextMeshProUGUI _timeText;

    private int currentTime;

    public void Init(GameController gameController)
    {
        gameController.onPointUpdate += UpdatePointText;
        gameController.onTimeUpdate += HandleTimeUpdate;
        _landingCanvasController.Init();
    }

    private void Start()
    {
        _debugValueButton.onClick.AddListener(() => { _debugValueController.Toggle(true); });
    }

    private void HandleTimeUpdate(float time)
    {
        int newTime = Mathf.CeilToInt(time);
        if (newTime != currentTime)
        {
            currentTime = newTime;
            _timeText.text = currentTime.ToString();
        }  
    }

    public void UpdatePointText(int point) { _pointText.text = point.ToString(); }

    // Update is called once per frame
    void Update()
    {
        
    }
}
