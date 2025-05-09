using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] public float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] public Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .05f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	public Rigidbody2D m_Rigidbody2D;
		
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.

	private Vector3 m_Velocity = Vector3.zero;

	public Vector2 Forward
    {
		get
        {
			return (FacingRight) ? transform.right : -transform.right;
        }
    }

	public bool Grounded { get { return m_Grounded; } }
	public bool FacingRight => m_FacingRight;
	public bool BypassGrounded = false;
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	float _coyoteTimer = 0;
	float _coyoteTime = 0.1f;

	public string CurrentFloorLayer = "Floor";
	public Vector3 LastGroundPosition;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		
	}

    private void Update()
    {
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Floor"))
					LastGroundPosition = transform.position;	
				CurrentFloorLayer = LayerMask.LayerToName(colliders[i].gameObject.layer);
				_coyoteTimer = _coyoteTime;
				
			}
		}
		bool wasGrounded = m_Grounded;
		m_Grounded = _coyoteTimer > 0;
		if (m_Grounded && !wasGrounded)
		{
			BypassGrounded = false;
			OnLandEvent.Invoke();
		}
		if (_coyoteTimer > -1)
			_coyoteTimer -= Time.deltaTime;


	}


    public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			BypassGrounded = false;
			PlayerController pc = GetComponent<PlayerController>();
			float yVel = m_Rigidbody2D.velocity.y;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
			if (pc.nearDice != null && pc.Carrying)
            {
				SwingDie swing = pc.nearDice.GetComponent<SwingDie>();
				if (swing != null)
                {
					swing.Detach(pc);
					pc.Carrying = false;
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, yVel);
                }
            }
			_coyoteTimer = -1;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce)	);
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		GetComponent<SpriteRenderer>().flipX = !m_FacingRight;
		PlayerController p = GetComponent<PlayerController>();
		if (p.Carrying && p.nearDice != null)
        {
			if (p.nearDice.GetComponent<SwingDie>() == null)
            {

				Vector3 pos = p.nearDice.transform.localPosition;
				pos.x = (m_FacingRight) ? 0.5f : -0.5f;
				p.nearDice.transform.localPosition = pos;
            }
        }
		
	}

}