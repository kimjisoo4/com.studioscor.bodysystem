using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.BodySystem
{
    [DefaultExecutionOrder(BodySystemExcutionOrder.SUB_ORDER)]
    public class BodyPartComponent : BaseMonoBehaviour
    {
        [Header(" [ Body Part Component ] ")]
        [SerializeField] private BodySystemComponent _BodySystem;
        [SerializeField] private BodyTag _BodyTag;

        public BodySystemComponent BodySystem
        {
            get
            {
                if(_BodySystem == null)
                {
                    _BodySystem = GetComponentInParent<BodySystemComponent>();
                }

                return _BodySystem;
            }
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _BodySystem = GetComponentInParent<BodySystemComponent>();
        }
#endif
        private void Awake()
        {
            if(_BodySystem == null)
                _BodySystem = GetComponentInParent<BodySystemComponent>();
        }

        private void OnEnable()
        {
            Log("Try Grant Body Part - " + _BodyTag);

            BodySystem.TryGrantBodyPart(_BodyTag, this);
        }
        private void OnDisable()
        {
            Log("Try Remove Body Part - " + _BodyTag);

            BodySystem.TryRemoveBodyPart(_BodyTag);
        }
    }
}
