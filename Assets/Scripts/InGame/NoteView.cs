using UnityEngine;

public class NoteView : MonoBehaviour
{
    private float _createdAt; 
    
    public void Initialize(float time)
    {
        _createdAt = time;
        transform.localPosition = new Vector3(time, 0f, transform.localPosition.z); 
    }

    public void UpdatePosition(float progressTime)
    {
        var pos = transform.localPosition;
        pos.x = _createdAt - progressTime;
        transform.localPosition = pos * 3f;
    }
}
