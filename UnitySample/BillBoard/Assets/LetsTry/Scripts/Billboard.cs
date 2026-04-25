using UnityEngine;

/// <summary>
/// ビルボード
/// </summary>
public class Billboard : MonoBehaviour
{
    /// <summary>
    /// Update
    /// NOTE:
    /// LateUpdateを使う理由は、カメラの移動がUpdateで行われるため、
    /// LateUpdateでビルボードの向きを更新することで、カメラの移動後に正しい向きに調整されるようにするため。
    /// </summary>
    void LateUpdate()
    {
        // Let's Try カメラの座標を取得


        // Let's Try 高さを自分と同じにして、水平方向の向きを計算


        // Let's Try XとZを固定して、Y軸の回転だけ活かす

    }
}