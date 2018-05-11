using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotFromClamped : MonoBehaviour
{

    public Transform _targetTr;
    public float _to;
    public enum _rotAxis
    {
        X, Y, Z, Custom
    }
    public _rotAxis _axis;

    [SerializeField]
    [Range(0f, 1f)]
    private float m_phase;
    public float _phase
    {
        get { return m_phase; }
        set
        {
            m_phase = value;
            // --- mainFunction
            switch (_axis)
            {

                case _rotAxis.X:
                    _targetTr.localRotation = Quaternion.LerpUnclamped(Quaternion.identity, Quaternion.Euler(new Vector3(_to, 0f, 0f)), value);
                    break;

                case _rotAxis.Y:
                    _targetTr.localRotation = Quaternion.LerpUnclamped(Quaternion.identity, Quaternion.Euler(new Vector3(0f, _to, 0f)), value);
                    break;

                case _rotAxis.Z:
                    _targetTr.localRotation = Quaternion.LerpUnclamped(Quaternion.identity, Quaternion.Euler(new Vector3(0f, 0f, _to)), value);
                    break;

                case _rotAxis.Custom:
                    // 注意 MoveFromToのEaser Rotateのカーブは直線の / にした方がわかりやすくなります。
                    Vector3 _eulerRotation = new Vector3(_curveX.Evaluate(value) * _to, _curveY.Evaluate(value) * _to, _curveZ.Evaluate(value) * _to);
                    _targetTr.localRotation = Quaternion.Euler(_eulerRotation);
                    break;

            }
        }
    }


    [Header("CustomRotation")]

    public AnimationCurve _curveX = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));
    public AnimationCurve _curveY = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f)),
          _curveZ = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

    public void OverrideValueTo(float nw)
    {

        _to = nw;
    }



}
