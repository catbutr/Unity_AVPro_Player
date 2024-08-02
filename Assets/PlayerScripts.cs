using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;

public class PlayerScripts : MonoBehaviour
{
    /// <summary>
    /// Проигрыватель
    /// <summary>
    private MediaPlayer _mediaPlayer = null;

    /// <summary>
    /// Кнопка проигрывателя
    /// <summary>
    private Button _playPauseButton = null;

    /// <summary>
    /// Подпись к проигрываемому видео
    /// <summary>
    private Text _videoTitleText = null;

    /// <summary>
    /// Проигрывается ли видеозапись
    /// <summary>
    private bool _isPlaying = false;

    /// <summary>
    /// Что происходит при запуске
    /// <summary>
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

    /// <summary>
    /// Работа кнопки Play/Pause
    /// <summary>
    public void PlayPause()
    {
        if (!_isPlaying){
            _mediaPlayer.Play();
            PlayVideo();
        }
        else{
            _mediaPlayer.Pause();
            PauseVideo();
        }
    }

    /// <summary>
    /// Подготовка интерфейса и переменных к запуску видео
    /// <summary>
    public void PlayVideo()
    {
        _isPlaying = true;
        _playPauseButton.image.sprite = Resources.Load<Sprite>("UI/Pause");
    }

    /// <summary>
    /// Подготовка интерфейса и переменных к паузе
    /// <summary>
    public void PauseVideo()
    {
        _isPlaying = false;
        _playPauseButton.image.sprite = Resources.Load<Sprite>("UI/Play");
    }

    /// <summary>
    /// Смена проигрываемого ролика
    /// <summary>
    public void ChangeVideo(string path)
    {
        var newMediaPath = new MediaPath(path, MediaPathType.AbsolutePathOrURL);
        if (newMediaPath == _mediaPlayer.MediaPath){
            return;
        }
        _mediaPlayer.OpenMedia(newMediaPath, autoPlay:false);
        PauseVideo();
        _videoTitleText.text = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
    }
}
