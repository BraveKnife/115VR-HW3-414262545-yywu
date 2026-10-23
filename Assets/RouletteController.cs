using UnityEngine;
using UnityEngine.InputSystem; // 確保有引入這行

public class RouletteController : MonoBehaviour
{
    public float rotSpeed = 0f;          
    public float maxSpeed = 1000f;       
    public float speedDamping = 0.985f;  

    public AudioSource audioSource;     
    public AudioClip wheelSound;        

    private bool isSpinning = false;    

    void Update()
    {
        // 只使用新版 Input System 檢測滑鼠左鍵，避免觸發舊版報錯
        bool isClicked = false;
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            isClicked = true;
        }

        // 點擊且目前沒有在旋轉時，啟動轉動
        if (isClicked && !isSpinning)
        {
            StartSpinning();
        }

        // 旋轉與自動減速邏輯
        if (isSpinning)
        {
            transform.Rotate(0, 0, -rotSpeed * Time.deltaTime); 
            rotSpeed *= speedDamping; // 逐步減速

            // 當速度降到極低時，自動停止
            if (rotSpeed < 1f)
            {
                StopSpinning();
            }
        }
    }

    void StartSpinning()
    {
        rotSpeed = Random.Range(maxSpeed * 0.8f, maxSpeed);
        isSpinning = true;

        if (audioSource != null && wheelSound != null)
        {
            audioSource.clip = wheelSound;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

    void StopSpinning()
    {
        rotSpeed = 0f;
        isSpinning = false;

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}