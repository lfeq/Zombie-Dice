using System;

[Serializable]
public class CaraDado
{
    public enum Cara { Cerebro, Patas, Disparo, Cerebrox2, Disparox2, Casco, PatasACerebros }
}

[Serializable]
public class Dado
{
    public enum TipoDeDado {  Verde, Amarillo, Rojo, Novia, Novio, Santa }
    public TipoDeDado tipoDeDado;
    public CaraDado.Cara[] caras;

    public Dado(TipoDeDado _tipoDeDado)
    {
        tipoDeDado = _tipoDeDado;

        if(_tipoDeDado == TipoDeDado.Verde)
        {
            caras = new CaraDado.Cara[6] {CaraDado.Cara.Cerebro, CaraDado.Cara.Cerebro, CaraDado.Cara.Cerebro,
                                          CaraDado.Cara.Patas, CaraDado.Cara.Patas, CaraDado.Cara.Disparo};
        }

        else if (_tipoDeDado == TipoDeDado.Amarillo)
        {
            caras = new CaraDado.Cara[6] {CaraDado.Cara.Cerebro, CaraDado.Cara.Cerebro, CaraDado.Cara.Patas,
                                          CaraDado.Cara.Patas, CaraDado.Cara.Disparo, CaraDado.Cara.Disparo};
        }

        else if (_tipoDeDado == TipoDeDado.Rojo)
        {
            caras = new CaraDado.Cara[6] {CaraDado.Cara.Cerebro, CaraDado.Cara.Patas, CaraDado.Cara.Patas,
                                          CaraDado.Cara.Disparo, CaraDado.Cara.Disparo, CaraDado.Cara.Disparo};
        }

        else if (_tipoDeDado == TipoDeDado.Novia)
        {
            caras = new CaraDado.Cara[6] {CaraDado.Cara.Cerebro, CaraDado.Cara.Patas, CaraDado.Cara.Patas,
                                          CaraDado.Cara.Patas, CaraDado.Cara.Disparo, CaraDado.Cara.Disparo};
        }

        else if (_tipoDeDado == TipoDeDado.Novio)
        {
            caras = new CaraDado.Cara[6] {CaraDado.Cara.Cerebrox2, CaraDado.Cara.Patas, CaraDado.Cara.Patas,
                                          CaraDado.Cara.Disparo, CaraDado.Cara.Disparo, CaraDado.Cara.Disparox2};
        }

        else if (_tipoDeDado == TipoDeDado.Santa)
        {
            caras = new CaraDado.Cara[6] {CaraDado.Cara.Cerebro, CaraDado.Cara.Cerebrox2, CaraDado.Cara.Patas,
                                          CaraDado.Cara.Disparo, CaraDado.Cara.Casco, CaraDado.Cara.PatasACerebros};
        }
    }
}