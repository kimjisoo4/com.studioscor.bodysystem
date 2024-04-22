using StudioScor.Utilities;
using System;
using UnityEngine;

namespace StudioScor.BodySystem.Variable
{
    [Serializable]
    public class BodyPositionVariable : PositionVariable
    {
        [Header(" [ Body Position Variable ] ")]
        [SerializeField] private BodyTag _bodyTag;
        [SerializeField] private Vector3 _offset = Vector3.zero;

        private IBodySystem _bodySystem;
        private BodyPositionVariable _original;

        public override void Setup(GameObject owner)
        {
            base.Setup(owner);

            _bodySystem = owner.GetBodySystem();
        }

        public override IPositionVariable Clone()
        {
            var clone = new BodyPositionVariable();

            clone._original = this;

            return clone;
        }

        public override Vector3 GetValue()
        {
            var tag = _original is null ? _bodyTag : _original._bodyTag;

            if (_bodySystem.TryGetBodyPart(tag, out IBodyPart bodypart))
            {
                return bodypart.transform.TransformPoint(_original._offset);
            }
            else
            {
                return _bodySystem.transform.TransformPoint(_original._offset);
            }
        }
    }
}
