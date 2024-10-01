using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;

    [SerializeField] private Sprite _defaultSprite;


    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void CloseCurrentPanel()
    {
        image.sprite = _defaultSprite;
        text.text = "";
        gameObject.SetActive(false);
    }


}
