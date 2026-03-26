using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            // カメラの座標を取得
            Vector3 targetPos = Camera.main.transform.position;

            // 高さを自分と同じにして、水平方向の向きを計算
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);

            // ここがポイント：XとZを固定して、Y軸の回転だけ活かす
            Vector3 currentRotation = transform.eulerAngles;
            transform.eulerAngles = new Vector3(90f, currentRotation.y, 0f);
        }
    }
}