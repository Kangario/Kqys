using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TogglerPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _panel;
    [SerializeField] private Vector2 _startSize;
    [SerializeField] private Vector2 _finalySize;
    [SerializeField] private float _smoothTime;
    private bool _isOpen = false;
    private bool _isWork = false;
    private Coroutine _toggleCoroutine;

    private static bool _isClosingAllPanels = false;

    public void TogglePanel()
    {
        if (!_isWork)
        {
            if (!_isClosingAllPanels)
            {
                CloseAllPanel(this);
            }

            if (_toggleCoroutine != null)
            {
                StopCoroutine(_toggleCoroutine);
            }
            _isWork = true;
            _toggleCoroutine = StartCoroutine(SmoothToggle());
        }
    }

    private IEnumerator SmoothToggle()
    {
        Vector2 startSize = _isOpen ? _finalySize : _startSize;
        Vector2 targetSize = _isOpen ? _startSize : _finalySize;

        float elapsedTime = 0f;

        while (elapsedTime < _smoothTime)
        {
            _panel.sizeDelta = Vector2.Lerp(startSize, targetSize, elapsedTime / _smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isWork = false;
        _panel.sizeDelta = targetSize;

        _isOpen = !_isOpen;
    }

    public void CloseAllPanel(TogglerPanel currentPanel)
    {
        // Устанавливаем флаг для предотвращения рекурсии
        _isClosingAllPanels = true;

        List<TogglerPanel> panels = FindObjectsOfType<TogglerPanel>().ToList();
        foreach (var pan in panels)
        {
            if (currentPanel != pan && pan._isOpen && !pan._isWork)
            {
                pan.TogglePanel();
            }
        }

        // Сбрасываем флаг после завершения работы
        _isClosingAllPanels = false;
    }
}