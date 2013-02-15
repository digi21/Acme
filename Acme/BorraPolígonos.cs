using System;
using Digi21.DigiNG.Plugin.Command;
using Digi21.DigiNG.Entities;
using Digi21.DigiNG;
using Digi21.Digi3D;
using Digi21.Math;

namespace Acme
{
    [Command(Name = "borra_polígonos")]
    public class BorraPolígonos : Command
    {
        public BorraPolígonos()
        {
            this.Initialize += new EventHandler(BorraPolígonos_Initialize);
            this.SetFocus += new EventHandler(BorraPolígonos_SetFocus);
            this.DataUp += new EventHandler<Point3DEventArgs>(BorraPolígonos_DataUp);
            this.EntitySelected += new EventHandler<EntitySelectedEventArgs>(BorraPolígonos_EntitySelected);
        }

        void BorraPolígonos_EntitySelected(object sender, EntitySelectedEventArgs e)
        {
            DigiNG.DrawingFile.Delete(e.Entity);
            DigiNG.RenderScene();
            DigiNG.NewTransaction();
        }

        void BorraPolígonos_DataUp(object sender, Point3DEventArgs e)
        {
            DigiNG.SelectEntity(e.Coordinates, entidad => entidad is ReadOnlyPolygon);
        }

        void BorraPolígonos_SetFocus(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        void BorraPolígonos_Initialize(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        private static void InvitaUsuarioSeleccionar()
        {
            Digi3D.StatusBar.Text = "Selecciona el polígono a eliminar...";
        }
    }
}
