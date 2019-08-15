using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject currentWeapon;
    public GameObject LevelMenu;
    public CharacterStats stats;
    public Perk choice1;    // Temporary way to get perk.
    public Perk choice2;
    [HideInInspector] public bool busy;
    [HideInInspector] public AnimationController animationController;
    [HideInInspector] public Transform hand;
    [HideInInspector] public float currentHandAngle;
    [HideInInspector] public int damage;
    [HideInInspector] public Rigidbody2D myBody;

    private Vector2 lastMove;
    private int level;
    private float moveSpeed;
    private int current_animationID;
    private PlayerAbilityManager abilityManager;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
        current_animationID = 0;
        Sprite[] sprites = Resources.LoadAll<Sprite>("Art/CharacterSpriteSheet");
        animationController = this.GetComponent<AnimationController>();
        animationController.Animations = new AnimationController.SpriteAnimation[6];
        animationController.Animations[0] = new AnimationController.SpriteAnimation();
        animationController.Animations[0].name = "Idle";
        animationController.Animations[0].sprites = new Sprite[4];
        animationController.Animations[0].sprites[0] = sprites[45];
        animationController.Animations[0].sprites[1] = sprites[46];
        animationController.Animations[0].sprites[2] = sprites[47];
        animationController.Animations[0].sprites[3] = sprites[48];

        animationController.Animations[1] = new AnimationController.SpriteAnimation();
        animationController.Animations[1].name = "Left_Walk";
        animationController.Animations[1].sprites = new Sprite[7];
        animationController.Animations[1].sprites[0] = sprites[0];
        animationController.Animations[1].sprites[1] = sprites[1];
        animationController.Animations[1].sprites[2] = sprites[2];
        animationController.Animations[1].sprites[3] = sprites[3];
        animationController.Animations[1].sprites[4] = sprites[4];
        animationController.Animations[1].sprites[5] = sprites[5];
        animationController.Animations[1].sprites[6] = sprites[6];

        animationController.Animations[2] = new AnimationController.SpriteAnimation();
        animationController.Animations[2].name = "Right_Walk";
        animationController.Animations[2].sprites = new Sprite[7];
        animationController.Animations[2].sprites[0] = sprites[49];
        animationController.Animations[2].sprites[1] = sprites[50];
        animationController.Animations[2].sprites[2] = sprites[51];
        animationController.Animations[2].sprites[3] = sprites[52];
        animationController.Animations[2].sprites[4] = sprites[53];
        animationController.Animations[2].sprites[5] = sprites[54];
        animationController.Animations[2].sprites[6] = sprites[55];

        animationController.Animations[3] = new AnimationController.SpriteAnimation();
        animationController.Animations[3].name = "Up_Walk";
        animationController.Animations[3].sprites = new Sprite[7];
        animationController.Animations[3].sprites[0] = sprites[37];
        animationController.Animations[3].sprites[1] = sprites[38];
        animationController.Animations[3].sprites[2] = sprites[39];
        animationController.Animations[3].sprites[3] = sprites[40];
        animationController.Animations[3].sprites[4] = sprites[41];
        animationController.Animations[3].sprites[5] = sprites[42];
        animationController.Animations[3].sprites[6] = sprites[43];

        animationController.Animations[4] = new AnimationController.SpriteAnimation();
        animationController.Animations[4].name = "Down_Walk";
        animationController.Animations[4].sprites = new Sprite[7];
        animationController.Animations[4].sprites[0] = sprites[29];
        animationController.Animations[4].sprites[1] = sprites[30];
        animationController.Animations[4].sprites[2] = sprites[31];
        animationController.Animations[4].sprites[3] = sprites[32];
        animationController.Animations[4].sprites[4] = sprites[33];
        animationController.Animations[4].sprites[5] = sprites[34];
        animationController.Animations[4].sprites[6] = sprites[35];

        animationController.Animations[5] = new AnimationController.SpriteAnimation();
        animationController.Animations[5].name = "Down_Attack";
        animationController.Animations[5].sprites = new Sprite[5];
        animationController.Animations[5].sprites[0] = sprites[23];
        animationController.Animations[5].sprites[1] = sprites[24];
        animationController.Animations[5].sprites[2] = sprites[25];
        animationController.Animations[5].sprites[3] = sprites[26];
        animationController.Animations[5].sprites[4] = sprites[27];

            
        myBody = GetComponent<Rigidbody2D>();
        hand = transform.Find("PlayerHand");
        currentHandAngle = 0;
        stats = new CharacterStats(5, 5, 5, 5, 5, 5, 100);
        level = getLevel();
        moveSpeed = stats.GetStat(Stat.StatType.Speed).GetFinalStatValue();
        abilityManager = GetComponent<PlayerAbilityManager>();
        busy = false;
    }
	// Update is called once per frame
	void Update () {

        if (!busy)
        {

            // Use an ability
            foreach (KeyCode key in abilityManager.abilityKeys)
            {
                if (Input.GetKeyDown(key)) {
                    if (abilityManager.GetAbility(key))
                    {
                        StartCoroutine(abilityManager.GetAbility(key).UseAbility(this));
                        break;
                    }
                }
            }

            /*
             * Old ability code
            if (dashTimeRemaining > 0) {
                dashTimeRemaining -= Time.deltaTime;
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                // Attack
                // Rotate attack towards mouse
                Vector3 mousePos = Input.mousePosition;
                mousePos.x -= Screen.width / 2;
                mousePos.y -= Screen.height / 2;
                float newAngle = 190 + Vector3.SignedAngle(new Vector3(1, 0, 0), mousePos, new Vector3(0, 0, -1));
                currentWeapon.GetComponent<IWeapon>().PerformAttack(20);
                hand.RotateAround(transform.position, new Vector3(0, 0, 1), currentHandAngle - newAngle);
                currentHandAngle = newAngle;

                damage = currentWeapon.GetComponent<IWeapon>().BaseDamage + stats.GetStat(Stat.StatType.Strength).GetFinalStatValue();
                currentWeapon.GetComponent<IWeapon>().PerformAttack(damage);
                anim.Play("Attack");
            } else if (Input.GetKeyDown(KeyCode.Q)) {
                // Dash towards the mouse position
                Vector3 dashDirection = (Input.mousePosition - new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0)).normalized;
                myBody.velocity = dashDirection * dashSpeed;
                dashTimeRemaining = dashTime;

            } else {
            */

            // If no abilities used, move
            if (!busy)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");

                if (horizontal > .5f) {
                    myBody.velocity = new Vector2(horizontal * moveSpeed, myBody.velocity.y);
                    lastMove = new Vector2(horizontal, 0);
                    if (current_animationID != 2) {
                        current_animationID = 2;
                        animationController.PlayAnimation(2, 15);
                    }
                }

                else if (horizontal < -.5f) {
                    myBody.velocity = new Vector2(horizontal * moveSpeed, myBody.velocity.y);
                    lastMove = new Vector2(horizontal, 0);
                    if (current_animationID != 1) {
                        current_animationID = 1;
                        animationController.PlayAnimation(1, 15);
                    }
                }

                if (vertical > .5f) {
                    myBody.velocity = new Vector3(myBody.velocity.x, vertical * moveSpeed);
                    lastMove = new Vector2(0, vertical);
                    if (current_animationID != 3) {
                        current_animationID = 3;
                        animationController.PlayAnimation(3, 15);
                    }
                }

                else if (vertical < -.5f) {
                    myBody.velocity = new Vector3(myBody.velocity.x, vertical * moveSpeed);
                    lastMove = new Vector2(0, vertical);
                    if (current_animationID != 4) {
                        current_animationID = 4;
                        animationController.PlayAnimation(4, 15);
                    }
                }

                if (Mathf.Abs(horizontal) < .5f)
                {
                    myBody.velocity = new Vector2(0f, myBody.velocity.y);
                }

                if (Mathf.Abs(vertical) < .5f)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, 0f);
                }
                //anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
                //anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
                //anim.SetFloat("LastX", lastMove.x);
                //anim.SetFloat("LastY", lastMove.y);
            }
        }
    }

    public int getLevel() {
        return (int) Mathf.Sqrt(stats.GetStat(Stat.StatType.Experience).GetFinalStatValue());
    }

    public void LevelUp() {
        level++;
        Debug.Log("Leveled up to " + level + "!");
        Perk[] newPerks = new Perk[5];
        newPerks[0] = choice1;
        newPerks[1] = choice1;
        newPerks[2] = choice1;
        newPerks[3] = choice2;
        newPerks[4] = choice2;
        LevelMenu.GetComponent<LevelUpMenu>().LevelUp(newPerks);
        // Check if player gained enough xp to level up multiple times
        if (getLevel() != level) {
            LevelUp();
        }
    }

    public void getXP(int xp) {
        stats.AddStatAdditive(new StatAdditive(xp, Stat.StatType.Experience, "enemy"));
        // Did player level up?
        if (getLevel() != level) {
            LevelUp();
        }
    }
}
