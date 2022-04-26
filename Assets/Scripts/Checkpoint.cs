using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Checkpoint_Manager").GetComponent<Checkpoint_Manager>().LatestCheckPoint = this.transform.position;
        Destroy(this.gameObject);
    }
}
