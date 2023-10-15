using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // A anotação "SerializeField" em atributos privados expõem uma propriedade
    // na interface da Unity (atributos publicos causam efeito similar)
    [SerializeField]
    private float  velocidade = 3f;

    private Rigidbody2D rb;
    private Vector2 direcaoDoMovimento;

    private SpriteRenderer sr;
    private float proximaMudanca;

    // Awake é invocado quando o script é inicalizado
    private void Awake()
    {
        // Obtém a referência do objeto RigidBody do GameObject associado ao
        // Script (Sprite Quadrado) armazenando no atributo "rb"
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        proximaMudanca = 0.0f;
    }

    // Update é invocado a cada atualização de frame
    void Update()
    {
        // "Input.GetAxisRaw" retorna "-1", "0" ou "1" sendo que:
        // Para "Horizontal" será -1 (Esquerda), 0 (Centro) e 1 (Direita)
        float horizontal = Input.GetAxisRaw("Horizontal");
        // Para "Horizontal" será -1 (Baixo), 0 (Centro) e 1 (Cima)
        float vertical = Input.GetAxisRaw("Vertical");

        // Seta "direcaoDoMovimento" com os valores de "horizontal" e "vertical"
        direcaoDoMovimento = new Vector2(horizontal, vertical);

        if ((Time.time > proximaMudanca) && Input.GetButton("Jump"))
        {
            proximaMudanca = Time.time + 0.10f;
            if(sr.color == Color.white) {
                sr.color= Color.red;
            } else {
                sr.color= Color.white;
            }
        }
    }

    // Update é invocado em uma frequencia de frames fixa ideal para uso de física
    private void FixedUpdate()
    {
        // Calcula a nova posição multiplicando "velocidade" ao deltatime do jogo e à direção
        // somando o resultado à posição atual.
        Vector3 movePosition = (velocidade * Time.fixedDeltaTime * direcaoDoMovimento) + rb.position;
        // Atribui à posição atual do sprite o valor calculado
        rb.MovePosition(movePosition);
    }
}
