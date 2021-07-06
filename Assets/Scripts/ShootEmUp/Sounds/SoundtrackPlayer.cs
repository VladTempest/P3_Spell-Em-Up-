using System;
using System.Collections.Generic;
using ShootEmUp.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp.Sounds
{
    public class SoundtrackPlayer : SingletonBase<SoundtrackPlayer>
    {
        private Dictionary<TypeOfOSTByItsNature, Soundtrack> _ostDictionary;
        private Dictionary<TypeOfSFXByItsNature, Soundtrack> _sfxDictionary;

        [SerializeField]
        private AudioSource _ostAudioSource;
        [SerializeField]
        private AudioSource _sfxAudioSource;
        [SerializeField]
        private GameObject _localSFXPlayer;
        [SerializeField]
        private SoundtracksListSettings _listOfSoundtracks;

        private void Awake()
        {
            _ostDictionary = new Dictionary<TypeOfOSTByItsNature, Soundtrack>();
            _sfxDictionary = new Dictionary<TypeOfSFXByItsNature, Soundtrack>();
            FillDictionaries();
        }

        private void FillDictionaries()
        {
            foreach (Soundtrack soundtrack in _listOfSoundtracks.list)
            {
                switch (soundtrack.typeOfSoundtrack)
                {
                    case TypeOfSoundtrack.OST:
                        _ostDictionary.Add(soundtrack.typeOfOstByItsNature, soundtrack);
                        break;
                    case TypeOfSoundtrack.SFX:
                        _sfxDictionary.Add(soundtrack.typeOfSfxByItsNature, soundtrack);
                        break;
                }
            }
        }

        public void PlaySoundtrack(TypeOfOSTByItsNature typeOfOstByItsNature = TypeOfOSTByItsNature.None, TypeOfSFXByItsNature typeOfSfxByItsNature = TypeOfSFXByItsNature.None, Transform transformOfPlayPoint = null)
        {
            if (typeOfOstByItsNature == TypeOfOSTByItsNature.None && typeOfSfxByItsNature == TypeOfSFXByItsNature.None)
            {
                Debug.LogWarning("There is no any soundtrack for playing");
            }

            Soundtrack soundtrack;
            if (_ostDictionary.TryGetValue(typeOfOstByItsNature, out soundtrack) == false)
            {
                soundtrack = _sfxDictionary[typeOfSfxByItsNature];
            }

            PlayCertainSoundtrack(soundtrack, transformOfPlayPoint);

        }

        private void PlayCertainSoundtrack(Soundtrack soundtrack,Transform transformOfPlayPoint = null)
        {
            switch (soundtrack.typeOfSoundtrack)
            {
                case TypeOfSoundtrack.OST:
                    _ostAudioSource.Stop();
                    _ostAudioSource.PlayOneShot(soundtrack.clip[ReturnRandomIndexOfClip(soundtrack)],soundtrack.volume);
                    break;
                case TypeOfSoundtrack.SFX:
                    if (soundtrack.typeOfSfxByPlace == TypeOfSFXByPlace.Global)
                    {
                        _sfxAudioSource.PlayOneShot(soundtrack.clip[ReturnRandomIndexOfClip(soundtrack)],soundtrack.volume);
                        break;
                    }
                    else
                    {
                        var localSFXPLayer = Instantiate(_localSFXPlayer, transformOfPlayPoint.position, transformOfPlayPoint.rotation);
                        var localAudioSource = localSFXPLayer.GetComponent<AudioSource>();
                        var randomClipFromArray = soundtrack.clip[ReturnRandomIndexOfClip(soundtrack)];
                        localAudioSource.PlayOneShot(randomClipFromArray,soundtrack.volume);
                        Destroy(localSFXPLayer,randomClipFromArray.length);
                        break;
                    }
                    
                    
            }
        }

        private int ReturnRandomIndexOfClip(Soundtrack soundtrack)
        {
            return Random.Range(0, soundtrack.clip.Length);
        } 
        
        
    }
}
