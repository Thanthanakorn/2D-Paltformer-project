using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palrallax : MonoBehaviour
{
    private Material _mat;
    private float _distance;

    [Range(0f, 0.5f)] public float speed = 0.2f;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        _distance += Time.deltaTime * speed;
        _mat.SetTextureOffset(MainTex,Vector2.right*_distance);
    }
}
