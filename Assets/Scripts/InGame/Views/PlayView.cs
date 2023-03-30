using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayView : MonoBehaviour
{
    [SerializeField]
    private Transform _noteParent;

    [SerializeField]
    private NoteView _noteViewPrefab;

    [SerializeField]
    private Transform _lineView;

    [SerializeField]
    private RankView _rankView;

    private Dictionary<int, NoteView> _noteViews = new ();

    private void Start()
    {
        _noteParent.position = _lineView.position;
    }

    /// <summary>
    /// ノーツビュー生成.
    /// </summary>
    /// <param name="notes"></param>
    public void CreateNoteViews(IList<Note> notes)
    {
        foreach (var note in notes)
        {
            var view = Instantiate(_noteViewPrefab, _noteParent);
            view.Initialize(note.Time);
            _noteViews.Add(note.Uid, view);            
        }
    }

    /// <summary>
    /// ノーツビュー削除.
    /// </summary>
    public void DestroyNoteViews()
    {
        foreach (var view in _noteViews)
        {
            Destroy(view.Value.gameObject);
        }
        _noteViews.Clear();
    }

    /// <summary>
    /// ノーツを更新。主に座標.
    /// </summary>
    /// <param name="progressTime"></param>
    public void UpdateNoteViews(float progressTime)
    {
        foreach (var view in _noteViews)
        {
            view.Value.UpdatePosition(progressTime);
        }
    }

    /// <summary>
    /// ノーツビューをリセット.
    /// </summary>
    public void ResetNoteViews()
    {
        foreach (var view in _noteViews)
        {
            view.Value.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ノーツビューのアクティブを設定.
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="isActive"></param>
    public void SetNoteActive(int uid, bool isActive) => _noteViews[uid].gameObject.SetActive(isActive);

    /// <summary>
    /// ランク文字を反映.
    /// </summary>
    /// <param name="rank"></param>
    public void ApplyRank(GameDefine.Rank rank) => _rankView.SetRank(rank);
}
