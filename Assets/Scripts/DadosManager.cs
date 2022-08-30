using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DadosManager : MonoBehaviour
{
    [Range(1, 6)]
    public int maxDadosVerdes = 6;
    [Range(1, 4)]
    public int maxDadosAmarillos = 4;
    [Range(1, 3)]
    public int maxDadosRojos = 3;
    [Range(0, 1)]
    public int maxDadosChica = 1;


    private List<Dado> dados = new List<Dado>();
    private Dado[] dadosEscogidos;
    public int[] numDadosEscogidos;

    public Image[] images;
    public TMP_Text[] text;
    public TMP_Text puntajeText;
    public Button tirarBoton;

    public int cerebrosParaGanar = 7;
    public int disparosParaPerder = 3;
    int cerebros, disparos;

    // Start is called before the first frame update
    void Start()
    {
        StartNewRound();
    }

    void FillDice()
    {
        for(int i = 0; i < maxDadosVerdes; i++)
        {
            //dados[counter] = new Dado(Dado.TipoDeDado.Verde);
            dados.Add(new Dado(Dado.TipoDeDado.Verde));
        }//Crear dados verdes

        for (int i = 0; i < maxDadosAmarillos; i++)
        {
            //dados[counter] = new Dado(Dado.TipoDeDado.Amarillo);
            dados.Add(new Dado(Dado.TipoDeDado.Amarillo));
        }//Crear dados amarillos

        for (int i = 0; i < maxDadosRojos; i++)
        {
            //dados[counter] = new Dado(Dado.TipoDeDado.Rojo);
            dados.Add(new Dado(Dado.TipoDeDado.Rojo));
        }//Crear dados rojos
    }

    public void EscogerDados()
    {
        for (int i = 0; i < dadosEscogidos.Length; i++)
        {
            bool repetido = false;
            if(dadosEscogidos[i] == null || dadosEscogidos[i].caras == null)
            {
                text[i].text = "Dado " + (i + 1);
                int randomDice = Random.Range(0, dados.Count);

                for(int j = 0; j < dadosEscogidos.Length; j++)
                {
                    if(dadosEscogidos[j] == dados[randomDice])
                    {
                        i--;
                        repetido = true;
                        //dadosEscogidos[i] = null;
                    }//El dados escogido ya esta en la lista
                }//Checar los dados escogidos
                if (!repetido)
                {
                    dadosEscogidos[i] = dados[randomDice];
                    numDadosEscogidos[i] = randomDice;
                }
            }
        }

        SetUI();
    }

    public void SetUI()
    {
        for (int j = 0; j < dadosEscogidos.Length; j++)
        {
            if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Verde)
                images[j].color = Color.green;

            else if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Amarillo)
                images[j].color = Color.yellow;

            else if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Rojo)
                images[j].color = Color.red;
        }
    }

    public void TirarDados()
    {
        tirarBoton.interactable = false;

        for(int j = 0; j < dadosEscogidos.Length; j++)
        {
            int tiro = Random.Range(0, dadosEscogidos[j].caras.Length);
            CaraDado.Cara cara = dadosEscogidos[j].caras[tiro];
            text[j].text = cara.ToString();

            if (cara == CaraDado.Cara.Cerebro)
            {
                cerebros++;
                puntajeText.text = cerebros.ToString() + " cerebros, " + disparos.ToString() + " disparos";
                dados.Remove(dadosEscogidos[j]);
                dadosEscogidos[j] = null;
            }// Subir puntuacion de cerebros

            else if (cara == CaraDado.Cara.Disparo)
            {
                disparos++;
                puntajeText.text = cerebros.ToString() + " cerebros, " + disparos.ToString() + " disparos";
                dados.Remove(dadosEscogidos[j]);
                dadosEscogidos[j] = null;
            }// Subir numero de disparos
        }

        if(cerebros >= cerebrosParaGanar)
        {
            puntajeText.text = "Ganaste";
            StartCoroutine(Reiniciar());
        }
        else if(disparos >= disparosParaPerder)
        {
            puntajeText.text = "Perdiste";
            StartCoroutine(Reiniciar());
        }
        else
        {
            StartCoroutine(NuevosDados());
        }

    }

    public void StartNewRound()
    {
        cerebros = 0;
        disparos = 0;
        puntajeText.text = cerebros.ToString() + " cerebros, " + disparos.ToString() + " disparos";
        dadosEscogidos = new Dado[3];
        numDadosEscogidos = new int[3];
        FillDice();
        EscogerDados();
    }

    IEnumerator NuevosDados()
    {
        yield return new WaitForSeconds(3);
        EscogerDados();
        tirarBoton.interactable = true;
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(2);
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
