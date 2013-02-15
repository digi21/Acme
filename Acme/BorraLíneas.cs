using System;
using Digi21.DigiNG.Plugin.Command;
using Digi21.DigiNG.Entities;
using Digi21.DigiNG;
using Digi21.Digi3D;
using Digi21.Math;

namespace Acme
{
    [Command(Name = "borra_líneas")]
    public class BorraLíneas : Command
    {
        public BorraLíneas()
        {
            this.Initialize += new EventHandler(BorraLíneas_Initialize);
            this.SetFocus += new EventHandler(BorraLíneas_SetFocus);
            this.DataUp += new EventHandler<Point3DEventArgs>(BorraLíneas_DataUp);
            this.EntitySelected += new EventHandler<EntitySelectedEventArgs>(BorraLíneas_EntitySelected);
        }

        void BorraLíneas_EntitySelected(object sender, EntitySelectedEventArgs e)
        {
            DigiNG.DrawingFile.Delete(e.Entity);
            DigiNG.RenderScene();
            DigiNG.NewTransaction();
        }

        void BorraLíneas_DataUp(object sender, Point3DEventArgs e)
        {
            DigiNG.SelectEntity(e.Coordinates, entidad => entidad is ReadOnlyLine);
        }

        void BorraLíneas_SetFocus(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        void BorraLíneas_Initialize(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        private static void InvitaUsuarioSeleccionar()
        {
            Digi3D.StatusBar.Text = "Selecciona la línea a eliminar...";
        }
    }
}
