using UnityEngine;

public class MusicalScoreLoader
{
    public MusicalScoreEntity Load(MusicalScore musicalScore)
    {
        var entity = new MusicalScoreEntity();

        // ノーツやコントロールポイント.
        var jsonConverter = new JsonConverter();
        float[][] instance = jsonConverter.FromJsonToInstance<float[][]>(musicalScore.ElementsFromJson);
        float startTime = GameDefine.START_ADD_TIME_SEC;
        for (int i = 0; i < instance.Length; ++i)
        {
            var args = instance[i];
            switch ((int) args[0])
            {
                case (int) NoteType.Tap:
                    var note = new Note
                    {
                        Type = NoteType.Tap,
                        Time = args[1] + startTime,
                        IsActive = true,
                        Uid = i,
                    };
                    entity.Notes.Add(note);
                    break;
                case (int) ControlPointType.Bpm:
                    var controlPoint = new ControlPoint
                    {
                        Type = ControlPointType.Bpm,
                        Time = args[1] + startTime,
                        Uid = i,
                    };
                    entity.ControlPoints.Add(controlPoint);
                    break;
                default:
                    Debug.LogError("[MusicalScoreLoader] Invalid type.");
                    break;
            }
        }

        return entity;
    }
}