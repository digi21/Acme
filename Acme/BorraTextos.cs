using System;
using Digi21.DigiNG.Plugin.Command;
using Digi21.DigiNG.Entities;
using Digi21.DigiNG;
using Digi21.Digi3D;
using Digi21.Math;

namespace Acme
{
    [Command(Name="borra_textos")]
    public class BorraTextos : Command
    {
        public BorraTextos()
        {
            this.Initialize += new EventHandler(BorraTextos_Initialize);
            this.SetFocus += new EventHandler(BorraTextos_SetFocus);
            this.DataUp += new EventHandler<Point3DEventArgs>(BorraTextos_DataUp);
            this.EntitySelected += new EventHandler<EntitySelectedEventArgs>(BorraTextos_EntitySelected);
            //this.AllowRepeat = true;
        }

        void BorraTextos_EntitySelected(object sender, EntitySelectedEventArgs e)
        {
            DigiNG.DrawingFile.Delete(e.Entity);
            DigiNG.RenderScene();
            DigiNG.NewTransaction();
            //Dispose();
        }

        void BorraTextos_DataUp(object sender, Point3DEventArgs e)
        {
            DigiNG.SelectEntity(e.Coordinates, entidad => entidad is ReadOnlyText);
        }

        void BorraTextos_SetFocus(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        void BorraTextos_Initialize(object sender, EventArgs e)
        {
            InvitaUsuarioSeleccionar();
        }

        private static void InvitaUsuarioSeleccionar()
        {
            Digi3D.StatusBar.Text = "Selecciona el texto a eliminar...";
        }
    }
}
