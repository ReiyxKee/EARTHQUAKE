using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable_Wall : MonoBehaviour
{
    public bool Destroyable = false;
    public bool Destroyed = false;

    public Material[] Default_Texture;
    public Material[] Cracked_Texture;
    public Material[] Destroyed_Texture;

    public MeshRenderer PlatformMesh;

    public int DebrisAmt;
    public bool DebrisRemainAfterDestroyed = false;
    public bool ForceTriggerDestroy = false;

    public Mesh[] DebrisMesh;
    private void Update()
    {
        if (ForceTriggerDestroy)
        {
            if (!Destroyed)
            {
                Destroyed = true;
                DestroyPlatform();
            }
        }
    }


    public void DestroyPlatform()
    {
        StartCoroutine(SplitMesh(true));
    }

    public IEnumerator SplitMesh(bool destroy)
    {
        if (GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null)
        {
            yield return null;
        }

        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }

        Mesh M = new Mesh();
        if (GetComponent<MeshFilter>())
        {
            M = GetComponent<MeshFilter>().mesh;
        }
        else if (GetComponent<SkinnedMeshRenderer>())
        {
            M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }


        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;
        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {

            int[] indices = M.GetTriangles(submesh);

            for (int i = 0; i < DebrisAmt; i ++)
            {
                //Vector3[] newVerts = new Vector3[6];
                //Vector3[] newNormals = new Vector3[6];
                //Vector2[] newUvs = new Vector2[6];
                //for (int n = 0; n < 3; n++)
                //{
                //    int index = indices[i + n];
                //    newVerts[n] = verts[index];
                //    newUvs[n] = uvs[index];
                //    newNormals[n] = normals[index];
                //}

                Mesh mesh = DebrisMesh[Random.Range(0,DebrisMesh.Length-1)];
                //mesh.vertices = newVerts;
                //mesh.normals = newNormals;
                //mesh.uv = newUvs;

                //mesh.triangles = new int[] { 0, 1, 2, 2, 3, 1, 1, 3, 0, 1,1,1, 1,3,0};


                GameObject GO = new GameObject("Triangle " + (i / 3));
                GO.transform.localScale = new Vector3(Random.Range(0.5f,2.0f), Random.Range(0.5f, 2.0f), Random.Range(0.5f, 2.0f));
                GO.transform.position = new Vector3(Random.Range(transform.position.x - this.GetComponent<BoxCollider>().size.x, transform.position.x + this.GetComponent<BoxCollider>().size.x), Random.Range(transform.position.y - this.GetComponent<BoxCollider>().size.y, transform.position.y + this.GetComponent<BoxCollider>().size.y), Random.Range(transform.position.z -  this.GetComponent<BoxCollider>().size.z, transform.position.z +this.GetComponent<BoxCollider>().size.z));
                GO.transform.rotation = transform.rotation;
                GO.tag = "Untagged";
                GO.AddComponent<MeshRenderer>().materials = Destroyed_Texture;
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                if (DebrisRemainAfterDestroyed)
                {                    
                    GO.tag = "Debris";
                    GO.AddComponent<Damaging_Debris>();
                }

                Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + 1.5f, transform.position.z + Random.Range(-0.5f, 0.5f));
                GO.AddComponent<Rigidbody>().AddExplosionForce(0, explosionPos, 2);
                GO.GetComponent<Rigidbody>().mass = 5.0f;
                if (!DebrisRemainAfterDestroyed)
                {
                    Destroy(GO, 5 + Random.Range(0.0f, 1.5f));
                }
            }
        }

        GetComponent<Renderer>().enabled = false;

        yield return new WaitForSeconds(0.5f);
        if (destroy == true)
        {
            Destroy(gameObject);
        }


    }
}
