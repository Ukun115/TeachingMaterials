using UnityEngine;

/// <summary>
/// ビルボード
/// </summary>
public class BillboardSample : MonoBehaviour
{
    /// <summary>
    /// Update
    /// NOTE:
    /// LateUpdateを使う理由は、カメラの移動がUpdateで行われるため、
    /// LateUpdateでビルボードの向きを更新することで、カメラの移動後に正しい向きに調整されるようにするため。
    /// </summary>
    void LateUpdate()
    {
        // カメラの座標を取得
        var targetPos = Camera.main.transform.position;

        // 高さを自分と同じにして、水平方向の向きを計算
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);

        // XとZを固定して、Y軸の回転だけ活かす
        var currentRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(90f, currentRotation.y, 0f);
    }
}