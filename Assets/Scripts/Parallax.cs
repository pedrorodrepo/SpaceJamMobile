using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Parallax é um efeito usado em jogos 2D para criar uma sensação de profundidade.
    O fundo (background) se move mais lentamente em relação ao primeiro plano (foreground),
    dando a impressão de que elementos estão a diferentes distâncias do jogador.
    Isso enriquece a experiência visual do jogo.
*/
public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed;

    private float spriteHeight;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * parallaxSpeed * Time.deltaTime);

        if (transform.position.y < startPos.y - spriteHeight) {
            transform.position = startPos;
        }
    }
}
