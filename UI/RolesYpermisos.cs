using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BE;

namespace UI
{
    public partial class RolesYpermisos : Form
    {
        private readonly BLLManagerSeguridad _bll = BLLManagerSeguridad.Instance;
        private readonly BLLUsuario _bllUser = BLLUsuario.Instance;

        private List<BERol> _roles = new();
        private List<BEPermisoSimple> _permisos = new();
        private List<BEUsuario> _usuarios = new();

        private bool _suppressCheckboxEvents = false;

        public RolesYpermisos()
        {
            InitializeComponent();
            ModernTheme.Apply(this);
            Resize += (_, _) => OrganizarPaneles();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                WindowState = FormWindowState.Maximized;
            OrganizarPaneles();
        }
        private void OrganizarPaneles()
        {
            const int margen = 20;
            const int espacio = 15;
            const int alto = 620;
            var cuatroColumnas = ClientSize.Width >= 1455;
            var columnas = cuatroColumnas ? 4 : 2;
            var paneles = new[] { panelUsuarios, panelRoles, panelPermisos, panelAsignados };
            for (var i = 0; i < paneles.Length; i++)
            {
                var ancho = cuatroColumnas
                    ? (i == 3 ? 380 : 330) + (ClientSize.Width - 1455) / 4
                    : Math.Max(380, (ClientSize.Width - margen * 2 - espacio) / 2);
                var izquierda = cuatroColumnas
                    ? margen + i * (330 + (ClientSize.Width - 1455) / 4 + espacio)
                    : margen + (i % 2) * (ancho + espacio);
                paneles[i].Bounds = new System.Drawing.Rectangle(
                    izquierda,
                    105 + (i / columnas) * (alto + espacio), ancho, alto);
            }
            AutoScrollMinSize = new System.Drawing.Size(0, cuatroColumnas ? 745 : 1380);
        }
        private void RolesYpermisos_Load(object? sender, EventArgs e)
        {
            Utiles();
            CargarDatos();
        }
        private void Utiles()
        {
            treeViewPermisos.HideSelection = false;
            treeViewPermisosPorRol.HideSelection = false;
            treeViewRolesYPermisosUsuario.HideSelection = false;
            treeViewPermisos.CheckBoxes = true;
            treeViewPermisosPorRol.CheckBoxes = true;
            treeViewRolesYPermisosUsuario.CheckBoxes = true;
            treeViewRoles.CheckBoxes = true;
            treeViewUsuarios.CheckBoxes = false;
            treeViewRoles.HideSelection = false;
            treeViewUsuarios.HideSelection = false;
        }
        private void CargarDatos()
        {
            _roles = _bll.GetRoles().ToList();
            _permisos = _bll.GetPermisos().ToList();
            _usuarios = _bllUser.GetUsuarios().ToList();

            RefrescarUtiles();
        }
        private void RefrescarUtiles()
        {
            RolesTree();
            UsuariosTree();
            PermisosTree();
            ActualizarPermisosRolSeleccionado();
            ActualizarRolesUsuarioSeleccionado();
        }
        private void RolesTree()
        {
            treeViewRoles.BeginUpdate();
            treeViewRoles.Nodes.Clear();

            foreach (var r in _roles.OrderBy(r => r.Nombre))
            {
                var n = new TreeNode(r.Nombre ?? $"Rol {r.Id}") { Tag = r, Checked = false };
                treeViewRoles.Nodes.Add(n);
            }

            treeViewRoles.ExpandAll();
            treeViewRoles.EndUpdate();
        }
        private void UsuariosTree()
        {
            treeViewUsuarios.BeginUpdate();
            treeViewUsuarios.Nodes.Clear();

            foreach (var u in _usuarios.OrderBy(u => u.Nombre))
            {
                var n = new TreeNode(u.Nombre ?? $"Usuario {u.Id}") { Tag = u, Checked = false };
                treeViewUsuarios.Nodes.Add(n);
            }

            treeViewUsuarios.ExpandAll();
            treeViewUsuarios.EndUpdate();
        }
        private void PermisosTree()
        {
            treeViewPermisos.BeginUpdate();
            treeViewPermisos.Nodes.Clear();
            var allTipos = Enum.GetValues(typeof(BETipoPermiso))
                               .Cast<BETipoPermiso>()
                               .OrderBy(tp => tp.ToString())
                               .ToList();

            foreach (var tp in allTipos)
            {
                var permisoReal = _permisos.FirstOrDefault(p => p.Permiso == tp);
                TreeNode node;
                if (permisoReal != null)
                {
                    node = new TreeNode(FormatPermisoDisplay(permisoReal))
                    {
                        Tag = permisoReal,
                        Checked = false
                    };
                }
                else
                {
                    node = new TreeNode(tp.ToString().Replace("_", " "))
                    {
                        Tag = null,
                        Checked = false
                    };
                }
                treeViewPermisos.Nodes.Add(node);
            }
            treeViewPermisos.ExpandAll();
            treeViewPermisos.EndUpdate();
        }
        private string FormatPermisoDisplay(BEPermisoSimple permiso)
        {
            return permiso.Nombre ?? permiso.Permiso.ToString().Replace("_", " ");
        }

