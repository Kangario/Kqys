using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundPlaying : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _sounds;
    [SerializeField] private float _timeDelay;

    private int _currentTrackIndex = 0;

    private void Start()
    {
        if (_sounds.Count > 0)
        {
            _audioSource.clip = _sounds[_currentTrackIndex];
            _audioSource.Play();
            StartCoroutine(PlayTracksWithDelay());
        }
    }

    private IEnumerator PlayTracksWithDelay()
    {
        while (true)
        {
            yield return new WaitWhile(() => _audioSource.isPlaying);

            yield return new WaitForSeconds(_timeDelay);

            _currentTrackIndex = (_currentTrackIndex + 1) % _sounds.Count;
            _audioSource.clip = _sounds[_currentTrackIndex];
            _audioSource.Play();
        }
    }
}
