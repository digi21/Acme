using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digi21.DigiNG.Plugin.Command;
using Digi21.Digi3D;
using Digi21.DigiNG;
using Digi21.Math;
using Digi21.DigiNG.Entities;
using UtilidadesDigi;

namespace Acme
{
    [Command(Name="SuperBininfo")]
    public class SuperBininfo : Command
    {
        public SuperBininfo()
        {
            this.Initialize += new EventHandler(SuperBininfo_Initialize);
        }

        void SuperBininfo_Initialize(object sender, EventArgs e)
        {
            try
            {
                ImprimeDatos("Todas las entidades", DigiNG.DrawingFile);
                ImprimeDatos("Líneas", DigiNG.DrawingFile.SoloLíneas());
                ImprimeDatos("Puntos", DigiNG.DrawingFile.SoloPuntos());
                ImprimeDatos("Textos", DigiNG.DrawingFile.SoloTextos());
                ImprimeDatos("Polígonos", DigiNG.DrawingFile.SoloPolígonos());
                ImprimeDatos("Complejos", DigiNG.DrawingFile.SoloComplejos());
            }
            finally
            {
                Dispose();
            }
        }

        private static void ImprimeDatos(string título, IEnumerable<Entity> datos)
        {
            Digi3D.OutputWindow.WriteLine("{0}--------------", título);

            Digi3D.OutputWindow.WriteLine("{0} entidades",
                datos.Count());

            Window3D maxmin = new Window3D();
            foreach (var entidad in datos)
                maxmin.Union(entidad);

            Digi3D.OutputWindow.WriteLine("Las máximas y mínimas son: {0}", maxmin);

            var gruposDeCódigos = from entidad in datos
                                  from código in entidad.Codes
                                  group entidad by código.Name;

            foreach (var grupo in gruposDeCódigos)
            {
                Digi3D.OutputWindow.WriteLine("{0} : {1}",
                    grupo.Key,
                    grupo.Count());
            }
        }
    }
}
