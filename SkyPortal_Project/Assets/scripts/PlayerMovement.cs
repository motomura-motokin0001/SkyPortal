using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem; //#RInputSystemを使用する

public class PlayerMovement : MonoBehaviour
{
    [SerializeField,Header("移動速度")] private float _MoveSpeed;//#2B 移動速度
    [SerializeField,Header("ジャンプ")] private float _Jump; //#2B ジャンプ力
    [SerializeField,Header("インベントリCanvas")] private GameObject _Inventory; //#2B インベントリのオブジェクトを格納する変数
    private Vector2 _InputDirection; //#2B 入力の値を格納する変数
    private Rigidbody2D _Rigid2D; //#2B Rigidbody2Dを格納する変数
    private bool _IsJump; //#2B ジャンプしているかどうか

    
    void Start()
    {
        _Rigid2D = GetComponent<Rigidbody2D>();//#gRigidbody2Dを取得
        _IsJump = false; 
        // _Inventory.SetActive(false); //#Gインベントリを非表示にする

    }

    void Update()
    {
        _Move();
    }


    private void _Move()
    {
        _Rigid2D.linearVelocity = new Vector2(_InputDirection.x * _MoveSpeed , _Rigid2D.linearVelocity.y); //#G 移動
    }


private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground")) //#P衝突したオブジェクトがGroundタグを持っている場合
        {
            _IsJump = false; //#Gジャンプしていない状態にする
        }
    }

 //#Y移動の入力がされた場合動く
    public void _OnMove(InputAction.CallbackContext context)
    {
        _InputDirection = context.ReadValue<Vector2>(); //#W入力の値を格納
    }


//#Yジャンプの入力がされた場合動く
    public void _OnJump(InputAction.CallbackContext context) 
    {
        if (context.performed) //#P入力されたら
        {
            if(!context.performed || _IsJump) //#P入力されていなければ
            {
                return; //#R以降は何もしない
            }
            _Rigid2D.AddForce(Vector2.up * _Jump , ForceMode2D.Impulse); //#G上方向に力を加える
            _IsJump = true;
        }
    }

//#Yインベントリを開く処理
    public void _OnOpenInventory(InputAction.CallbackContext context) 
    {
        if (context.performed) //#P入力されたら
        {
            Debug.Log("インベントリ"); //#Gデバッグログを表示
            _Inventory.SetActive(!_Inventory.activeSelf); //#Gインベントリの表示・非表示を切り替える処理
        }
    }
}