using System;
using System.Windows.Forms;
using System.Drawing;
using Seguridad;
using BLL;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.BackColor = Color.FromArgb(16, 29, 48);
            menuStrip1.ForeColor = Color.White;
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.ForeColor = Color.White;
                item.DropDown.BackColor = Color.FromArgb(28, 43, 64);
                foreach (ToolStripItem child in item.DropDownItems)
                    child.ForeColor = Color.White;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            Permisos();
        }
        private void Permisos()
        {
            var current = BLLUsuario.Instance.CurrentUser;

            informaciónDeClienteToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("INFORMACIÓN_DE_CLIENTE");
            crearContratoToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("CREAR_CONTRATO");
            actualizaToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("ACTUALIZAR_CONTRATO");
            finalizarContratoToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("FINALIZAR_CONTRATO");
            historialDeServiciosToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("MONITOREAR_SERVICIO");
            generarFacturaToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("GENERAR_FACTURA");
            pagosYDeudasToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("PAGOS_Y_DEUDAS");
            reporteClientesToolStripMenuItem1.Visible = Autorizador.UsuarioActualTienePermiso("REPORTE_CLIENTES");
            reporteRecursosToolStripMenuItem1.Visible = Autorizador.UsuarioActualTienePermiso("REPORTE_RECURSOS");
            reporteEconomicoToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("REPORTE_ECONÓMICO");
            asignarPersonalToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("ASIGNAR_PERSONAL");
            disponibilidadDePersonalToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("DISPONIBILIDAD_PERSONAL");
            estadoDeVehiculosToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("ESTADO_VEHICULOS");
            asignarVehiculoToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("ASIGNAR_VEHICULO");
            monitorearServicioActivoToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("MONITOREAR_SERVICIO");
            generarInformeOperativoToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("GENERAR_INFORME_SERVICIO");
            crearServicioToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("CREAR_SERVICIO");
            gestionDePersonalToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("GESTION_PERSONAL");
            gestionarVehiculosToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("GESTION_VEHICULOS");
            moduloBackupToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("MODULO_BACKUP");
            bitacoraToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("BITACORA");
            restorteToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("RESTORE");
            rolesYPermisosToolStripMenuItem.Visible = Autorizador.UsuarioActualTienePermiso("ROLES_Y_PERMISOS");
            dashBoardToolStripMenuItem1.Visible = Autorizador.UsuarioActualTienePermiso("DASHBOARD");
            quickClientButton.Visible = informaciónDeClienteToolStripMenuItem.Visible;
            quickServiceButton.Visible = crearServicioToolStripMenuItem.Visible;
            quickPersonalButton.Visible = gestionDePersonalToolStripMenuItem.Visible;
            quickBillingButton.Visible = generarFacturaToolStripMenuItem.Visible;
            subtitleLabel.Text = current == null ? "Elegí una tarea para comenzar" : $"Sesión: {current.Nombre}  ·  Elegí una tarea";
        }
        private void informaciónDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("INFORMACIÓN_DE_CLIENTE")) return;
            using (GestionarInfoCliente gic = new UI.GestionarInfoCliente())
            {
                gic.ShowDialog();
            }
        }
        private void crearContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("CREAR_CONTRATO")) return;
            using (CrearContrato cc = new UI.CrearContrato())
            {
                cc.ShowDialog();
            }
        }
        private void actualizaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("ACTUALIZAR_CONTRATO")) return;
            using (ActualizarContrato ac = new UI.ActualizarContrato())
            {
                ac.ShowDialog();
            }
        }
        private void finalizarContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("FINALIZAR_CONTRATO")) return;
            using (FinalizarContrato fc = new UI.FinalizarContrato())
            {
                fc.ShowDialog();
            }
        }
        private void historialDeServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("MONITOREAR_SERVICIO")) return;
            using (HistorialServicios hds = new UI.HistorialServicios())
            {
                hds.ShowDialog();
            }
        }
        private void generarFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("GENERAR_FACTURA")) return;
            using (GenerarFactura gf = new UI.GenerarFactura())
            {
                gf.ShowDialog();
            }
        }
        private void pagosYDeudasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("PAGOS_Y_DEUDAS")) return;
            using (PagosYDeudas pyd = new UI.PagosYDeudas())
            {
                pyd.ShowDialog();
            }
        }
        private void dashBoardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("DASHBOARD")) return;
            using (DashBoard db = new UI.DashBoard())
            {
                db.ShowDialog();
            }
        }

        private void reporteClientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("REPORTE_CLIENTES")) return;
            using (ReporteCliente rc = new UI.ReporteCliente())
            {
                rc.ShowDialog();
            }
        }

        private void reporteEconomicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("REPORTE_ECONÓMICO")) return;
            using (ReporteEconomico re = new UI.ReporteEconomico())
            {
                re.ShowDialog();
            }
        }

        private void reporteRecursosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("REPORTE_RECURSOS")) return;
            using (ReporteRecursos rr = new UI.ReporteRecursos())
            {
                rr.ShowDialog();
            }
        }

        private void asignarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("ASIGNAR_PERSONAL")) return;
            using (AsignarPersonal ap = new UI.AsignarPersonal())
            {
                ap.ShowDialog();
            }
        }
        private void disponibilidadDePersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("DISPONIBILIDAD_PERSONAL")) return;
            using (DisponibilidadPersonal dp = new UI.DisponibilidadPersonal())
            {
                dp.ShowDialog();
            }
        }
        private void estadoDeVehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("ESTADO_VEHICULOS")) return;
            using (EstadoVehiculos ev = new UI.EstadoVehiculos())
            {
                ev.ShowDialog();
            }
        }
        private void asignarVehiculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("ASIGNAR_VEHICULO")) return;
            using (AsignarVehiculos av = new UI.AsignarVehiculos())
            {
                av.ShowDialog();
            }
        }
        private void monitorearServicioActivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("MONITOREAR_SERVICIO")) return;
            using (MonitorearServicios msa = new UI.MonitorearServicios())
            {
                msa.ShowDialog();
            }
        }
        private void generarInformeOperativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("GENERAR_INFORME_SERVICIO")) return;
            using (GenerarInformeOperativo gio = new UI.GenerarInformeOperativo())
            {
                gio.ShowDialog();
            }
        }
        private void crearServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("CREAR_SERVICIO")) return;
            using (CrearServicio cs = new UI.CrearServicio())
            {
                cs.ShowDialog();
            }
        }
        private void gestionDePersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("GESTION_PERSONAL")) return;
            using (GestionPersonal gp = new UI.GestionPersonal())
            {
                gp.ShowDialog();
            }
        }
        private void gestionarVehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("GESTION_VEHICULOS")) return;
            using (GestionVehiculos gv = new UI.GestionVehiculos())
            {
                gv.ShowDialog();
            }
        }
        private void moduloBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("MODULO_BACKUP")) return;
            using (Backup mb = new UI.Backup())
            {
                mb.ShowDialog();
            }
        }
        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("BITACORA")) return;
            using (Bitacora bt = new UI.Bitacora())
            {
                bt.ShowDialog();
            }
        }
        private void restorteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("RESTORE")) return;
            using (Restore rt = new UI.Restore())
            {
                rt.ShowDialog();
            }
        }
        private void rolesYPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Autorizador.AutorizarYMostrar("ROLES_Y_PERMISOS")) return;
            using (RolesYpermisos rp = new UI.RolesYpermisos())
            {
                rp.ShowDialog();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show("¿Desea cerrar sesión?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                BLLUsuario.Instance.Logout();

                Hide();
                using (var login = new Login())
                {
                    var dr = login.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        Permisos();
                        Show();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
