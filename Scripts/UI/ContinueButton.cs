using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] _canvasGroup;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private float _duration = 0.3f;
    private int _minimumValueAlpha = 0;
    private int _maximumValueAlpha = 1;
    private int _sceneNumber = 0;

    private void Awake()
    {
        if (SaveProgress.LevelNumber == 0)
            SaveProgress.LevelNumber++;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        StartCoroutine(GoToNextStage());
    }

    private void MakeDarkScreen()
    {
        _image.DOFade(_maximumValueAlpha, _duration);

        for (int i = 0; i < _canvasGroup.Length; i++)
        {
            _canvasGroup[i].DOFade(_minimumValueAlpha, _duration);
        }
    }

    public IEnumerator GoToNextStage()
    {
        _sceneNumber++;
        SaveProgress.LevelNumber++;
        MakeDarkScreen();
        yield return new WaitForSeconds(_duration);

        SceneManager.LoadScene(_sceneNumber);
    }
}
