using System;
using Digi21.DigiNG.Plugin.Command;
using Digi21.DigiNG.Entities;
using Digi21.DigiNG;
using Digi21.Digi3D;
using Digi21.Math;

namespace Acme
{
    [Command(Name = "borra_puntos")]
    public class BorraPuntos : Command
    {
        public BorraPuntos()
        {
            this.Initialize += new EventHandler(BorraPuntos_Initialize);
            this.SetFocus += new EventHandler(BorraPuntos_SetFocus);
            this.DataUp += new EventHandler<Point3DEventArgs>(BorraPuntos_DataUp);
            this.EntitySelected += new EventHandler<EntitySelectedEventArgs>(BorraPuntos_EntitySelected);
        }

        void BorraPuntos_EntitySelected(object sender, EntitySelectedEventArgs e)
        {
            DigiNG.DrawingFile.Delete(e.Entity);
            DigiNG.RenderScene();
            DigiNG.NewTransaction();
        }

        void BorraPuntos_DataUp(object sender, Point3DEventArgs e)
        {
            DigiNG.SelectEntity(e.Coordinates, entidad => entidad is ReadOnlyPoint);
        }

        void BorraPuntos_SetFocus(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        void BorraPuntos_Initialize(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        private static void InvitaUsuarioSeleccionar()
        {
            Digi3D.StatusBar.Text = "Selecciona el punto a eliminar...";
        }
    }
}
