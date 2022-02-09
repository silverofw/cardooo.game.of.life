using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockCube : MonoBehaviour
{
    public bool alive = false;

    public Material black = null;
    public Material white = null;
    public MeshRenderer meshRenderer = null;

    public Block TargetBlock = null;

    public void SetLife()
    {
        if (TargetBlock.Alive == alive)
            return;

        alive = TargetBlock.Alive;
        //meshRenderer.material = !alive ? black : white;
        meshRenderer.enabled = alive;
    }
}
