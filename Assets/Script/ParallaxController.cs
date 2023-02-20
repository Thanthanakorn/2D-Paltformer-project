using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ParallaxController : MonoBehaviour
{
    private Transform _cam;
    private Vector3 _camStartPos;
    private float _distance;

    private GameObject[] _background;
    private Material[] _mat;
    private float[] _backSpeed;
    private float _farthestBack;

    [Range(0.01f, 100f)] public float parallaxSpeed;
    //private static readonly int MainTex = Shader.PropertyToID("MainTex");

    void Start()
    {
        _cam = Camera.main.transform;
        _camStartPos = _cam.position;

        int backCount = transform.childCount;
        _mat = new Material[backCount];
        _backSpeed = new float[backCount];
        _background = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            _background[i] = transform.GetChild(i).gameObject;
            _mat[i] = _background[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // find the farthest background
        {
            if ((_background[i].transform.position.z - _cam.position.z) > _farthestBack)
            {
                _farthestBack = _background[i].transform.position.z - _cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) //set the speed of the backgrounds
        {
            _backSpeed[i] = 1 - (_background[i].transform.position.z - _cam.position.z) / _farthestBack;
        }
    }

    private void LateUpdate()
    {
        _distance = _cam.position.x - _camStartPos.x;
        transform.position = new Vector3(_cam.position.x, transform.position.y, 0);
        for (int i = 0; i < _background.Length; i++)
        {
            float speed = _backSpeed[i] * parallaxSpeed;
            _mat[i].SetTextureOffset("_MainTex", new Vector2(_distance,0) * speed);
        }
    }
}
