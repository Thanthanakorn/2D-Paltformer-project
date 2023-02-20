using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollSpeed = 0.5f;
    private float _offset;
    private Material _mat;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }


    private void Update()
    {
        _offset += (Time.deltaTime * scrollSpeed) / 10f;
        _mat.SetTextureOffset(MainTex, new Vector2(_offset, 0));
    }
}
