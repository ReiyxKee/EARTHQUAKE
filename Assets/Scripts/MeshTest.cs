using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MeshTest : MonoBehaviour
{
    public bool reset = false;
    public int[] triangles;
    public int vertcount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            Debug.Log("Generated");
                Mesh mesh = new Mesh();
                mesh.vertices = new Vector3[vertcount];
                mesh.normals = new Vector3[vertcount];
                mesh.uv = new Vector2[vertcount];
                mesh.triangles = triangles;


                this.GetComponent<MeshFilter>().mesh = mesh;

            
                reset = false;
            }
        }
    }