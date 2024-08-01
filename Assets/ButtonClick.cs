using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;

public class ButtonClick : MonoBehaviour
{
    private MediaPlayer _mediaPlayer = null;
    private Button _playPauseButton = null;
    private Text _videoTitleText = null;
    private bool _isPlaying = false;
    private Text textToEdit;
    void Start()
    {
        GameObject _playerObject = GameObject.Find("MediaPlayer");
        GameObject _playPauseButtonObject = GameObject.Find("PlayPauseButton");
        GameObject _videoTitleTextObject = GameObject.Find("VideoTitle");
        _mediaPlayer = _playerObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>();
        _playPauseButton = _playPauseButtonObject.GetComponent<Button>();
        _videoTitleText = _videoTitleTextObject.GetComponent<Text>();
        _videoTitleText.text = new DirectoryInfo(Path.GetDirectoryName(_mediaPlayer.MediaPath.Path)).Name;
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

    public void ChangeVideo(string path)
    {
        var newMediaPath = new MediaPath(path, MediaPathType.AbsolutePathOrURL);
        if (newMediaPath == _mediaPlayer.MediaPath){
            return;
        }
        _mediaPlayer.OpenMedia(newMediaPath, autoPlay:false);
        _isPlaying = false;
        _playPauseButton.image.sprite = Resources.Load<Sprite>("UI/Play");
        _videoTitleText.text = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
    }

    public void VideoFinished()
    {
        _isPlaying = false;
        _playPauseButton.image.sprite = Resources.Load<Sprite>("UI/Play");
    }
}
