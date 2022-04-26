using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Material_Instancing : MonoBehaviour
{
    public GameObject go;
    public Color color;
    public Material material;
    public Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        go = this.gameObject;
        material = go.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureScale = offset;
    }
}
