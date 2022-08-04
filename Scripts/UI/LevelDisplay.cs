using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private ContinueButton _continueButton;
    [SerializeField] private FinishScreen _finishPanel;
    [SerializeField] private TMP_Text _text;

    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _finishPanel.FinishPanelOpen += OnFinishPanelOpen;
    }

    private void OnDisable()
    {
        _finishPanel.FinishPanelOpen -= OnFinishPanelOpen;
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _text.text = _text.text + SaveProgress.LevelNumber;
    }

    private void OnFinishPanelOpen()
    {
        _canvasGroup.alpha = 0;
    }
}