        private string GetCategoria(BETipoPermiso tp)
        {
            var t = tp.ToString();
            var idx = t.IndexOf('_');
            if (idx > 0) return t.Substring(0, idx);
            return t;
        }
        private BERol? ObtenerRolSeleccionado()
        {
            if (treeViewRoles.SelectedNode?.Tag is BERol rol) return rol;
            return null;
        }

        private BEPermisoSimple? ObtenerPermisoSeleccionadoEnTree()
        {
            if (treeViewPermisos.SelectedNode?.Tag is BEPermisoSimple p) return p;
            var checkedList = ObtenerPermisosSeleccionadosEnTreeAll();
            return checkedList.FirstOrDefault();
        }

        private BEPermisoSimple? ObtenerPermisoSeleccionadoEnTreePorRol()
        {
            if (treeViewPermisosPorRol.SelectedNode?.Tag is BEPermisoSimple p) return p;
            var checkedList = ObtenerPermisosSeleccionadosEnTreePorRolAll();
            return checkedList.FirstOrDefault();
        }
        private BEUsuario? ObtenerUsuarioSeleccionado()
        {
            if (treeViewUsuarios.SelectedNode?.Tag is BEUsuario u) return u;
            return null;
        }
        private TreeNode CrearNodoRolRecursivo(BERol rol)
        {
            var n = new TreeNode(rol.Nombre ?? $"Rol {rol.Id}") { Tag = rol };
            foreach (var p in rol.Permisos)
                n.Nodes.Add(new TreeNode(p.Nombre ?? p.Permiso.ToString()) { Tag = p });
            foreach (var hijo in rol.RolesHijos)
                n.Nodes.Add(CrearNodoRolRecursivo(hijo));
            return n;
        }
        private void ActualizarRolesUsuarioSeleccionado()
        {
            var usuario = ObtenerUsuarioSeleccionado();
            treeViewRolesYPermisosUsuario.BeginUpdate();
            treeViewRolesYPermisosUsuario.Nodes.Clear();
            _suppressCheckboxEvents = true;

            if (usuario != null)
            {
                var root = new TreeNode(usuario.Nombre ?? $"Usuario {usuario.Id}") { Tag = usuario, Checked = false };
                var roles = usuario.Roles ?? new List<BERol>();
                foreach (var r in roles)
                {
                    root.Nodes.Add(CrearNodoRolRecursivo(r));
                }
                var permisosDirectos = usuario.Permisos?.OfType<BEPermisoSimple>().ToList() ?? new List<BEPermisoSimple>();
                if (permisosDirectos.Any())
                {
                    var directNode = new TreeNode("Permisos directos") { Tag = null, Checked = false };
                    foreach (var p in permisosDirectos)
                    {
                        var pNode = new TreeNode(p.Nombre ?? p.Permiso.ToString()) { Tag = p, Checked = false };
                        directNode.Nodes.Add(pNode);
                    }
                    root.Nodes.Add(directNode);
                }

                treeViewRolesYPermisosUsuario.Nodes.Add(root);
                root.Expand();
                checkBox1.Checked = usuario.Bloqueado;
                checkBox3.Checked = usuario.Activo;
                txtUsuarioId.Text = usuario.Id.ToString();
                txtUsuarioNombre.Text = usuario.Nombre ?? string.Empty;

                // La contraseña se guarda como hash y no se puede mostrar.
                txtUsuarioPassword.Text = "Protegida";
            }
            else
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                txtUsuarioId.Text = string.Empty;
                txtUsuarioNombre.Text = string.Empty;
                txtUsuarioPassword.Text = string.Empty;
            }
            _suppressCheckboxEvents = false;
            treeViewRolesYPermisosUsuario.EndUpdate();
        }
        private List<BEPermisoSimple> ObtenerPermisosSeleccionadosEnTreeAll()
        {
            var list = new List<BEPermisoSimple>();
            CollectCheckedPermisos(treeViewPermisos.Nodes, list);
            return list;
        }
        private List<BEPermisoSimple> ObtenerPermisosSeleccionadosEnTreePorRolAll()
        {
            var list = new List<BEPermisoSimple>();
            CollectCheckedPermisos(treeViewPermisosPorRol.Nodes, list);
            return list;
        }
        private void CollectCheckedPermisos(TreeNodeCollection nodes, List<BEPermisoSimple> list)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Checked && n.Tag is BEPermisoSimple p) list.Add(p);
                if (n.Nodes.Count > 0) CollectCheckedPermisos(n.Nodes, list);
            }
        }
        private List<BERol> ObtenerRolesSeleccionadosEnTreeRolesAll()
        {
            var list = new List<BERol>();
            CollectCheckedRoles(treeViewRoles.Nodes, list);
            return list;
        }
        private List<BERol> ObtenerRolesSeleccionadosEnTreeUsuarioAll()
        {
            var list = new List<BERol>();
            CollectCheckedRoles(treeViewRolesYPermisosUsuario.Nodes, list);
            return list;
        }
        private void CollectCheckedRoles(TreeNodeCollection nodes, List<BERol> list)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Checked && n.Tag is BERol r) list.Add(r);
                if (n.Nodes.Count > 0) CollectCheckedRoles(n.Nodes, list);
            }
        }
        private void treeViewUsuarios_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            ActualizarRolesUsuarioSeleccionado();
        }
        private void treeViewRoles_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            ActualizarPermisosRolSeleccionado();
        }
        private void treeViewPermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var nodo = e.Node;
            if (nodo == null) return;
            if (nodo.Tag is BEPermisoSimple)
            {
                if (!nodo.Checked) nodo.Checked = true;
                nodo.EnsureVisible();
                return;
            }
            foreach (TreeNode hijo in nodo.Nodes)
            {
                if (hijo.Tag is BEPermisoSimple)
                {
                    treeViewPermisos.SelectedNode = hijo;
                    hijo.EnsureVisible();
                    break;
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressCheckboxEvents) return;

            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null) return;
            var rolesUsuario = usuario.Roles ?? new List<BERol>();
            bool esAdmin = rolesUsuario.Any(r =>
                !string.IsNullOrWhiteSpace(r.Nombre) &&
                r.Nombre.Trim().Equals("ADMIN", StringComparison.OrdinalIgnoreCase));

            if (esAdmin && checkBox1.Checked)
            {
                _suppressCheckboxEvents = true;
                checkBox1.Checked = false;
                _suppressCheckboxEvents = false;

                MessageBox.Show("No se puede bloquear un usuario con rol ADMIN.", "Restricción",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            usuario.Bloqueado = checkBox1.Checked;
            if (usuario.Bloqueado)
                usuario.Activo = false;

            try
            {
                _suppressCheckboxEvents = true;
                checkBox3.Checked = usuario.Activo;
                checkBox1.Checked = usuario.Bloqueado;

                _suppressCheckboxEvents = false;

                BLLUsuario.Instance.ModificarUsuario(usuario);
                CargarDatos();
                SelectUsuarioById(usuario.Id);
            }
            catch (Exception ex)
            {
                _suppressCheckboxEvents = false;
                MessageBox.Show($"Error al actualizar estado Bloqueado: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressCheckboxEvents) return;

            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null) return;
            usuario.Activo = checkBox3.Checked;
            if (usuario.Activo)
                usuario.Bloqueado = false;

            try
            {
                _suppressCheckboxEvents = true;
                checkBox1.Checked = usuario.Bloqueado;
                checkBox3.Checked = usuario.Activo;

                _suppressCheckboxEvents = false;

                BLLUsuario.Instance.ModificarUsuario(usuario);
                CargarDatos();
                SelectUsuarioById(usuario.Id);
            }
            catch (Exception ex)
            {
                _suppressCheckboxEvents = false;
                MessageBox.Show($"Error al actualizar estado Activo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SelectUsuarioById(int id)
        {
            foreach (TreeNode n in treeViewUsuarios.Nodes)
            {
                if (n.Tag is BEUsuario u && u.Id == id)
                {
                    treeViewUsuarios.SelectedNode = n;
                    n.EnsureVisible();
                    return;
                }
            }
        }
        private void BtnAltaRol_Click(object sender, EventArgs e)
        {
            var nombre = textBox1.Text?.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Ingrese un nombre para el rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevo = new BERol { Nombre = nombre, Descripcion = string.Empty, Permisos = new List<BEPermisoSimple>() };
            _bll.CrearRol(nuevo);
            CargarDatos();
            MessageBox.Show("Rol creado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnModificarRol_Click(object sender, EventArgs e)
        {
            var rol = ObtenerRolSeleccionado();
            if (rol == null)
            {
                MessageBox.Show("Seleccione un rol para modificar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nuevoNombre = textBox1.Text?.Trim();
            if (string.IsNullOrEmpty(nuevoNombre))
            {
                MessageBox.Show("El nombre del rol no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            rol.Nombre = nuevoNombre;
            var mod = _bll.ModificarRol(rol);
            if (mod == null)
            {
                MessageBox.Show("No se pudo modificar el rol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CargarDatos();
            MessageBox.Show("Rol modificado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnEliminarRol_Click(object sender, EventArgs e)
        {
            var rol = ObtenerRolSeleccionado();
            if (rol == null)
            {
                MessageBox.Show("Seleccione un rol para eliminar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show($"¿Eliminar rol '{rol.Nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            if (!_bll.EliminarRol(rol.Id))
            {
                MessageBox.Show("No se pudo eliminar el rol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CargarDatos();
            MessageBox.Show("Rol eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnAsociarPermisoArol_Click(object sender, EventArgs e)
        {
            var rol = ObtenerRolSeleccionado();
            if (rol == null)
            {
                MessageBox.Show("Seleccione un rol destino.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var permisos = ObtenerPermisosSeleccionadosEnTreeAll();
            if (!permisos.Any())
            {
                var single = ObtenerPermisoSeleccionadoEnTree();
                if (single != null) permisos.Add(single);
            }
            if (!permisos.Any())
            {
                MessageBox.Show("Seleccione uno o más permisos en el árbol para asociar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var failed = new List<string>();
            foreach (var permiso in permisos)
            {
                if (!_bll.AsociarPermisoARol(rol.Id, permiso.Id))
                {
                    failed.Add(permiso.Nombre ?? permiso.Permiso.ToString());
                }
            }
            CargarDatos();
            if (failed.Any())
                MessageBox.Show($"Algunos permisos no pudieron asociarse: {string.Join(", ", failed)}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Permisos asociados al rol.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnQuitarPermisoArol_Click(object sender, EventArgs e)
        {
            var rol = ObtenerRolSeleccionado();
            if (rol == null)
            {
                MessageBox.Show("Seleccione un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var permisos = ObtenerPermisosSeleccionadosEnTreePorRolAll();
            if (!permisos.Any())
            {
                var single = ObtenerPermisoSeleccionadoEnTreePorRol();
                if (single != null) permisos.Add(single);
            }
            if (!permisos.Any())
            {
                MessageBox.Show("Seleccione uno o más permisos del rol (árbol Permisos por Rol) para quitar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var failed = new List<string>();
            foreach (var permiso in permisos)
            {
                if (!_bll.QuitarPermisoDeRol(rol.Id, permiso.Id))
                {
                    failed.Add(permiso.Nombre ?? permiso.Permiso.ToString());
                }
            }
            CargarDatos();
            if (failed.Any())
                MessageBox.Show($"Algunos permisos no pudieron removerse: {string.Join(", ", failed)}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Permisos removidos del rol.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnAsociarRolesaUsuarios_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null)
            {
                MessageBox.Show("Seleccione un usuario destino.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRoles = ObtenerRolesSeleccionadosEnTreeRolesAll();
            if (!selectedRoles.Any())
            {
                if (treeViewRoles.SelectedNode?.Tag is BERol single) selectedRoles.Add(single);
            }
            if (!selectedRoles.Any())
            {
                MessageBox.Show("Seleccione uno o más roles para asignar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var actuales = usuario.Roles ?? new List<BERol>();
            var actualesIds = new HashSet<int>(actuales.Select(r => r.Id));
            var duplicados = selectedRoles.Where(r => actualesIds.Contains(r.Id)).ToList();
            try
            {
                if (duplicados.Any())
                    throw new InvalidOperationException($"El usuario ya tiene los roles: {string.Join(", ", duplicados.Select(r => r.Nombre))}");

                var nuevos = actuales.Concat(selectedRoles)
                                     .GroupBy(r => r.Id)
                                     .Select(g => g.First())
                                     .ToList();

                BLLUsuario.Instance.AsignarRoles(usuario, nuevos);
                CargarDatos();
                SelectUsuarioById(usuario.Id);
                MessageBox.Show("Roles asignados al usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al asignar roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DesasociarRolesAUsuarios_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null)
            {
                MessageBox.Show("Seleccione un usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var rolesAEliminar = ObtenerRolesSeleccionadosEnTreeUsuarioAll();
            if (!rolesAEliminar.Any())
            {
                var sel = treeViewRolesYPermisosUsuario.SelectedNode;
                if (sel?.Tag is BERol r) rolesAEliminar.Add(r);
            }
            if (!rolesAEliminar.Any())
            {
                MessageBox.Show("Seleccione uno o más roles del usuario (marcados en el árbol) para quitar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var restantes = (usuario.Roles ?? new List<BERol>())
                            .Where(r => rolesAEliminar.All(rem => rem.Id != r.Id))
                            .ToList();
            BLLUsuario.Instance.AsignarRoles(usuario, restantes);
            CargarDatos();
            SelectUsuarioById(usuario.Id);
            MessageBox.Show("Roles removidos del usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnLimpiarCampos_Click(object sender, EventArgs e)
        {
            LimpiarCamposRol();
        }

        private void LimpiarCamposRol()
        {
            textBox1.Clear();
            label6.Text = "ID automático";
            treeViewPermisosPorRol.Nodes.Clear();
            _suppressCheckboxEvents = true;
            LimpiarChecksDeTreeView(treeViewPermisos);
            LimpiarChecksDeTreeView(treeViewRoles);
            LimpiarChecksDeTreeView(treeViewPermisosPorRol);
            LimpiarChecksDeTreeView(treeViewRolesYPermisosUsuario);
            _suppressCheckboxEvents = false;
        }
        private void LimpiarChecksDeTreeView(TreeView tv)
        {
            tv.BeginUpdate();
            LimpiarChecksRecursivo(tv.Nodes);
            tv.EndUpdate();
        }
        private void LimpiarChecksRecursivo(TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Checked) n.Checked = false;
                if (n.Nodes.Count > 0)
                    LimpiarChecksRecursivo(n.Nodes);
            }
        }
        private void BtnAsociarPermisoAUsuario_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null)
            {
                MessageBox.Show("Seleccione un usuario destino.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var permisos = ObtenerPermisosSeleccionadosEnTreeAll();
            if (!permisos.Any())
            {
                var single = ObtenerPermisoSeleccionadoEnTree();
                if (single != null) permisos.Add(single);
            }
            if (!permisos.Any())
            {
                MessageBox.Show("Seleccione uno o más permisos para asociar al usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                BLLUsuario.Instance.AsignarPermisos(usuario, permisos);
                CargarDatos();
                SelectUsuarioById(usuario.Id);
                MessageBox.Show("Permisos asociados al usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al asociar permisos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnQuitarPermisoAUsuario_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null)
            {
                MessageBox.Show("Seleccione un usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var permisos = new List<BEPermisoSimple>();
            CollectCheckedPermisos(treeViewRolesYPermisosUsuario.Nodes, permisos);
            if (!permisos.Any())
            {
                var sel = treeViewRolesYPermisosUsuario.SelectedNode;
                if (sel?.Tag is BEPermisoSimple p) permisos.Add(p);
            }
            if (!permisos.Any())
            {
                MessageBox.Show("Seleccione uno o más permisos del usuario para quitar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                BLLUsuario.Instance.QuitarPermisos(usuario, permisos);
                CargarDatos();
                SelectUsuarioById(usuario.Id);
                MessageBox.Show("Permisos removidos del usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al quitar permisos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnCrearUser_Click(object sender, EventArgs e)
        {
            using var frm = new CrearUsuario();
            var dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                CargarDatos();
            }
        }
        private void ActualizarPermisosRolSeleccionado()
        {
            var rol = ObtenerRolSeleccionado();
            treeViewPermisosPorRol.BeginUpdate();
            treeViewPermisosPorRol.Nodes.Clear();

            if (rol != null)
            {
                var root = new TreeNode(rol.Nombre ?? $"Rol {rol.Id}") { Tag = rol };
                foreach (var p in rol.Permisos)
                    root.Nodes.Add(new TreeNode(p.Nombre ?? p.Permiso.ToString()) { Tag = p });
                foreach (var hijo in rol.RolesHijos)
                    root.Nodes.Add(CrearNodoRolRecursivo(hijo));
                treeViewPermisosPorRol.Nodes.Add(root);
                root.Expand();
                label6.Text = $"ID: {rol.Id}";
                textBox1.Text = rol.Nombre ?? string.Empty;
            }
            else
            {
                label6.Text = "ID automático";
                textBox1.Text = string.Empty;
            }
            treeViewPermisosPorRol.EndUpdate();
        }
        private void btnAsociarRolARol_Click_1(object sender, EventArgs e)
        {
            var padre = ObtenerRolSeleccionado();
            if (padre == null)
            {
                MessageBox.Show("Seleccione el rol padre (árbol Roles).", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var candidatos = ObtenerRolesSeleccionadosEnTreeRolesAll()
                             .Where(r => r.Id != padre.Id).ToList();
            if (!candidatos.Any())
            {
                MessageBox.Show("Marque uno o más roles para asociar como hijos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var mensajesError = new List<string>();
            foreach (var hijo in candidatos)
            {
                try
                {
                    _bll.AsociarRolARol(padre.Id, hijo.Id);
                }
                catch (Exception ex)
                {
                    mensajesError.Add($"'{hijo.Nombre}': {ex.Message}");
                }
            }

            CargarDatos();
            foreach (TreeNode n in treeViewRoles.Nodes)
                if (n.Tag is BERol r && r.Id == padre.Id) treeViewRoles.SelectedNode = n;

            if (mensajesError.Any())
                MessageBox.Show($"Se produjeron errores:\n{string.Join("\n", mensajesError)}", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Roles asociados como hijos.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnQuitarRolHijo_Click_1(object sender, EventArgs e)
        {
            var padre = ObtenerRolSeleccionado();
            if (padre == null)
            {
                MessageBox.Show("Seleccione el rol padre.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nodo = treeViewPermisosPorRol.SelectedNode;
            if (nodo?.Tag is not BERol rolHijo || rolHijo.Id == padre.Id)
            {
                MessageBox.Show("Seleccione un rol hijo en el árbol Permisos por Rol.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!_bll.QuitarRolHijo(padre.Id, rolHijo.Id))
            {
                MessageBox.Show("No se pudo quitar el rol hijo.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CargarDatos();
            MessageBox.Show("Rol hijo removido.", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (BLLUsuario.Instance.CurrentUser != null &&
                BLLUsuario.Instance.CurrentUser == usuario)
            {
                MessageBox.Show("No puede eliminar el usuario actualmente logueado.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var rolesUsuario = usuario.Roles ?? new List<BERol>();
            bool esAdmin = rolesUsuario.Any(r =>
                !string.IsNullOrWhiteSpace(r.Nombre) &&
                r.Nombre.Trim().Equals("ADMIN", StringComparison.OrdinalIgnoreCase));

            if (esAdmin)
            {
                MessageBox.Show("No se puede eliminar un usuario con rol ADMIN.", "Restricción",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea eliminar al usuario '{usuario.Nombre}' (ID {usuario.Id})?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                bool ok = BLLUsuario.Instance.EliminarUsuario(usuario.Id);
                if (!ok)
                {
                    MessageBox.Show("No se pudo eliminar el usuario.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CargarDatos();
                MessageBox.Show("Usuario eliminado correctamente.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
