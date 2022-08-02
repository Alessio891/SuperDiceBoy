using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireworkSpawner : MonoBehaviour
{
    public Firework Preafab;
    public Vector2 SpeedRange;
    public Vector2 LifeRange;
    public float Delay = 0.5f;
    float _timer = 0;
    public AudioSource BGM;
    public Player input;
    public Text Congratz, Thanks, AnyKey;
    bool canReset = false;
    public GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Coop", 0) == 0)
            player2.SetActive(false);
        input = new Player();
        bool bgm = PlayerPrefs.GetInt("BGM", 1) == 1;
        if (!bgm)
            BGM.Stop();
        else
            BGM.Play();

        int vol = PlayerPrefs.GetInt("Volume", 5);
        float val = BGM.volume * (float)((float)vol / 10.0f);
        BGM.volume = val;

        _timer = Delay;
        Camera.main.gameObject.LeanMoveLocalY(2.4f, 10.5f).setEaseOutSine().setOnComplete(()=> {
            gameObject.LeanValue((f) => { AnyKey.color = new Color(1, 1, 1, f); }, 0.0f, 1.0f, 0.5f).setEaseOutSine().setDelay(2.0f);
        });
        Congratz.color = new Color(1,1, 1, 0);
        Thanks.color = new Color(1, 1, 1, 0);
        AnyKey.color = new Color(1, 1, 1, 0);
        gameObject.LeanValue((f) => { Congratz.color = new Color(1, 1, 1, f); }, 0.0f, 1.0f, 1.5f).setEaseOutSine().setDelay(2.0f);
        gameObject.LeanValue((f) => { Thanks.color = new Color(1, 1, 1, f); }, 0.0f, 1.0f, 1.5f).setEaseOutSine().setDelay(3.0f).setOnComplete(() => canReset = true);
    }

    bool reset = false;
    public void Restart(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) return;
        reset = true;
    }

    // Update is called once per frame
    void Update()
    {
           
        if ( ( Keyboard.current.anyKey.wasPressedThisFrame || reset) && canReset)
        {
            SceneManager.LoadScene(0);
        }

        _timer += Time.deltaTime;
        if (_timer >= Delay)
        {
            Delay = Random.Range(0.4f, 1.5f);
            Firework f = GameObject.Instantiate<Firework>(Preafab);
            f.transform.SetParent(transform);
            f.transform.localPosition = new Vector3(Random.Range(-8.0f, 8.0f), 0, 0);
            f.Speed = Random.Range(SpeedRange.x, SpeedRange.y);
            f.AliveTime = Random.Range(LifeRange.x, LifeRange.y);
            _timer = 0;
        }
    }
}
