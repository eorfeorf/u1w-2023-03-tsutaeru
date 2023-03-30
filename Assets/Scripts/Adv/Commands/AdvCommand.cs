using System;
using Adv.Commands;

[Serializable]
public abstract class AdvCommand : IAdvCommand
{
    protected AdvCommandType Type;
    protected int Uid;
    //protected AdvCommandData Data;

    public abstract void Execute();
}