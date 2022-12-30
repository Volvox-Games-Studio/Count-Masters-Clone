using DG.Tweening;
using UnityEngine;

namespace Emre
{
    public enum SkinButtonState
    {
        Locked,
        NonSelected,
        Selected
    }
    
    
    public class SkinButton : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private RectTransform main;
        [SerializeField] private GameObject lockedGameObject;
        [SerializeField] private GameObject selectedGameObject;

        [Header("Values")]
        [SerializeField] private AnimationCurve punchCurve;
        [SerializeField, Min(0f)] private float punchDuration;
        [SerializeField, Min(0f)] private float shrinkSize;
        [SerializeField, Min(0f)] private float shakeDuration;


        public SkinButtonState State
        {
            get
            {
                var defaultState = Index == 0 ? SkinButtonState.Selected : SkinButtonState.Locked;
                return (SkinButtonState) PlayerPrefs.GetInt(SaveKey, (int) defaultState);
            }
            private set
            {
                lockedGameObject.SetActive(value == SkinButtonState.Locked);
                selectedGameObject.SetActive(value == SkinButtonState.Selected);
                PlayerPrefs.SetInt(SaveKey, (int) value);
                
                if (value == SkinButtonState.Selected) GameEvents.RaiseSkinChanged(Index);
            }
        }

        public int Index
        {
            get
            {
                var parent = transform.parent;
                return transform.GetSiblingIndex() + parent.GetSiblingIndex() * parent.childCount;
            }
        }

        
        private string SaveKey => $"Skin_{Index}";


        private Vector2 m_StartAnchorPos;
        private Tween m_AnchorPosTween;
        private Tween m_SizeTween;
        

        private void Start()
        {
            m_StartAnchorPos = main.anchoredPosition;
            State = State;
        }


        public void OnClicked()
        {
            TrySelect();
        }
        
        public void Unlock()
        {
            State = SkinButtonState.Selected;
            DoPunch();
        }

        public void Deselect()
        {
            State = SkinButtonState.NonSelected;
        }
        
        
        private bool TrySelect()
        {
            if (State == SkinButtonState.Locked)
            {
                DoShake();
                return false;
            }

            State = SkinButtonState.Selected;
            DoPunch();
            return true;
        }

        private void DoPunch()
        {
            m_SizeTween?.Kill();
            m_SizeTween = main.DOScale(Vector3.one, punchDuration)
                .From(shrinkSize);

            m_SizeTween
                .SetEase(punchCurve);
        }

        private void DoShake()
        {
            m_AnchorPosTween?.Kill();
            
            main.anchoredPosition = m_StartAnchorPos;
            m_AnchorPosTween = main.DOShakeAnchorPos(shakeDuration, 75f, 50, 90f);
            m_AnchorPosTween
                .OnUpdate(() =>
                {
                    var anchorPos = main.anchoredPosition;
                    anchorPos.y = m_StartAnchorPos.y;
                    main.anchoredPosition = anchorPos;
                });
        }
    }
}
