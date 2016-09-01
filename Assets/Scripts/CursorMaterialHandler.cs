using UnityEngine;
using System.Collections;

public class CursorMaterialHandler : MonoBehaviour
{
    public Material[] cursorMaterials=new Material[5];
    public Renderer rend;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    public void SetMaterial(int pose)
    {
        rend.material = cursorMaterials[pose];
    }
}
