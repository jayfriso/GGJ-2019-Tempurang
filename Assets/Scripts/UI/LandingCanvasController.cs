using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LandingCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController;

    [SerializeField]
    private Button _playButton;

    public void Toggle(bool toggle) { gameObject.SetActive(toggle); }

    // Use this for initialization
    public void Init()
    {
        Toggle(true);
        _playButton.onClick.AddListener(HandlePlayGame);
    }

    private void HandlePlayGame()
    {
        _gameController.StartGame();
        Toggle(false);
    }
}
