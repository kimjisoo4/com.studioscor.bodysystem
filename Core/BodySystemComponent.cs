using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.BodySystem
{

    [DefaultExecutionOrder(BodySystemExcutionOrder.MAIN_ORDER)]
    [AddComponentMenu("StudioScor/BodySystem/BodySystem Component", order: 0)]
    public class BodySystemComponent : BaseMonoBehaviour
    {
        #region Events
        public delegate void ChangedBodyPartHandler(BodySystemComponent bodySystem, BodyTag body, BodyPartComponent transform);
        #endregion

        [Header(" [ Body System ] ")]
        [SerializeField] private Dictionary<BodyTag, BodyPartComponent> _BodyParts;

        public event ChangedBodyPartHandler OnGrantedBodyPart;
        public event ChangedBodyPartHandler OnRemovedBodyPart;

        private void Awake()
        {
            Setup();
        }
        private void Setup()
        {
            if (_BodyParts == null)
            {
                _BodyParts = new Dictionary<BodyTag, BodyPartComponent>();
            }
        }

        protected virtual void OnSetup() { }


        public bool TryGetBodyPart(BodyTag body, out BodyPartComponent bodyPart)
        {
            return _BodyParts.TryGetValue(body, out bodyPart);
        }
        public bool HasBodyPart(BodyTag bodyTag)
        {
            return _BodyParts.ContainsKey(bodyTag);
        }
        public BodyPartComponent TryGetBodyPart(BodyTag body)
        {
            if(_BodyParts.TryGetValue(body, out BodyPartComponent bodyPart))
            {
                return bodyPart;
            }
            else
            {
                return null;
            }
        }

        public bool TryGrantBodyPart(BodyTag body, BodyPartComponent transform)
        {
            if(_BodyParts.TryAdd(body, transform))
            {
                Callback_OnGrantedBodyPart(body, transform);

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TryRemoveBodyPart(BodyTag body)
        {
            if (_BodyParts.TryGetValue(body, out BodyPartComponent value))
            {
                _BodyParts.Remove(body);

                Callback_OnRemovedBodyPart(body, value);

                return true;
            }
            else
            {
                return false;
            }
        }

        #region Events CallBack
        public void Callback_OnGrantedBodyPart(BodyTag bodyTag, BodyPartComponent bodyPart)
        {
            OnGrantedBodyPart?.Invoke(this, bodyTag, bodyPart);
        }
        public void Callback_OnRemovedBodyPart(BodyTag bodyTag, BodyPartComponent bodyPart)
        {
            OnRemovedBodyPart?.Invoke(this, bodyTag, bodyPart);
        }
        #endregion
    }

}
