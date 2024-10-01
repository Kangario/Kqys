using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.eulerAngles += new Vector3(0,_speed * Time.deltaTime,0);
    }
}
