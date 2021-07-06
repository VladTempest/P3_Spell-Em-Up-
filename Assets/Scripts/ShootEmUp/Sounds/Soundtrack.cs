using UnityEngine;

namespace ShootEmUp.Sounds
{
    public class Soundtrack:MonoBehaviour
    {
        public string name="audiotrack";
        public AudioClip[] clip;
        [Range(0f, 1f)]
        public float volume;

        public TypeOfSoundtrack typeOfSoundtrack;
        public TypeOfOSTByItsNature typeOfOstByItsNature;
        public TypeOfSFXByItsNature typeOfSfxByItsNature;
        public TypeOfSFXByNumberOfSounds typeOfSfxByNumberOfSounds;
        public TypeOfSFXByPlace typeOfSfxByPlace;
    }
}
