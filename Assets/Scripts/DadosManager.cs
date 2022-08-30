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
    public int maxDadosNovia = 1;
    [Range(0, 1)]
    public int maxDadosNovio = 1;
    [Range(0, 1)]
    public int maxDadosSanta = 1;


    private List<Dado> dados = new List<Dado>();
    private Dado[] dadosEscogidos;
    private int[] numDadosEscogidos;

    public Image[] images;
    public TMP_Text[] text;
    public TMP_Text puntajeText;
    public Button tirarBoton;

    public int cerebrosParaGanar = 7;
    public int disparosParaPerder = 3;
    int cerebros, disparos;
    bool atrapoNovio;

    // Start is called before the first frame update
    void Start()
    {
        StartNewRound();
    }

    void FillDice()
    {
        dados.Clear();

        for(int i = 0; i < maxDadosVerdes; i++)
        {
            dados.Add(new Dado(Dado.TipoDeDado.Verde));
        }//Crear dados verdes

        for (int i = 0; i < maxDadosAmarillos; i++)
        {
            dados.Add(new Dado(Dado.TipoDeDado.Amarillo));
        }//Crear dados amarillos

        for (int i = 0; i < maxDadosRojos; i++)
        {
            dados.Add(new Dado(Dado.TipoDeDado.Rojo));
        }//Crear dados rojos

        for (int i = 0; i < maxDadosNovia; i++)
        {
            dados.Add(new Dado(Dado.TipoDeDado.Novia));
        }//Crear dados novia
        for (int i = 0; i < maxDadosNovio; i++)
        {
            dados.Add(new Dado(Dado.TipoDeDado.Novio));
        }//Crear dados novio
        for (int i = 0; i < maxDadosSanta; i++)
        {
            dados.Add(new Dado(Dado.TipoDeDado.Santa));
        }//Crear dados novio
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

            else if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Novia)
                images[j].color = Color.magenta;

            else if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Novio)
                images[j].color = Color.white;

            else if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Santa)
                images[j].color = Color.gray;
        }
    }

    public void TirarDados()
    {
        tirarBoton.interactable = false;
        int[] tiros = new int[3];

        for (int i = 0; i < dadosEscogidos.Length; i++)
        {
            int tiro = Random.Range(0, dadosEscogidos[i].caras.Length);
            tiros[i] = tiro;
        }// Hacer tiro de dados

        for(int j = 0; j < dadosEscogidos.Length; j++)
        {
            CaraDado.Cara cara = dadosEscogidos[j].caras[tiros[j]];
            text[j].text = cara.ToString();

            if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Novia)
            {
                if (cara == CaraDado.Cara.Cerebro)
                {
                    cerebros++;
                    atrapoNovio = true;
                }// subir un cerebro y atrapar novia

                else if (cara == CaraDado.Cara.Disparo)
                {
                    disparos++;
                }// Subir numero de disparos
            }// Salio Novia

            else if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Novio)
            {
                if (cara == CaraDado.Cara.Cerebrox2)
                {
                    cerebros += 2;
                    atrapoNovio = true;
                }// subir dos cerebros y atrapar novia

                else if (cara == CaraDado.Cara.Disparo)
                {
                    disparos++;;
                }// Subir numero de disparos

                else if (cara == CaraDado.Cara.Disparox2)
                {
                    disparos += 2;
                }// Subir dos de disparos

            }// Salio Novio

            else if (dadosEscogidos[j].tipoDeDado == Dado.TipoDeDado.Santa)
            {
                if (cara == CaraDado.Cara.Cerebro)
                {
                    cerebros ++;
                }// subir un cerebro

                else if (cara == CaraDado.Cara.Cerebrox2)
                {
                    cerebros += 2;
                }// Subir dos cerebros

                else if (cara == CaraDado.Cara.Disparo)
                {
                    disparos++;
                }// subir un disparo

                else if (cara == CaraDado.Cara.Casco)
                {
                    bool blockedOnce = false;
                    for (int i = 0; i < dadosEscogidos.Length; i++)
                    {
                        CaraDado.Cara cara2 = dadosEscogidos[i].caras[tiros[i]];

                        if ((cara2 == CaraDado.Cara.Disparo || cara2 == CaraDado.Cara.Disparox2) && !blockedOnce)
                        {
                            disparos--;
                            dados.Add(new Dado(Dado.TipoDeDado.Santa));
                            blockedOnce = true;
                        }// Restar un disparo en el tiro actual
                    }// Checar si hay un disparo en el tiro actual
                }// Evitar un disparo

                else if (cara == CaraDado.Cara.PatasACerebros)
                {
                    for (int i = 0; i < dadosEscogidos.Length; i++)
                    {
                        CaraDado.Cara cara2 = dadosEscogidos[i].caras[tiros[i]];

                        if (cara2 == CaraDado.Cara.Patas)
                        {
                            cerebros++;
                            cara2 = CaraDado.Cara.Cerebro;
                        }// Convertir patas a cerebro
                    }// Checar si hay patas en el tiro actual
                }// Convertir patas a cerebros

            }// Salio Santa

            else if (cara == CaraDado.Cara.Cerebro)
            {
                cerebros++;
            }// Subir puntuacion de cerebros

            else if (cara == CaraDado.Cara.Disparo)
            {
                disparos++;
            }// Subir numero de disparos
        }

        for(int k = 0; k < dadosEscogidos.Length; k++)
        {
            if(dadosEscogidos[k].tipoDeDado == Dado.TipoDeDado.Novia || dadosEscogidos[k].tipoDeDado == Dado.TipoDeDado.Novio)
            {
                if (dadosEscogidos[k].caras[tiros[k]] == CaraDado.Cara.Patas)
                {
                    dadosEscogidos[k] = null;
                    if (atrapoNovio)
                    {
                        cerebros--;
                        dados.Add(new Dado(Dado.TipoDeDado.Novia));
                        dados.Add(new Dado(Dado.TipoDeDado.Novio));
                    }// Regresar novios a la bolsa
                    continue;
                }
            }

            if (dadosEscogidos[k].caras[tiros[k]] != CaraDado.Cara.Patas)
            {
                dados.Remove(dadosEscogidos[k]);
                dadosEscogidos[k] = null;
            }
        }

        puntajeText.text = cerebros.ToString() + " cerebros, " + disparos.ToString() + " disparos";

        if(disparos >= disparosParaPerder)
        {
            puntajeText.text = "Perdiste";
            StartCoroutine(Reiniciar());
        }
        else if (cerebros >= cerebrosParaGanar)
        {
            puntajeText.text = "Ganaste";
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
        atrapoNovio = false;
        puntajeText.text = cerebros.ToString() + " cerebros, " + disparos.ToString() + " disparos";
        dadosEscogidos = new Dado[3];
        numDadosEscogidos = new int[3];
        FillDice();
        EscogerDados();
    }

    IEnumerator NuevosDados()
    {
        yield return new WaitForSeconds(5);
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
