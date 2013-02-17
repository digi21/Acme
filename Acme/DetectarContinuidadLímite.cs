using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digi21.DigiNG.Plugin.Command;
using Digi21.Digi3D;
using Digi21.DigiNG;
using Digi21.DigiNG.Topology;
using UtilidadesDigi;
using Digi21.DigiNG.Entities;
using Digi21.Math;

namespace Acme
{
    [Command(Name="detectar_continuidad_digi")]
    public class DetectarContinuidadLímite : Command
    {
        public DetectarContinuidadLímite()
        {
            this.Initialize += new EventHandler(DetectarContinuidadLímite_Initialize);
        }

        void DetectarContinuidadLímite_Initialize(object sender, EventArgs e)
        {
            try
            {
                if (!CompruebaRequerimientos())
                    return;

                var nodosDeInterés = from nodo in UtilidadesDigiNG.EnumeraTodasLasEntidades<ReadOnlyLine>().NoEliminadas().QueTenganElCódigo(this.Args[0]).DetectNodes()
                                     select nodo.Key;

                var nodos = UtilidadesDigiNG.EnumeraTodasLasEntidades<ReadOnlyLine>().NoEliminadas().DetectNodes(
                    (Point2D coordenada) => nodosDeInterés.ContieneElPunto(coordenada));


                foreach (var nodo in nodos)
                {
                    Dictionary<string, int> contadores = new Dictionary<string, int>();
                    foreach (var vp in nodo)
                    {
                        foreach (var código in vp.Line.Codes)
                        {
                            if (contadores.ContainsKey(código.Name))
                                contadores[código.Name]++;
                            else
                                contadores[código.Name] = 1;
                        }
                    }

                    foreach (var código in contadores)
                    {
                        if (código.Value % 2 != 0)
                        {
                            string descripción = string.Format("Línea sin continuidad. Código {0}", código.Key);
                            Digi3D.Tasks.Add(new TaskGotoPoint(
                                new Point3D(nodo.Key), 
                                descripción, 
                                TaskSeverity.Error));
                        }
                    }
                }

            }
            finally
            {
                Dispose();
            }
        }

        private bool CompruebaRequerimientos()
        {
            if (this.Args.Length < 1)
            {
                Digi3D.Music(MusicType.Error);
                Digi3D.ShowBallon("detectar_continuidad_digi",
                    "No has introducido el código de límite",
                    2);
                return false;
            }

            if (DigiNG.ReferenceFiles.Length == 0)
            {
                Digi3D.Music(MusicType.Error);
                Digi3D.ShowBallon("detectar_continuidad_digi",
                    "No tienes cargado ningún archivo de referencia",
                    2);
                return false;
            }

            return true;
        }
    }
}
