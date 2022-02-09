using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{    
    public bool isStop = false;

    public int Width = 5;

    public List<Block> list = new List<Block>();

    public Graphics Graphics = null;

    public float CurTime = 0f;
    public float ActionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ,len = Width; i <len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                Block b = new Block();
                list.Add(b);
            }
        }

        for (int g = 0, len = list.Count; g < len; g++)
        {
            int i = g / Width;
            int j = g % Width;

            list[g].NeiBlocks[0] = GetBlock(GetRoundNum(i - 1), GetRoundNum(j - 1));
            list[g].NeiBlocks[1] = GetBlock(GetRoundNum(i - 1), GetRoundNum(j));
            list[g].NeiBlocks[2] = GetBlock(GetRoundNum(i - 1), GetRoundNum(j + 1));
            list[g].NeiBlocks[3] = GetBlock(GetRoundNum(i), GetRoundNum(j - 1));
            list[g].NeiBlocks[4] = GetBlock(GetRoundNum(i), GetRoundNum(j + 1));
            list[g].NeiBlocks[5] = GetBlock(GetRoundNum(i + 1), GetRoundNum(j - 1));
            list[g].NeiBlocks[6] = GetBlock(GetRoundNum(i + 1), GetRoundNum(j));
            list[g].NeiBlocks[7] = GetBlock(GetRoundNum(i + 1), GetRoundNum(j + 1));
        }
        Graphics.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            return;
        }

        if (CurTime > 0)
        {
            CurTime -= Time.deltaTime;
            return;
        }
        CurTime = ActionTime;

        foreach (Block b in list)
        {
            b.LifeCheck();
        }

        foreach (Block b in list)
        {
            b.SetState();
        }
    }

    int GetRoundNum(int index)
    {
        if(index < 0)
        {
            index += Width;
        }
        if(index > Width - 1)
        {

            index -= Width;
        }
        return index;
    }

    public void Stop()
    {
        isStop = !isStop;
    }

    public void Clear()
    {
        foreach (var b in list)
        {
            b.Alive = false;
        }
    }

    public Block GetBlock(int x, int y)
    {
        return list[x * Width + y];
    }
}
