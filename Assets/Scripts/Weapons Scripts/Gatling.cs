using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Gatling : MonoBehaviour
{
    [FormerlySerializedAs("HeadGun")] [SerializeField] private GameObject headGun;

    private float _rotationSpeed = 100f, _maxSpeed= 10000f;
  

    // Update is called once per frame
    void Update()
    {
        this.HeadGunRotate();
    }

    private void HeadGunRotate()
    {
        if(_rotationSpeed <= _maxSpeed) _rotationSpeed += 10;
        headGun.transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);

    }
}
