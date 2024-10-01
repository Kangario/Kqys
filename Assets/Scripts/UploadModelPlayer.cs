using Dummiesman;
using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UploadModelPlayer : MonoBehaviour
{
    [SerializeField] Transform _container;
    public GameObject LoadedObject;
    public void UploadModel()
    {
        var extensions = new[] {
        new ExtensionFilter("Image Files", "obj") };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        if (paths.Length > 0)
        {
            string modelPath = paths[0];
            if (!File.Exists(modelPath))
            {
                Debug.Log("File doesn't exist.");
            }
            else
            {
                if (LoadedObject != null)
                    Destroy(LoadedObject);
                LoadedObject = new OBJLoader().Load(modelPath);
                LoadedObject.transform.SetParent(_container, true);
                LoadedObject.transform.localPosition = new Vector3(0,0,-0.2f);
            }
        }
    }

}
