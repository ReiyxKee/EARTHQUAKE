using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable_Floor : MonoBehaviour
{
    public bool Destroyable = false;
    public bool PlayerOnPlatform = false;
    public bool Destroyed = false;
    public float time;
    public float durabilitytime;

    public Material[] Default_Texture;
    public Material[] Cracked_Texture;
    public Material[] Destroyed_Texture;

    public MeshRenderer PlatformMesh;
    public ParticleSystem Cracks;

    private void Update()
    {
        if(Destroyable)
        {            
            if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical")!= 0) && PlayerOnPlatform)
            {
                time -= Time.deltaTime;

                if(time < durabilitytime * 0.45f)
                {
                    PlatformMesh.materials = Cracked_Texture;
                    Cracks.Play();
                }

                if(time <= 0)
                {
                    if (!Destroyed)
                    {
                        Destroyed = true;
                        DestroyPlatform();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroyable = true;
            if (time == 0)
            {
                time = Random.Range(0.25f, 0.75f);
                durabilitytime = time;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        PlayerOnPlatform = other.tag == "Player" ? true : false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroyable = false;
            PlayerOnPlatform = false;
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

            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[6];
                Vector3[] newNormals = new Vector3[6];
                Vector2[] newUvs = new Vector2[6];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 3, 1, 1, 3, 0, 0, 3, 2,  2, 1, 0 };


                GameObject GO = new GameObject("Triangle " + (i / 3));
                GO.layer = LayerMask.NameToLayer("Particle");
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.tag = "Untagged";
                GO.AddComponent<MeshRenderer>().materials = Destroyed_Texture;
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + 1.5f, transform.position.z + Random.Range(-0.5f, 0.5f));
                GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(300, 500), explosionPos, 4);
                Destroy(GO, 5 + Random.Range(0.0f, 5.0f));
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
