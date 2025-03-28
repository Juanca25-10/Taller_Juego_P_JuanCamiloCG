using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using models;
using System.IO;
using System;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class GameControllerPreguntas : MonoBehaviour
{
    
    List<PreguntasMultiples> preguntasFacilesPM;
    List<PreguntasMultiples> preguntasDificilesPM;
    List<PreguntasAbiertas> preguntasFacilesPA;
    List<PreguntasAbiertas> preguntasDificilesPA;
    List<PreguntasVerdaderoFalso> preguntasFacilesPVF;
    List<PreguntasVerdaderoFalso> preguntasDificilesPVF;


    System.Random random;

    string lineaLeida;

    public Timer temporizador;
    
    public TextMeshProUGUI txtPreguntaPM;
    public TextMeshProUGUI txtrespuesta1PM;
    public TextMeshProUGUI txtrespuesta2PM;
    public TextMeshProUGUI txtrespuesta3PM;
    public TextMeshProUGUI txtrespuesta4PM;

    public TextMeshProUGUI txtPreguntaPVF;
    public TextMeshProUGUI txtrespuesta1PVF;
    public TextMeshProUGUI txtrespuesta2PVF;

    public TextMeshProUGUI txtPreguntaPA;
    public TextMeshProUGUI txtRespuestaPA;

    public GameObject panelPreguntasPM;
    public GameObject panelPreguntasVF;
    public GameObject panelPreguntasPA;

    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;
    public GameObject panelRespuestaPA;
    public GameObject panelFinal;
    public GameObject panel2daRonda;
    public GameObject panel1eraRonda;
    public GameObject panelIndicadores;
    public GameObject panelHome;
    public GameObject panelCorazones;
    public GameObject Corazon1;
    public GameObject Corazon2;
    public GameObject Corazon3;
    public GameObject IntentosPaila;

    public GameObject panelOvejas;
    public GameObject panelFuegos;

    public TextMeshProUGUI txtContadorMalasIn;
    public TextMeshProUGUI txtContadorBuenasIn;
    public TextMeshProUGUI txtContadorMalasFinal;
    public TextMeshProUGUI txtContadorBuenasFinal;

    public AudioSource AudioBotones;
    public AudioClip AudioBueno;
    public AudioClip AudioMalo;

    int contadorMalas = 0;
    int contadorBuenas = 0;
    int contadorAviso = 1;
    int contadorCorazones = 3;

    string respuesta;
    string preguntaTipo;


    // Start is called before the first frame update
    void Start()
    {

        preguntasFacilesPM = new List<PreguntasMultiples>();
        preguntasDificilesPM = new List<PreguntasMultiples>();
        preguntasFacilesPA = new List<PreguntasAbiertas>();
        preguntasDificilesPA = new List<PreguntasAbiertas>();
        preguntasFacilesPVF = new List<PreguntasVerdaderoFalso>();
        preguntasDificilesPVF = new List<PreguntasVerdaderoFalso>();

        random = new System.Random();

        leerPreguntasMultiples();
        leerPreguntasVerdaderoFalso();
        leerPreguntasAbiertas();


        panelPreguntasPA.SetActive(false);
        panelPreguntasPM.SetActive(false);
        panelPreguntasVF.SetActive(false);
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        panelRespuestaPA.SetActive(false);
        panelIndicadores.SetActive(false);
        panel2daRonda.SetActive(false);
        panel1eraRonda.SetActive(false);
        panelHome.SetActive(true);
        panelFinal.SetActive(false);

    }

    void Update()
    {

    }

    public void resetear()
    {

        preguntasDificilesPA.Clear();
        preguntasDificilesPM.Clear();
        preguntasDificilesPVF.Clear();
        preguntasFacilesPA.Clear();
        preguntasFacilesPM.Clear();
        preguntasFacilesPVF.Clear();
        contadorMalas = 0;
        contadorBuenas = 0;
        txtContadorMalasIn.text = "0";
        txtContadorBuenasIn.text = "0";
        txtContadorMalasFinal.text = "0";
        txtContadorBuenasFinal.text = "0";
        panelFinal.SetActive(false);
        contadorAviso = 1;

        leerPreguntasMultiples();
        leerPreguntasVerdaderoFalso();
        leerPreguntasAbiertas();

        mostrarPreguntaBoton();

    }


    public void mostrarPreguntaBoton()
    {

        int tipoPregunta = UnityEngine.Random.Range(0, 3);
        Debug.Log("Tipo de Pregunta: " + tipoPregunta);

        if (preguntasFacilesPM.Count == 0 && preguntasFacilesPVF.Count == 0 && preguntasFacilesPA.Count == 0 && contadorAviso == 1)
        {
            panelOvejas.SetActive(false);
            panelFuegos.SetActive(true);
            panel2daRonda.SetActive(true);
            contadorAviso--;
        }

        if (preguntasFacilesPM.Count == 0 && preguntasFacilesPVF.Count == 0 && preguntasFacilesPA.Count == 0 && preguntasDificilesPM.Count == 0 && preguntasDificilesPVF.Count == 0 && preguntasDificilesPA.Count == 0)
        {
            panelFinal.SetActive(true);
            panelOvejas.SetActive(true);
            panelFuegos.SetActive(false);
            panelIndicadores.SetActive(false);

            temporizador.TimerStop();

            Debug.Log("No hay mas preguntas");
        }
        else
        {
            switch (tipoPregunta)
            {

                case 0:
                    mostrarPreguntaMultiple();
                    break;
                case 1:
                    mostrarPreguntaVF();
                    break;
                case 2:
                    mostrarPreguntaAbierta();
                    break;
            }
        }
    }

    #region Metodos de Mostrar Pregunta
    public void mostrarPreguntaMultiple()
    {
        int res;

        if (preguntasFacilesPM.Count != 0)
        {
            panelPreguntasPM.SetActive(true);
            panelPreguntasVF.SetActive(false);
            panelPreguntasPA.SetActive(false);
            preguntaTipo = "Multiple";

            res = random.Next(0, preguntasFacilesPM.Count);

            txtPreguntaPM.text = preguntasFacilesPM[res].Pregunta;
            txtrespuesta1PM.text = preguntasFacilesPM[res].Respuesta1;
            txtrespuesta2PM.text = preguntasFacilesPM[res].Respuesta2;
            txtrespuesta3PM.text = preguntasFacilesPM[res].Respuesta3;
            txtrespuesta4PM.text = preguntasFacilesPM[res].Respuesta4;
            respuesta = preguntasFacilesPM[res].RespuestaCorrecta;

            preguntasFacilesPM.RemoveAt(res);
            Debug.Log("Cantidad restantes PM Faciles: " + preguntasFacilesPM.Count());
        }
        else
        {

            if (preguntasFacilesPA.Count == 0 && preguntasFacilesPM.Count == 0 && preguntasFacilesPVF.Count == 0 && preguntasDificilesPM.Count != 0)
            {
                panelPreguntasPM.SetActive(true);
                panelPreguntasVF.SetActive(false);
                panelPreguntasPA.SetActive(false);
                preguntaTipo = "Multiple";

                res = random.Next(0, preguntasDificilesPM.Count);

                txtPreguntaPM.text = preguntasDificilesPM[res].Pregunta;
                txtrespuesta1PM.text = preguntasDificilesPM[res].Respuesta1;
                txtrespuesta2PM.text = preguntasDificilesPM[res].Respuesta2;
                txtrespuesta3PM.text = preguntasDificilesPM[res].Respuesta3;
                txtrespuesta4PM.text = preguntasDificilesPM[res].Respuesta4;
                respuesta = preguntasDificilesPM[res].RespuestaCorrecta;

                preguntasDificilesPM.RemoveAt(res);
                Debug.Log("Cantidad restantes PM Dificiles: " + preguntasDificilesPM.Count());

            }
            else
            {
                Debug.Log("No hay mas preguntas de tipo Multiple");
                mostrarPreguntaBoton();
            }

        }
    }
    public void mostrarPreguntaVF()
    {

        int res;

        if (preguntasFacilesPVF.Count != 0)
        {
            panelPreguntasPM.SetActive(false);
            panelPreguntasVF.SetActive(true);
            panelPreguntasPA.SetActive(false);
            preguntaTipo = "VF";

            res = random.Next(0, preguntasFacilesPVF.Count);

            txtPreguntaPVF.text = preguntasFacilesPVF[res].Pregunta;
            txtrespuesta1PVF.text = "true";
            txtrespuesta2PVF.text = "false";
            respuesta = preguntasFacilesPVF[res].RespuestaCorrecta;

            preguntasFacilesPVF.RemoveAt(res);
            Debug.Log("Cantidad restantes PVF Faciles: " + preguntasFacilesPVF.Count());

        }
        else
        {

            if (preguntasFacilesPA.Count == 0 && preguntasFacilesPM.Count == 0 && preguntasFacilesPVF.Count == 0 && preguntasDificilesPVF.Count != 0)
            {
                panelPreguntasPM.SetActive(false);
                panelPreguntasVF.SetActive(true);
                panelPreguntasPA.SetActive(false);
                preguntaTipo = "VF";

                res = random.Next(0, preguntasDificilesPVF.Count);

                txtPreguntaPVF.text = preguntasDificilesPVF[res].Pregunta;
                txtrespuesta1PVF.text = "true";
                txtrespuesta2PVF.text = "false";
                respuesta = preguntasDificilesPVF[res].RespuestaCorrecta;

                preguntasDificilesPVF.RemoveAt(res);
                Debug.Log("Cantidad restantes PVF Dificiles: " + preguntasDificilesPVF.Count());
            }
            else
            {
                Debug.Log("No hay mas preguntas de tipo V/F");
                mostrarPreguntaBoton();
            }
        }

    }
    public void mostrarPreguntaAbierta()
    {
        int res;

        if (preguntasFacilesPA.Count != 0)
        {
            panelPreguntasPM.SetActive(false);
            panelPreguntasVF.SetActive(false);
            panelPreguntasPA.SetActive(true);
            preguntaTipo = "Abierta";

            res = random.Next(0, preguntasFacilesPA.Count);

            txtPreguntaPA.text = preguntasFacilesPA[res].Pregunta;
            respuesta = preguntasFacilesPA[res].RespuestaCorrecta;

            preguntasFacilesPA.RemoveAt(res);
            Debug.Log("Cantidad restantes PA Faciles: " + preguntasFacilesPA.Count());
        }
        else
        {

            if (preguntasFacilesPA.Count == 0 && preguntasFacilesPM.Count == 0 && preguntasFacilesPVF.Count == 0 && preguntasDificilesPA.Count != 0)
            {
                panelPreguntasPM.SetActive(false);
                panelPreguntasVF.SetActive(false);
                panelPreguntasPA.SetActive(true);
                preguntaTipo = "Abierta";

                res = random.Next(0, preguntasDificilesPA.Count);
                txtPreguntaPA.text = preguntasDificilesPA[res].Pregunta;
                respuesta = preguntasDificilesPA[res].RespuestaCorrecta;
                preguntasDificilesPA.RemoveAt(res);
                Debug.Log("Cantidad restantes PA Dificiles: " + preguntasDificilesPA.Count());
            }
            else
            {
                Debug.Log("No hay mas preguntas de tipo Abiertas");
                mostrarPreguntaBoton();
            }
        }

    }
    #endregion

    public void validarRespuesta(TextMeshProUGUI answer)
    {
        panelCorazones.SetActive(false);
        if (preguntaTipo.Equals("Abierta"))
        {
            panelRespuestaPA.SetActive(true);
            txtRespuestaPA.text = respuesta;
            //contadorBuenas++;
            //txtContadorBuenasIn.text = contadorBuenas.ToString();
            //txtContadorBuenasFinal.text = txtContadorBuenasIn.text;

            AudioBotones.clip = AudioBueno;

            AudioBotones.enabled = false;
            AudioBotones.enabled = true;
        }
        else
        {
            if (string.Equals(answer.text, respuesta, StringComparison.OrdinalIgnoreCase))
            {
                panelCorrecto.SetActive(true);
                contadorBuenas++;
                Debug.Log("Respuesta Correcta");
                txtContadorBuenasIn.text = contadorBuenas.ToString();
                txtContadorBuenasFinal.text = txtContadorBuenasIn.text;
                AudioBotones.clip = AudioBueno;

                AudioBotones.enabled = false;
                AudioBotones.enabled = true;

                contadorCorazones = 3;  

                if (preguntaTipo == "VF")
                {
                   Debug.Log(respuesta);
                }
            }
            else
            {
                if (preguntaTipo == "Multiple")
                {
                    contadorCorazones--;
                    panelCorazones.SetActive(true);

                    switch (contadorCorazones)
                    {

                        case 2:
                            Corazon1.SetActive(false);
                            Corazon2.SetActive(true);
                            Corazon3.SetActive(true);
                            IntentosPaila.SetActive(false);
                            break;
                        case 1:
                            Corazon1.SetActive(false);
                            Corazon2.SetActive(false);
                            Corazon3.SetActive(true);
                            IntentosPaila.SetActive(false);
                            break;
                        case 0:
                            Corazon1.SetActive(false);
                            Corazon2.SetActive(false);
                            Corazon3.SetActive(false);

                            IntentosPaila.SetActive(true);

                            contadorCorazones = 3;
                            break;
                    }
                }

                if (preguntaTipo == "VF")
                {
                    IntentosPaila.SetActive(true);
                }
                panelIncorrecto.SetActive(true);
                contadorMalas++;
                Debug.Log("Respuesta Incorrecta");
                txtContadorMalasIn.text = contadorMalas.ToString();
                txtContadorMalasFinal.text = txtContadorMalasIn.text;

                AudioBotones.clip = AudioMalo;

                AudioBotones.enabled = false;
                AudioBotones.enabled = true;

                if (preguntaTipo == "VF")
                {
                    Debug.Log(respuesta);
                }
            }
        }

    }
    #region Leer Preguntas Metodos
    public void leerPreguntasMultiples()
    {
        List<PreguntasMultiples> listaPM = new List<PreguntasMultiples>();

        try
        {
            StreamReader sr1 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasM.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineapartida = lineaLeida.Split("-");
                string pregunta = lineapartida[0];
                string respuesta1 = lineapartida[1];
                string respuesta2 = lineapartida[2];
                string respuesta3 = lineapartida[3];
                string respuesta4 = lineapartida[4];
                string respuestaCorrecta = lineapartida[5];
                string versiculo = lineapartida[6];
                string dicultad = lineapartida[7];
                PreguntasMultiples objPM = new PreguntasMultiples(pregunta, respuesta1, respuesta2,
                    respuesta3, respuesta4, respuestaCorrecta, versiculo, dicultad);
                listaPM.Add(objPM);
            }
            Debug.Log("Tamaño de Lista de Preguntas Multiples: " + listaPM.Count());
        }
        catch (Exception e)
        {
            Debug.Log("ERROR " + e.ToString());
        }

        foreach (PreguntasMultiples a in listaPM)
        {
            if (a.Dificultad.Equals("Facil", StringComparison.OrdinalIgnoreCase))
            {
                preguntasFacilesPM.Add(a);
            }
            else if (a.Dificultad.Equals("Dificil", StringComparison.OrdinalIgnoreCase))
            {
                preguntasDificilesPM.Add(a);
            }

        }
    }
    public void leerPreguntasVerdaderoFalso()
    {
        List<PreguntasVerdaderoFalso> listaPVF = new List<PreguntasVerdaderoFalso>();

        try
        {
            StreamReader sr2 = new StreamReader("Assets/Resources/Files/preguntasFalso_Verdadero.txt");
            while ((lineaLeida = sr2.ReadLine()) != null)
            {
                string[] lineapartida = lineaLeida.Split("-");
                string pregunta = lineapartida[0];
                string respuestaCorrecta = lineapartida[1];
                string versiculo = lineapartida[2];
                string dicultad = lineapartida[3];
                PreguntasVerdaderoFalso objPVF = new PreguntasVerdaderoFalso(pregunta, respuestaCorrecta, versiculo, dicultad);
                listaPVF.Add(objPVF);
            }
            Debug.Log("Tamaño de Lista de Preguntas VF: " + listaPVF.Count());
        }
        catch (Exception e)
        {
            Debug.Log("ERROR " + e.ToString());
        }

        foreach (PreguntasVerdaderoFalso a in listaPVF)
        {
            if (a.Dificultad.Equals("Facil", StringComparison.OrdinalIgnoreCase))
            {
                preguntasFacilesPVF.Add(a);
            }
            else if (a.Dificultad.Equals("Dificil", StringComparison.OrdinalIgnoreCase))
            {
                preguntasDificilesPVF.Add(a);
            }

        }
    }
    public void leerPreguntasAbiertas()
    {
        List<PreguntasAbiertas> listaPA = new List<PreguntasAbiertas>();

        try
        {
            StreamReader sr3 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasAbiertas.txt");
            while ((lineaLeida = sr3.ReadLine()) != null)
            {
                string[] lineapartida = lineaLeida.Split("-");
                string pregunta = lineapartida[0];
                string respuestaCorrecta = lineapartida[1];
                string versiculo = lineapartida[2];
                string dicultad = lineapartida[3];
                PreguntasAbiertas objPA = new PreguntasAbiertas(pregunta, respuestaCorrecta, versiculo, dicultad);
                listaPA.Add(objPA);
            }
            Debug.Log("Tamaño de Lista de Preguntas Abiertas: " + listaPA.Count());
        }
        catch (Exception e)
        {
            Debug.Log("ERROR " + e.ToString());
        }

        foreach (PreguntasAbiertas a in listaPA)
        {
            if (a.Dificultad.Equals("Facil", StringComparison.OrdinalIgnoreCase))
            {
                preguntasFacilesPA.Add(a);
            }
            else if (a.Dificultad.Equals("Dificil", StringComparison.OrdinalIgnoreCase))
            {
                preguntasDificilesPA.Add(a);
            }

        }

    }
    #endregion

}