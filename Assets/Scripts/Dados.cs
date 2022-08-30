using System;

[Serializable]
public class CaraDado
{
    public enum Cara { Cerebro, Patas, Disparo}
}

[Serializable]
public class Dado
{
    public enum TipoDeDado {  Verde, Amarillo, Rojo }
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
    }
}