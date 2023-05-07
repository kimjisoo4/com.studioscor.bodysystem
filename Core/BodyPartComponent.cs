using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.BodySystem
{
    public interface IBodyPart
    {
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public IBodySystem BodySystem { get; }
        public BodyTag BodyTag { get; }
        public bool CanEquiped { get; }
        public void SetCanEquip(bool isEquip);
    }

    [DefaultExecutionOrder(BodySystemExcutionOrder.SUB_ORDER)]
    public class BodyPartComponent : BaseMonoBehaviour, IBodyPart
    {
        [Header(" [ Body Part Component ] ")]
        [SerializeField] private BodyTag _BodyTag;
        [SerializeField] private bool _CanEquiped = true;

        private IBodySystem _BodySystem;
        public BodyTag BodyTag => _BodyTag;
        public IBodySystem BodySystem => _BodySystem;
        public bool CanEquiped => _CanEquiped;

#if UNITY_EDITOR
        private void Reset()
        {
            _BodySystem = GetComponentInParent<IBodySystem>();
        }
#endif
        private void Awake()
        {
            if(_BodySystem is null)
            {
                if(!transform.TryGetComponentInParentOrChildren(out _BodySystem))
                {
                    Log($"{gameObject} is Not Has {nameof(IBodySystem)}!!!", true);
                }
            }
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

        public void SetCanEquip(bool canEquip)
        {
            if (_CanEquiped == canEquip)
                return;

            _CanEquiped = canEquip;
        }
    }
}
