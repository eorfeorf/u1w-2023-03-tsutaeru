/// <summary>
/// 制御データ.
/// </summary>
public class ControlPoint
{
    public ControlPointType Type;
    public float Time;
    public int Uid;

    public ControlPoint()
    {
        
    }
    
    public ControlPoint(ControlPoint controlPoint)
    {
        Type = controlPoint.Type;
        Time = controlPoint.Time;
        Uid = controlPoint.Uid;
    }
}