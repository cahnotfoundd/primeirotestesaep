using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb; // referência ao componente Rigidbody2D do jogador
    private InputAction moveAction; // ação de entrada para o movimento do jogador
    private Vector2 moveDirection; // direção do movimento do jogador
    private Camera cam; // referência à câmera principal do jogo
    private Vector2 halfSize; // metade do tamanho do player para limitar o movimento dentro da tela
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        cam = Camera.main; // referencia camera principal
        halfSize = GetComponent<SpriteRenderer>().bounds.extents; // pega metade do tamanho do player para limitar o movimento dentro da tela

        moveAction = new InputAction("Move", InputActionType.Value);
       
       //movimentação WASD
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // movimentação com setas
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");
    }

    private void OnEnable()  { moveAction.Enable(); }
    private void OnDisable() { moveAction.Disable(); }

    private void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime; // calcula a nova posição do jogador com base na direção de movimento e velocidade

        //cantos da tela convertidos para coordenadas do mundo
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)); //canto inferior esquerdo da tela
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));//canto superior direito da tela

        newPosition.x = Mathf.Clamp(newPosition.x, min.x + halfSize.x, max.x - halfSize.x); // limita a posição do jogador dentro da tela
        newPosition.y = Mathf.Clamp(newPosition.y, min.y + halfSize.y, max.y - halfSize.y); // limita a posição do jogador dentro da tela

        rb.MovePosition(newPosition);
    }
}