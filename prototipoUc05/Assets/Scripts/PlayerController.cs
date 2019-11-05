using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private Rigidbody2D rb2d;

	public Transform posPe;
	[HideInInspector] public bool tocaChao = false;


	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool viradoDireita = true;

	public Image vida;
	private MensagemControle MC;

    public Transform bulletSpawn;
    public GameObject bulletObject;
    public float fireRate;
    public float nextFire;

    void Start()
    {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

		GameObject mensagemControleObject = GameObject.FindWithTag ("MensagemControle");
		if (mensagemControleObject != null) {
			MC = mensagemControleObject.GetComponent<MensagemControle> ();
		}
	}
	
	
	void Update () {
		//Implementar Pulo Aqui! 
	}

	void FixedUpdate()
	{
		float translationY = 0;
		float translationX = Input.GetAxis ("Horizontal") * Velocidade;
		transform.Translate (translationX, translationY, 0);
		transform.Rotate (0, 0, 0);
		if (translationX != 0) {
			anim.SetTrigger ("corre");
		} else {
			anim.SetTrigger("parado");
		}

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Velocidade * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.right * Velocidade * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Fire();
        }
		//Programar o pulo Aqui! 

		if (translationX > 0 && !viradoDireita) {
			Flip ();
		} else if (translationX < 0 && viradoDireita) {
			Flip();
		}

	}
	void Flip()
	{
		viradoDireita = !viradoDireita;
		Vector3 escala = transform.localScale;
		escala.x *= -1;
		transform.localScale = escala;
	}

	public void SubtraiVida()
	{
		vida.fillAmount-=0.1f;
		if (vida.fillAmount <= 0) {
			MC.GameOver();
			Destroy(gameObject);
		}
	}
	
    void Fire()
    {
        float translationY = 0;
		float translationX = Input.GetAxis ("Horizontal") * Velocidade;
		transform.Translate (translationX, translationY, 0);
		transform.Rotate (0, 0, 0);
       
        anim.SetTrigger("Fire");
        nextFire = Time.time + fireRate;

        GameObject cloneBullet = Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);
        
        if (translationX < 0 && viradoDireita)
        {
            cloneBullet.transform.eulerAngles =new Vector3 (0, 0, 180);
        }
        
    
        
    }

}
