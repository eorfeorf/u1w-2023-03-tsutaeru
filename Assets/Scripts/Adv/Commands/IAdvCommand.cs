using Adv.Commands;

public interface IAdvCommand
{
    protected AdvCommandType Type { get; }
    protected int Uid { get; }
    
    public void Execute();
}