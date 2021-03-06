using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Character2D : MonoBehaviour
{

	public float speed = 1.5f; // ???????? ????????
	public float acceleration = 100; // ?????????
	public float jumpForce = 5; // ???? ??????
	public float jumpDistance = 0.75f; // ?????????? ?? ?????? ???????, ?? ??????????? (???????????? ??????? ? ??????????? ?? ???????? ???????)
	public bool facingRight = true; // ? ????? ??????? ??????? ???????? ?? ???????
	public KeyCode jumpButton = KeyCode.Space; // ??????? ??? ??????
	public string ladderTag = "GameController"; // ??? ???????

	private int layerMask;
	private Rigidbody2D body;
	private Vector3 upLadder, downLadder, ladderPos, direction;
	private bool isLadder;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		body.freezeRotation = true;
		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == ladderTag && !isLadder)
		{
			Ladder ladder = other.GetComponent<Ladder>();
			upLadder = ladder.up.position;
			downLadder = ladder.down.position;
			ladderPos = other.transform.position;
			isLadder = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == ladderTag)
		{
			isLadder = false;
			body.isKinematic = false;
		}
	}

	bool GetJump() // ?????????, ???? ?? ????????? ??? ??????
	{
		bool result = false;

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, jumpDistance, layerMask);
		if (hit.collider)
		{
			result = true;
		}

		return result;
	}

	void FixedUpdate()
	{
		if (!body.isKinematic) body.AddForce(direction * body.mass * speed * acceleration);

		if (Mathf.Abs(body.velocity.x) > speed)
		{
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
		}
	}

	void Flip() // ????????? ?? ???????????
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Update()
	{
		Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red); // ?????????, ??? ?????????? ????????? jumpDistance

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if (Input.GetKeyDown(jumpButton) && GetJump() || Input.GetKeyDown(jumpButton) && isLadder && v == 0)
		{
			body.isKinematic = false;
			body.velocity = new Vector2(0, jumpForce);
		}

		if (isLadder) LadderMode(v);

		direction = new Vector2(h, 0);

		if (h > 0 && !facingRight) Flip(); else if (h < 0 && facingRight) Flip();
	}

	void LadderMode(float vertical)
	{
		if (transform.position.y < upLadder.y && vertical > 0)
		{
			body.isKinematic = true;
		}
		else if (transform.position.y > downLadder.y && vertical < 0 && transform.position.y > upLadder.y)
		{
			body.isKinematic = true;
		}
		else if (vertical < 0 && GetJump() && transform.position.y < upLadder.y)
		{
			body.isKinematic = false;
		}

		if (body.isKinematic)
		{
			transform.Translate(new Vector2(0, speed * vertical * Time.fixedDeltaTime)); // ???????? ?? ????????
			float xPos = Mathf.Lerp(transform.position.x, ladderPos.x, 10 * Time.deltaTime);
			transform.position = new Vector2(xPos, transform.position.y); // ??????? ???????????? ?? ?????? ????????
		}
	}
}