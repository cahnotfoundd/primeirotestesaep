using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // prefab da bala
    [SerializeField] private Transform firePoint; // ponto de disparo da bala
    [SerializeField] private float fireRate = 0.25f; // segundos entre cada disparo

    private Camera cam; // referência à câmera principal do jogo
    private InputAction fireAction; // ação de entrada para o disparo
    private float nextFireTime; // tempo do próximo disparo
    
    private void Awake()
    {
        cam = Camera.main; // referencia camera principal
        
        // cria a ação de entrada para o disparo
        fireAction = new InputAction("Shoot", InputActionType.Button); 
        //indica que o disparo será feito com o botão esquerdo do mouse
        fireAction.AddBinding("<Mouse>/leftButton");
    }

    // ativando e desativando a ação de disparo 
    private void OnEnable()  { fireAction.Enable(); }
    private void OnDisable() { fireAction.Disable(); }
    
    void Update()
    {
        if (fireAction.ReadValue<float>() > 0.5f && Time.time >= nextFireTime) // verifica se o botão de disparo foi pressionado e se o tempo do próximo disparo já passou
        {
            Shoot(); // chama a função de disparo
            nextFireTime = Time.time + fireRate; // atualiza o tempo do próximo disparo
        }

    }

    private void Shoot()
    {
       if (Mouse.current == null) return; // verifica se o mouse está presente

        // 1. onde está o mouse na tela
        Vector2 mousePos = Mouse.current.position.ReadValue(); // pega a posição do mouse na tela

        //2. converte a posição do mouse para coordenadas do mundo
        Vector2 worldMousePos = cam.ScreenToWorldPoint(mousePos); // converte a posição do mouse para coordenadas do mundo

        //3. direção do disparo da bala
        Vector2 direction = worldMousePos - (Vector2)firePoint.position;

        //4. rotação do ponto de disparo da bala
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // calcula o ângulo da direção do disparo
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle); // cria a rotação do ponto de disparo da bala

        Instantiate(bulletPrefab, firePoint.position, rotation); // instancia a bala no ponto de disparo com a rotação do ponto de disparo
    }
}
