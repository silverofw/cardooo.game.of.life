using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public Block[] NeiBlocks = new Block[8];
    public bool Alive = false;
    public bool WillDead = false;
    public bool WillAlive = false;

    List<Rule> rules = new List<Rule>();

    public Block()
    {
        rules.Add(new LonelyRule());
    }

    public void LifeCheck()
    {
        foreach (Rule r in rules)
        {
            r.Excute(this);
        }
    }

    public void SetState()
    {
        if (WillDead)
            Alive = false;

        if(WillAlive)
            Alive = true;

        WillDead = false;
        WillAlive = false;
    }
}

public class Rule
{
    public virtual void Excute(Block block)
    {
            
    }
}

public class LonelyRule : Rule
{
    public override void Excute(Block block)
    {
        base.Excute(block);

        int aliveNeiNum = 0;
        foreach (Block b in block.NeiBlocks)
        {
            if (b.Alive)
                aliveNeiNum++;
        }

        if (block.Alive)
        {
            if (aliveNeiNum < 2)
                block.WillDead = true;


            if (aliveNeiNum > 3)
                block.WillDead = true;
        }
        else
        {
            if (aliveNeiNum == 3)
                block.WillAlive = true;
        }
    }
}



