using UnityEngine;
using System.Collections;
//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Completed;

public class PlayerController : MonoBehaviour
{
    //public float speed;
    //public GameObject bulletPrefab;
    //public GameObject muzzle;
    private float thrust = 20;
    //private float brake = 3;
    private float angularVelocity;
    //Vector2 lookDirection = Vector2.zero;
    Rigidbody2D rgbody;

    public Text Points;          //Store a reference to the UI Text component which will display the number of pickups collected.
    public Text Events;            //Store a reference to the UI Text component which will display the 'You win' message.
    public Text IntroText;
    public Text EndText;
    public Text EnderText;
    public Text EndestText;
    public Image EndImage;
    public int points;
    public int eventTimer;
    public string username;
    public bool playing;
    public bool nameSet;
    public bool gameWin;
    public Sprite[] sprites;
    public AudioClip squish;
    public AudioClip bump;
    public AudioClip hit;
    public AudioClip win;
    public AudioClip spin;
    public AudioClip lose;
    public AudioClip pay;
    public AudioClip boing;

    // Use this for initialization
    void Start()
    {
        rgbody = GetComponent<Rigidbody2D>();
        points = 0;
        eventTimer = 0;
        EndImage.CrossFadeAlpha(0, 0, true);
        gameWin = false;
        nameSet = false;
        playing = false;
        sprites = new Sprite[3];
        sprites[0] = Resources.Load<Sprite>("Splat");
        sprites[1] = Resources.Load<Sprite>("TrafficConeCrushed");
        sprites[2] = Resources.Load<Sprite>("CrushedTaxi");
        IntroText.text = "Welcome to your final driving test.\nPlease type in your name:\n";
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        //lookDirection = (mousePos - (Vector2)transform.position).normalized;
        //transform.right = lookDirection;
        if (Events.material.color.a > 0 && eventTimer < 100)
        {
            Events.material.color = new Color(255, 255, 255, Events.material.color.a /*- (float)0.01*/);
        }
        if (eventTimer > 0)
        {
            eventTimer--;
        }
        if (eventTimer == 0)
        {
            Events.text = "";
        }

        if (playing && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            IntroText.text = "";
        }

        if (!playing && !nameSet)
        {
            IntroText.text = "Welcome to your final driving test.\nPlease type in your name:\n" + username;

            if (Input.GetKey(KeyCode.Return))
            {
                IntroText.text = "Good luck for your driving test.\nTake us to the finish line please.\nWASD or Arrow Keys to move.";
                nameSet = true;
                playing = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                username += "A";
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                username += "B";
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                username += "C";
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                username += "D";
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                username += "E";
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                username += "F";
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                username += "G";
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                username += "H";
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                username += "I";
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                username += "J";
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                username += "K";
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                username += "L";
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                username += "M";
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                username += "N";
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                username += "O";
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                username += "P";
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                username += "Q";
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                username += "Q";
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                username += "R";
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                username += "S";
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                username += "T";
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                username += "U";
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                username += "V";
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                username += "W";
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                username += "X";
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                username += "Y";
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                username += "Z";
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                username += " ";
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (username.Length > 0)
                    username = username.Substring(0, username.Length - 1);
            }

        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (playing)
        {
            if (coll.collider.gameObject.CompareTag("Taxi"))
            {
                eventTimer = 200;
                Events.text = "You hit a taxi!\n10 Penalty Points";
                points += 10;
                coll.collider.GetComponent<SpriteRenderer>().sprite = sprites[2];
                SoundController.instance.PlaySingle(hit);
                int i = 0;
                foreach (Collider2D c in coll.collider.gameObject.GetComponents<Collider2D>())
                {
                    if (i < 2)
                    {
                        c.enabled = false;
                        i++;
                    }
                }
            }
            else
            {
                eventTimer = 200;
                Events.text = "Don't dent the car!\n1 Penalty Point";
                SoundController.instance.PlaySingle(bump);
                points += 1;
            }
        }
        SetText();
    }
    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (playing)
        {
            //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
            if (other.gameObject.CompareTag("Durian"))
            {
                //... then set the other object we just collided with to inactive.
                other.enabled = false;
                eventTimer = 200;
                Events.text = "You hit a durian!\n3 Penalty Points";
                Events.material.color = new Color(255, 255, 255, (float)1);
                SoundController.instance.PlaySingle(squish);
                //Add one to the current value of our count variable.
                points = points + 3;
                other.GetComponent<SpriteRenderer>().sprite = sprites[0];
                //other.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Splat");
            }
            else if (other.gameObject.CompareTag("TrafficCone"))
            {
                //... then set the other object we just collided with to inactive.
                other.enabled = false;
                eventTimer = 200;
                Events.text = "You hit a traffic cone!\n7 Penalty Points";
                Events.material.color = new Color(255, 255, 255, (float)1);
                SoundController.instance.PlaySingle(bump);
                //Add one to the current value of our count variable.
                points = points + 7;
                other.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else if (other.gameObject.CompareTag("ERP"))
            {
                //... then set the other object we just collided with to inactive.
                //other.gameObject.SetActive(false);
                eventTimer = 200;
                Events.text = "You crossed an ERP Gantry!\n5 Penalty Points";
                Events.material.color = new Color(255, 255, 255, (float)1);
                SoundController.instance.PlaySingle(pay);
                //Add one to the current value of our count variable.
                points = points + 5;
            }
            else if (other.gameObject.CompareTag("Peel"))
            {
                other.gameObject.SetActive(false);
                SoundController.instance.PlaySingle(spin);
                if (Random.value > 0.5)
                    angularVelocity += 1000;
                else
                    angularVelocity += -1000;
                rgbody.AddForce(transform.up * 600);
            }
            /*else if (other.gameObject.CompareTag("Taxi"))
            {
                //... then set the other object we just collided with to inactive.
                //other.gameObject.SetActive(false);
                eventTimer = 200;
                Events.text = "You hit a taxi!\n10 Penalty Points";
                Events.material.color = new Color(255, 255, 255, (float)1);
                //Add one to the current value of our count variable.
                points = points + 10;
                other.GetComponent<SpriteRenderer>().sprite = sprites[2];
            }*/
            else if (other.gameObject.CompareTag("RedLine"))
            {
                //... then set the other object we just collided with to inactive.
                //other.gameObject.SetActive(false);
                eventTimer = 200;
                Events.text = "Don't go off road!\n4 Penalty Points";
                SoundController.instance.PlaySingle(bump);
                Events.material.color = new Color(255, 255, 255, (float)1);
                //Add one to the current value of our count variable.
                points = points + 4;
            }
            else if (other.gameObject.CompareTag("FinishLine"))
            {
                //... then set the other object we just collided with to inactive.
                other.gameObject.SetActive(false);
                eventTimer = 200;
                SoundController.instance.PlaySingle(win);
                EndText.text = "You made it to the end!\n You accumulated " + points + " penalty points.\nPress \"Enter\" to see your result.";
                Events.text = "";
                playing = false;
                gameWin = true;
                //Add one to the current value of our count variable.

            }
        }
        //Update the currently displayed count by calling the SetCountText function.
        SetText();
    }

    void SetText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        Points.text = "Penalty Points: " + points;

        //Check if we've collected all 12 pickups. If we have...
        if (points > 50)
        {    //... then set the text property of our winText object to "You win!"
            EndText.text = "You accumulated too many penalty points!\nPress \"Enter\" to continue";
            Events.text = "";
            SoundController.instance.PlaySingle(lose);
            playing = false;
            gameWin = true;
        }
    }

