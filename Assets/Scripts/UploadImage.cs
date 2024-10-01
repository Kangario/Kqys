using SFB;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadImage : MonoBehaviour
{
    [SerializeField] private Image _previewImage;

    public void UploadImageToPreview()
    {
        var extensions = new[] {
        new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ) };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            string path = paths[0];

            // Чтение файла в байтовый массив
            byte[] imageData = File.ReadAllBytes(path);

            // Создание новой текстуры
            Texture2D texture = new Texture2D(2, 2);

            // Загрузка текстуры из массива байт
            if (texture.LoadImage(imageData))
            {
                // Преобразование текстуры в спрайт
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                // Назначение спрайта на UI Image
                _previewImage.sprite = sprite;
            }
        }
    }
}
