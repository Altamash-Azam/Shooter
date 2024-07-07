using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorCube : Interactable
{
    MeshRenderer mesh;
    public Color[] colors;
    private int index;

    private void Start() {
        mesh = GetComponent<MeshRenderer>();
    }

    protected override void Interact()
    {
        index++;
        if(index == colors.Length){
            index = 0;
        }
        mesh.material.color = colors[index];
    }
}