    void LateUpdate()
    {
    }

    void FixedUpdate()
    {

        if (nameSet)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Return) && gameWin)
        {
            EndImage.CrossFadeAlpha(255, 0, false);
            EnderText.text = "Close Enough!\n\n\n\n\n\n\n\n\n\nClass ";
            if (points < 10)
                EnderText.text += "A";
            else if (points < 20)
                EnderText.text += "B";
            else if (points < 30)
                EnderText.text += "C";
            else if (points < 40)
                EnderText.text += "D";
            else if (points < 50)
                EnderText.text += "E";
            else
                EnderText.text += "F";
            EnderText.text += " Licence";
            EndText.text = "";
            if (username.Length > 0)
                EndestText.text = username;
            else
                EndestText.text = "Player";
        }
        if (playing)
        {
            //Vector3 velocity = Vector3.zero;
            //bool AorD = false;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                //velocity += Vector3.up * speed;
                rgbody.AddForce(transform.up * thrust);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //velocity += Vector3.left * speed;
                //AorD = true;
                if (angularVelocity < 70)
                    angularVelocity += 20;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //velocity += Vector3.down * speed;
                rgbody.AddForce(-transform.up * (thrust));
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //velocity += Vector3.right * speed;
                //AorD = true;
                if (angularVelocity > -70)
                    angularVelocity -= 20;
            }
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }


                if (angularVelocity > 0)
                    angularVelocity -= 10;
                else if (angularVelocity < 0)
                    angularVelocity += 10;

            rgbody.angularVelocity = angularVelocity;
            /*
            if (Input.GetMouseButtonDown (0)) {
                //Instantiate bullet
                GameObject newBullet = Instantiate(bulletPrefab);
                newBullet.transform.position = muzzle.transform.position;
                newBullet.transform.rotation = this.transform.rotation;
                newBullet.GetComponent<Rigidbody2D> ().velocity = (bulletSpeed + speed) * lookDirection;
            }
            */
        }
    }
}

/*using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;             //Floating point variable to store the player's movement speed.

    public float angle;
    public float rotation = 0;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        angle = 0;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        //float moveHorizontal = Input.GetAxis("Horizontal");
        float moveHorizontal = 0;

        float newangle = angle + Input.GetAxis("Horizontal");
        float difangle = angle - newangle;

        transform.Rotate(0, 0, difangle);

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
}
*/