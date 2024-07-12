using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // Controls World (Cubes and other environments)
    // Contains methods that affects World envrionments by duration of time - subscribe to events in StageManager

    [SerializeField] SphereCollider worldCollider;
    [SerializeField] float worldSize;

    private void Awake()
    {
        worldCollider.radius = worldSize;
    }

    public void SetWorld()
    {
        worldCollider.radius = worldSize;
    }

    // must add parameter in order to adjust shrinking amount
    public void ShrinkWorld()
    {
        worldCollider.GetComponent<SphereCollider>().radius -= 5;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
