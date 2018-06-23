using RootMotion.FinalIK;
using UnityEngine;

[RequireComponent(typeof(VRIK))]
class VRIKScaler: MonoBehaviour {
    [SerializeField, Range(0.1f, 5f)]
    private float m_scale = 1f;
    public float scale
    {
        get { return m_scale; }
        set
        {
            transform.localScale = initialScale * value;

            var vrikLoco = vrik.solver.locomotion;

            vrikLoco.footDistance = initialLoco.footDistance * value;
            vrikLoco.stepThreshold = initialLoco.stepThreshold * value;
            vrikLoco.maxVelocity = initialLoco.maxVelocity * value;
            vrikLoco.velocityFactor = initialLoco.velocityFactor * value;

            for (var i = 0; i < vrikLoco.stepHeight.keys.Length; i++) {
                var newKey = initialLoco.stepHeight.keys [i];
                newKey.value *= value;
                vrikLoco.stepHeight.MoveKey(i, newKey);
            }

            for (var i = 0; i < vrikLoco.heelHeight.keys.Length; i++) {
                var newKey = initialLoco.heelHeight.keys [i];
                newKey.value *= value;
                vrikLoco.heelHeight.MoveKey(i, newKey);
            }
        }
    }

    VRIK vrik;

    Vector3 initialScale;
    IKSolverVR.Locomotion initialLoco;

    void Awake () {
        vrik = GetComponent<VRIK>();
        initialScale = transform.localScale;

        initialLoco = new IKSolverVR.Locomotion();
        var vrikLoco = vrik.solver.locomotion;

        initialLoco.footDistance = vrikLoco.footDistance;
        initialLoco.stepThreshold = vrikLoco.stepThreshold;
        initialLoco.maxVelocity = vrikLoco.maxVelocity;
        initialLoco.velocityFactor = vrikLoco.velocityFactor;
        initialLoco.stepHeight = new AnimationCurve(vrikLoco.stepHeight.keys);
        initialLoco.heelHeight = new AnimationCurve(vrikLoco.heelHeight.keys);
    }

    void Update () {/*
        transform.localScale = initialScale * scale;

        var vrikLoco = vrik.solver.locomotion;

        vrikLoco.footDistance = initialLoco.footDistance * scale;
        vrikLoco.stepThreshold = initialLoco.stepThreshold * scale;
        vrikLoco.maxVelocity = initialLoco.maxVelocity * scale;
        vrikLoco.velocityFactor = initialLoco.velocityFactor * scale;

        for (var i = 0; i < vrikLoco.stepHeight.keys.Length; i++) {
            var newKey = initialLoco.stepHeight.keys [i];
            newKey.value *= scale;
            vrikLoco.stepHeight.MoveKey(i, newKey);
        }

        for (var i = 0; i < vrikLoco.heelHeight.keys.Length; i++) {
            var newKey = initialLoco.heelHeight.keys [i];
            newKey.value *= scale;
            vrikLoco.heelHeight.MoveKey(i, newKey);
        }*/
    }
}