using System;
using Digi21.DigiNG.Plugin.Command;
using Digi21.DigiNG.Entities;
using Digi21.DigiNG;
using Digi21.Digi3D;
using Digi21.Math;

namespace Acme
{
    [Command(Name = "borra_complejos")]
    public class BorraComplejos : Command
    {
        public BorraComplejos()
        {
            this.Initialize += new EventHandler(BorraComplejos_Initialize);
            this.SetFocus += new EventHandler(BorraComplejos_SetFocus);
            this.DataUp += new EventHandler<Point3DEventArgs>(BorraComplejos_DataUp);
            this.EntitySelected += new EventHandler<EntitySelectedEventArgs>(BorraComplejos_EntitySelected);
        }

        void BorraComplejos_EntitySelected(object sender, EntitySelectedEventArgs e)
        {
            DigiNG.DrawingFile.Delete(e.Entity);
            DigiNG.RenderScene();
            DigiNG.NewTransaction();
        }

        void BorraComplejos_DataUp(object sender, Point3DEventArgs e)
        {
            DigiNG.SelectEntity(e.Coordinates, entidad => entidad is ReadOnlyComplex);
        }

        void BorraComplejos_SetFocus(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        void BorraComplejos_Initialize(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        private static void InvitaUsuarioSeleccionar()
        {
            Digi3D.StatusBar.Text = "Selecciona el complejo a eliminar...";
        }
    }
}
