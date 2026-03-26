using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    public float moveSpeed = 10f;   // 移動スピード
    public float rotateSpeed = 100f; // 回転スピード（度/秒）

    void Update()
    {
        // 移動の処理
        float x = 0;
        float z = 0;
        float rotate = 0; // 回転用の変数

        if (Keyboard.current != null)
        {
            // 移動（WASD / 矢印）
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) z = 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) z = -1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1;

            // 回転（Q/E）の入力を取得
            // Qキーで左回転（マイナス）、Eキーで右回転（プラス）
            if (Keyboard.current.qKey.isPressed) rotate = -1;
            if (Keyboard.current.eKey.isPressed) rotate = 1;
        }

        // 移動を実行
        Vector3 move = new Vector3(x, 0, z) * moveSpeed * Time.deltaTime;
        transform.Translate(move);

        // 回転を実行（Y軸を中心に回転させる）
        // Vector3.up は (0, 1, 0) なので、水平に回ります
        transform.Rotate(Vector3.up, rotate * rotateSpeed * Time.deltaTime);
    }
}