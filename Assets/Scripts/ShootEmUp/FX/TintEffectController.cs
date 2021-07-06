using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp.FX
{
    public class TintEffectController : MonoBehaviour
    {
        private SpriteRenderer[] _spriteRenderers;
        private Dictionary<SpriteRenderer,Color> _startColors;

        private Coroutine _tintCoroutine;
        [SerializeField]
        private Color _colorForTint=new Color(1f,0,0);
        private int _numberOfCycles = 3;
        private float _pauseBetweenTints = 0.08f;
        
        
        
        void Awake()
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            _startColors = new Dictionary<SpriteRenderer, Color>(); 
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                _startColors.Add(spriteRenderer,spriteRenderer.color);
        }

       

        void SetDefaultColors()
        {
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                spriteRenderer.color=_startColors[spriteRenderer];
        }

        void SetTintColor()
        {
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                spriteRenderer.color=_colorForTint;
        }
        public void StartTintCoroutine()
        {
            StopTintCoroutine();
            _tintCoroutine = StartCoroutine(TintCycle());
        }
        private void StopTintCoroutine()
        {
            if (_tintCoroutine!=null) StopCoroutine(_tintCoroutine);
            SetDefaultColors();
            _tintCoroutine = null;
        }

        IEnumerator TintCycle()
        {
            var currentNumberOfCycles = 1;
            while (currentNumberOfCycles<=_numberOfCycles)
            {
                SetTintColor();
                yield return new WaitForSeconds(_pauseBetweenTints);
                SetDefaultColors();
                currentNumberOfCycles++;
                yield return new WaitForSeconds(_pauseBetweenTints);
            }

        }

        private void OnDestroy()
        {
            StopTintCoroutine();
        }




    }
}

