using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    /// <summary>
    /// 移動スピード
    /// </summary>
    private float _moveSpeed = 10f;

    /// <summary>
    /// 回転スピード（度/秒）
    /// </summary>
    private float _rotateSpeed = 100f;

    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        // 移動の処理


        // 移動（WASD / 矢印）


        // 回転（Q/E）の入力を取得
        // Qキーで左回転（マイナス）、Eキーで右回転（プラス）


        // 移動を実行


        // 回転を実行（Y軸を中心に回転させる）
        // Vector3.up は (0, 1, 0) なので、水平に回る

    }
}