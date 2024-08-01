using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;

public class ButtonClick : MonoBehaviour
{
    private MediaPlayer _mediaPlayer = null;
    private Button _playPauseButton = null;
    private bool _isPlaying = false;
    private Text textToEdit;
    void Start()
    {
        GameObject _playerObject = GameObject.Find("MediaPlayer");
        GameObject _playPauseButtonObject = GameObject.Find("PlayPauseButton");
        _mediaPlayer = _playerObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>();
        _playPauseButton = _playPauseButtonObject.GetComponent<Button>();
    }

    public void PlayPause()
    {
        if (!_isPlaying){
            _mediaPlayer.Play();
            _isPlaying = true;
            _playPauseButton.image.sprite = Resources.Load<Sprite>("UI/Pause");
        }
        else{
            _mediaPlayer.Pause();
            _isPlaying = false;
            _playPauseButton.image.sprite = Resources.Load<Sprite>("UI/Play");
        }
    }

    public void ChangeVideo(string Path)
    {
        var newMediaPath = new MediaPath(Path, MediaPathType.AbsolutePathOrURL);
        if (newMediaPath == _mediaPlayer.MediaPath){
            return;
        }
        _mediaPlayer.OpenMedia(newMediaPath, autoPlay:false);
        _isPlaying = false;
        _playPauseButton.image.sprite = Resources.Load<Sprite>("UI/Play");
    }
}
