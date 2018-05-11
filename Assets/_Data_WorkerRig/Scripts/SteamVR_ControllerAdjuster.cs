using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Steam V r controller adjuster.
/// コントローラーをセットすると、カメラから見て左右を判断し、
/// もし左右がまちがっていれば入れ替えます。
/// </summary>
public class SteamVR_ControllerAdjuster : MonoBehaviour
{
    /// <summary>
    /// 左手コントローラーのインスタンスです 
    /// </summary>
    [SerializeField] private GameObject leftController;

    /// <summary>
    /// 左手コントローラーのインスタンスです 
    /// </summary>
    [SerializeField] private GameObject rightController;

    private Transform _head;

    /// <summary>
    /// ローカルのX座標を返します。
    /// 中谷変更：カメラ（Head）に対しての相対位置に変更。
    /// </summary>
    /// <returns>The x.</returns>
    /// <param name="obj">Compare.</param>
    private float GetX(GameObject obj)
    {
        //return obj.transform.localPosition.x;
        return _head.InverseTransformPoint(obj.transform.position).x;
    }

    /// <summary>
    /// Switch the specified left and right.
    /// コントローラーの左右を切り替えます。
    /// </summary>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    private void Switch(GameObject left, GameObject right)
    {
        SteamVR_TrackedObject tObjL = left.GetComponent<SteamVR_TrackedObject>();
        SteamVR_TrackedObject tObjR = right.GetComponent<SteamVR_TrackedObject>();
        SteamVR_TrackedObject.EIndex temp = tObjL.index;
        tObjL.index = tObjR.index;
        tObjR.index = temp;

        SteamVR_TrackedController tCtrL = left.GetComponent<SteamVR_TrackedController>();
        SteamVR_TrackedController tCtrR = right.GetComponent<SteamVR_TrackedController>();
        tCtrL.controllerIndex = (uint)tObjL.index;
        tCtrR.controllerIndex = (uint)tObjR.index;
    }

    /// <summary>
    /// Start this instance.
    /// このインスタンスがシーンに作られたときに呼ばれて、
    /// 左右のコントローラが入っているかを確かめ、
    /// メインカメラが入っていなければ取得します。
    /// </summary>
    private void Start()
    {
        Assert.IsNotNull(leftController);
        Assert.IsNotNull(rightController);

        _head = Camera.main.transform;
    }

    /// <summary>
    /// Adjusts the LR.
    /// </summary>
    public void AdjustLR()
    {
        float left_x = GetX(leftController);
        float right_x = GetX(rightController);

        // 左右が正しければ終了
        if (left_x < right_x) { return; }

        // 左右が間違っているので修正
        Switch(leftController, rightController);
    }

    /// <summary>
    /// 必要があれば入れ替え処理を実行する
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AdjustLR();
    }
}
