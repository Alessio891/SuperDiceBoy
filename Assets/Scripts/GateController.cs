using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    BoxCollider2D coll;
    public int number = 1;
    public List<Sprite> sprites;
    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<ParticleSystem>().textureSheetAnimation.SetSprite(0, sprites[number - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        bool player1 = PlayerController.Player1.Carrying && PlayerController.Player1.CurrentNumber == number;
        bool player2 = PlayerController.Player2 != null && PlayerController.Player2.Carrying && PlayerController.Player2.CurrentNumber == number;
        if (player1 && player2)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            coll.enabled = false;
        } else if (player1 && !player2)
        {
            coll.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("IgnorePlayer1");
        } else if (player2 && !player1)
        {
            coll.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("IgnorePlayer2");
        } else
        {
            coll.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

    }
}
